import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TpnCalculationService } from '../services/tpn-calculation.service';
import { ChangeDetectorRef } from '@angular/core';
import { debounceTime } from 'rxjs';

@Component({
  selector: 'app-tpn-form',
  templateUrl: './tpn-form.component.html',
  styleUrls: ['./tpn-form.component.css']
})
export class TpnFormComponent implements OnInit {
  tpnForm!: FormGroup;
  savedData: any;
  baseDextroseLabel = '10%';
  concDextroseLabel = '50%';
  sodiumLabel = '3% NaCl';
  isCalculating: boolean = false;

  constructor(
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private tpnService: TpnCalculationService
  ) {
    this.createForm();
  }

  ngOnInit() {
    this.setupFormListeners();
    this.updateCalculations();
  }

  private createForm() {
    this.tpnForm = this.fb.group({
      uhid: [''],
      babyName: [''],
      dob: [''],
      birthTime: [''],
      age: [{ value: '', disabled: true }],
      gender: ['male'],

      // Syringe 1
      lipid: [0],
      mvi: [0],
      syringe1Celcel: [{ value: 0, disabled: true }],
      syringe1Ml: [0],

      // Syringe 2
      aminovenPer: [0],
      aminoven50ml: [0],
      naclPer: [0],
      nacl50ml: [0],
      kclPer: [0],
      kcl50ml: [0],
      calciumPer: [0],
      calcium50ml: [0],
      mgso4Per: [0],
      mgso450ml: [0],
      dextrose5Per: [0],
      dextrose550ml: [0],
      dextrose10Per: [0],
      dextrose1050ml: [0],
      dextrose25Per: [0],
      dextrose2550ml: [0],
      dextrose50Per: [0],
      dextrose5050ml: [0],

      // Totals
      totalVolume: [0],
      totalPer50: [0],
      potPhos: [0],
      calcium10: [0],

      // Dosing & Fluid
      dosingWeight: [0],
      tfr: [0],
      feed: [0],
      ivm: [0],
      a: [0],
      l: [0],
      g: [0],
      na: [0],
      k: [0],
      ca: [0],
      mg: [0],
      dextrose5: [0],
      dextrose10: [0],
      dextrose25: [0],
      dextrose50: [0],
      sodiumSource: ['3% NaCl'],

      // IVM Details
      n5: [0],
      n2: [0],
      ns: [0],
      dex10: [0],
      typeOfOralFeed: ['EBM/PDHM'],
      preNanStrength: ['None'],
      po4: [0],
      calciumViaTPN: [0],
      overfillFactor: [1],
      celcelInput: [0],

      // Nutritional
      nutriationtfr: [0],
      tfv: [0],
      feeds: [0],
      ivfMlKg: [0],
      ivfMl: [0],
      tpnFluid: [0],
      tpnGlucose: [0],
      fluidForGlucose: [0],
      osmolarity: [0],
      dextrosePercentage: [0],
      cnr: [0],
      caloriesToday: [0],
      proteinsToday: [0],
      naInIvm: [0],
      glucoseInIvm: [0],
      kInPotphos: [0],

      // Errors
      ivmError: ['']
    });
  }

  private setupFormListeners() {
    this.tpnForm.valueChanges.pipe(debounceTime(100)).subscribe(() => {
      this.updateCalculations();
      this.updateLabels();
      this.calculateExactAge();
    });

    this.tpnForm.get('sodiumSource')?.valueChanges.subscribe(() => {
      this.updateSodiumLabel();
    });

    this.tpnForm.get('dob')?.valueChanges.subscribe(() => this.calculateExactAge());
    this.tpnForm.get('birthTime')?.valueChanges.subscribe(() => this.calculateExactAge());
  }

  private calculateExactAge() {
    const dob = this.tpnForm.get('dob')?.value;
    const birthTime = this.tpnForm.get('birthTime')?.value;
    
    if (dob && birthTime) {
      const birthDateTime = new Date(`${dob}T${birthTime}`);
      const now = new Date();
      const diff = now.getTime() - birthDateTime.getTime();
      
      const days = Math.floor(diff / (1000 * 60 * 60 * 24));
      const hours = Math.floor((diff % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));

      this.tpnForm.get('age')?.setValue(`${days}d ${hours}h ${minutes}m`);
    }
  }


private updateCalculations() {
  const values = this.tpnForm.value;
  const dw = values.dosingWeight || 0;
  const B16 = (values.po4 * dw) / 93;

  // IVM Validation
  const ivmSum = +values.n5 + +values.n2 + +values.ns + +values.dex10;
  const ivmError = ivmSum > +values.ivm ? 'Wrong IVM volume' : '';

  // Syringe 1 Calculations
  const lipid = 5 * dw * values.l;
  const mvi = values.l > 0 ? dw : 0;
  const celcel = values.celcelInput ? 0.5 * dw : 0;
  const syringe1Total = (lipid + mvi + celcel) / 24;

  // Syringe 2 Calculations
  const aminoven = 10 * dw * values.a;
  const naInIvm = (values.n5 * 0.031 + values.n2 * 0.077 + values.ns * 0.154) / (dw || 1);
  const nacl = values.sodiumSource === 'CRL' 
    ? ((values.na - naInIvm) * dw) / 3
    : ((values.na - naInIvm) * dw) * 2;
  const kcl = this.tpnService.calculatePotassium(dw, values.k, B16);
  const calcium = values.calciumViaTPN ? (dw * values.ca) / 9.3 : 0;
  const mgso4 = (dw * values.mg) / 4;


  const tpnFluid = ((values.tfr - values.feed) * dw) - values.ivm;

  
  const fluidForGlucose = tpnFluid - lipid - aminoven - nacl - kcl - calcium - mvi - celcel;


  const dextroseResult = this.tpnService.calculateDextroseVolumes({
    tpnGlucose: values.g * dw * 1.44 - (values.dex10 * 0.1),
    fluidForGlucose: fluidForGlucose, 
    dextrose5: values.dextrose5,
    dextrose10: values.dextrose10,
    dextrose25: values.dextrose25,
    dextrose50: values.dextrose50,
    dosingWeight: dw,
    dextroseBase: values.dextrose5 ? 5 : 10,
    dextroseConc: values.dextrose25 ? 25 : 50
  });

    // Calculate Syringe 2 Total
    const syringe2Total = aminoven + nacl + kcl + calcium + mgso4 + 
                         dextroseResult.baseVolume + dextroseResult.concVolume;

 
    const calc50ml = (component: number) => 
      syringe2Total > 0 ? (component * 50) / syringe2Total : 0;
    
    // Osmolarity calculation
    const osmolarity = this.tpnService.calculateOsmolarity(
      lipid,
      aminoven,
      nacl,
      kcl,
      calcium,
      mgso4
    );

    // Update Form Values
    this.tpnForm.patchValue({
      lipid: this.tpnService.safeRound(lipid),
      mvi: this.tpnService.safeRound(mvi),
      syringe1Celcel: this.tpnService.safeRound(celcel),
      syringe1Ml: this.tpnService.safeRound(syringe1Total),

   
      aminovenPer: this.tpnService.safeRound(aminoven),
      aminoven50ml: this.tpnService.safeRound(calc50ml(aminoven)),
      naclPer: this.tpnService.safeRound(nacl),
      nacl50ml: this.tpnService.safeRound(calc50ml(nacl)),
      kclPer: this.tpnService.safeRound(kcl),
      kcl50ml: this.tpnService.safeRound(calc50ml(kcl)),
      calciumPer: this.tpnService.safeRound(calcium),
      calcium50ml: this.tpnService.safeRound(calc50ml(calcium)),
      mgso4Per: this.tpnService.safeRound(mgso4),
      mgso450ml: this.tpnService.safeRound(calc50ml(mgso4)),

      // Dextrose values
      dextrose5Per: this.tpnService.safeRound(dextroseResult.baseVolume),
      dextrose550ml: this.tpnService.safeRound(calc50ml(dextroseResult.baseVolume)),
      dextrose10Per: this.tpnService.safeRound(dextroseResult.baseVolume),
      dextrose1050ml: this.tpnService.safeRound(calc50ml(dextroseResult.baseVolume)),
      dextrose25Per: this.tpnService.safeRound(dextroseResult.concVolume),
      dextrose2550ml: this.tpnService.safeRound(calc50ml(dextroseResult.concVolume)),
      dextrose50Per: this.tpnService.safeRound(dextroseResult.concVolume),
      dextrose5050ml: this.tpnService.safeRound(calc50ml(dextroseResult.concVolume)),

      // Totals
      totalVolume: this.tpnService.safeRound(syringe1Total + syringe2Total),
      totalPer50: this.tpnService.safeRound((syringe1Total + syringe2Total) / 24),
      potPhos: this.tpnService.safeRound((values.po4 * dw) / 93),
      calcium10: this.tpnService.safeRound(values.ca ? (dw * values.ca) / 9.3 : 0),

      // Nutritional
      nutriationtfr: this.tpnService.safeRound(values.tfr),
      tfv: this.tpnService.safeRound(dw * values.tfr),
      feeds: this.tpnService.safeRound(dw * values.feed),
      ivfMlKg: this.tpnService.safeRound(values.tfr - values.feed),
      ivfMl: this.tpnService.safeRound(dw * (values.tfr - values.feed)),
      tpnFluid: this.tpnService.safeRound(tpnFluid), 
      tpnGlucose: this.tpnService.safeRound(values.g * dw * 1.44 - (values.dex10 * 0.1)),
      fluidForGlucose: this.tpnService.safeRound(fluidForGlucose),
       osmolarity: this.tpnService.safeRound(osmolarity),
      dextrosePercentage: this.tpnService.safeRound(((values.g * dw * 1.44 - (values.dex10 * 0.1)) / (syringe1Total + syringe2Total)) * 100),
      cnr: this.tpnService.calculateCNR(values.g, values.l, values.a),
      caloriesToday: this.tpnService.calculateCalories(values.a, values.l, values.g, values.feed, values.typeOfOralFeed, values.preNanStrength),
      proteinsToday: this.tpnService.calculateProteins(values.a, values.feed, values.typeOfOralFeed, values.preNanStrength),
      naInIvm: this.tpnService.safeRound(naInIvm),
      glucoseInIvm: this.tpnService.safeRound(values.dex10 * 0.1),
      kInPotphos: this.tpnService.safeRound((4.4 * B16) / (dw || 1)),
      ivmError: ivmError
    }, { emitEvent: false });
  }

  updateDextrose(type: 'base' | 'conc', percentage: number) {
    const state: any = {};
    
    if (type === 'base') {
      state.dextrose5 = percentage === 5;
      state.dextrose10 = percentage === 10;
    } else {
      state.dextrose25 = percentage === 25;
      state.dextrose50 = percentage === 50;
    }

    this.tpnForm.patchValue(state);
    this.updateLabels();
  }

  private updateLabels() {
    const values = this.tpnForm.value;
    this.baseDextroseLabel = values.dextrose5 ? '5%' : '10%';
    this.concDextroseLabel = values.dextrose25 ? '25%' : '50%';
    this.cd.detectChanges();
  }

  private updateSodiumLabel() {
    this.sodiumLabel = this.tpnForm.value.sodiumSource === 'CRL' ? 'Conc. RL' : '3% NaCl';
    this.cd.detectChanges();
  }

  onSubmit() {
    this.savedData = this.tpnForm.getRawValue();
  }

  clearData() {
    this.tpnForm.reset({
      gender: 'male',
      sodiumSource: '3% NaCl',
      typeOfOralFeed: 'EBM/PDHM',
      preNanStrength: 'None',
      overfillFactor: 1,
      dosingWeight: 0,
      tfr: 0,
      feed: 0,
      ivm: 0,
      a: 0,
      l: 0,
      g: 0,
      na: 0,
      k: 0,
      ca: 0,
      mg: 0,
      dextrose5: 0,
      dextrose10: 0,
      dextrose25: 0,
      dextrose50: 0,
      n5: 0,
      n2: 0,
      ns: 0,
      dex10: 0,
      po4: 0,
      calciumViaTPN: 0
    }, { emitEvent: false });
    
    this.updateLabels();
    this.updateSodiumLabel();
    this.cd.detectChanges();
  }
}