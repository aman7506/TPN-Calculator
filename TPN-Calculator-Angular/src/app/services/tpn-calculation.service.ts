import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TpnCalculationService {

  calculateLipid(dosingWeight: number, lipidReq: number): number {
    return 5 * lipidReq * dosingWeight; 
  }

  calculateMVI(lipidReq: number, dosingWeight: number): number {
    return lipidReq > 0 ? dosingWeight : 0; 
  }

  calculateCelcel(celcelInput: number, dosingWeight: number): number {
    return celcelInput ? 0.5 * dosingWeight : 0;
  }

  calculateAminoven(dosingWeight: number, proteinReq: number): number {
    return 10 * dosingWeight * proteinReq;
  }

  calculateSodium(
    dosingWeight: number,
    sodiumReq: number,
    sodiumSource: string,
    naInIvm: number
  ): number {
    const adjustedNa = sodiumReq - naInIvm;
    return sodiumSource === 'CRL' 
      ? (adjustedNa * dosingWeight) / 3
      : adjustedNa * dosingWeight * 2;
  }
  calculatePotassium(dw: number, kReq: number, potPhos: number): number {
    const kFromPO4 = (4.4 * potPhos) / dw;
    const adjustedK = kReq - kFromPO4;
    const clinicalK = Math.max(0, adjustedK);
    return (clinicalK * dw) / 2;
  }
  calculateCalcium(
    dosingWeight: number,
    calciumReq: number,
    calciumViaTPN: boolean
  ): number {
    return calciumViaTPN ? (dosingWeight * calciumReq) / 9.3 : 0;
  }

  calculateMagnesium(dosingWeight: number, magnesiumReq: number): number {
    return (magnesiumReq * dosingWeight) / 4;
  }

  calculateBaseDextrose(
    tpnGlucose: number,
    fluidForGlucose: number,
    use5Percent: boolean,
    use10Percent: boolean
  ): number {
    const baseConc = use5Percent ? 0.05 : 0.10;
    return Math.min(fluidForGlucose, tpnGlucose / baseConc);
  }


 calculateDextroseVolumes(params: {
  tpnGlucose: number,
  fluidForGlucose: number,
  dextrose5: boolean,
  dextrose10: boolean,
  dextrose25: boolean,
  dextrose50: boolean,
  dosingWeight: number,
  dextroseBase: number,
  dextroseConc: number
}): { baseVolume: number, concVolume: number } {
  const L8 = params.tpnGlucose;
  const L9 = params.fluidForGlucose;
  const F13 = params.dextrose5 ? 1 : 0;
  const F14 = params.dextrose10 ? 1 : 0;
  const F15 = params.dextrose25 ? 1 : 0;
  const F16 = params.dextrose50 ? 1 : 0;

  let baseVolume = 0;
  let concVolume = 0;

  if (params.dextrose5 || params.dextrose10) {
    const numerator = (5 * L9 * F16) + (2.5 * L9 * F15) - (10 * L8);
    const denominator = (5 * F16) + (2.5 * F15) - (0.5 * F13 + F14);
    baseVolume = denominator !== 0 ? numerator / denominator : 0;
    baseVolume = Math.max(0, Math.min(baseVolume, L9));
  }

  if (params.dextrose25 || params.dextrose50) {
    const remainingGlucose = L8 - (baseVolume * (params.dextrose5 ? 0.05 : 0.10));
    const availableFluid = L9 - baseVolume;
    
    if (params.dextrose25) {
      concVolume = ((0.1 * L9 * F14) + (0.05 * L9 * F13)) > L8 
        ? 0 
        : remainingGlucose / 0.25;
    } else {
      concVolume = ((0.1 * L9 * F14) + (0.05 * L9 * F13)) > L8 
        ? 0 
        : remainingGlucose / 0.50;
    }
    concVolume = Math.max(0, Math.min(concVolume, availableFluid));
  }

  return {
    baseVolume: this.safeRound(baseVolume),
    concVolume: this.safeRound(concVolume)
  };
}


  calculateSyringe1Rate(
    lipid: number,
    mvi: number,
    celcel: number
  ): number {
    return (lipid + mvi + celcel) / 24;
  }

  calculateComponentRate(
    componentVolume: number,
    totalVolume: number,
    syringe2Rate: number
  ): number {
    return (componentVolume / totalVolume) * syringe2Rate;
  }

  calculateTFV(dosingWeight: number, tfr: number): number {
    return dosingWeight * tfr;
  }

  calculateFeeds(dosingWeight: number, feedRate: number): number {
    return dosingWeight * feedRate;
  }

  calculateIVFmlKg(tfr: number, feedRate: number): number {
    return tfr - feedRate;
  }

  calculateIVFml(dosingWeight: number, ivfMlKg: number): number {
    return dosingWeight * ivfMlKg;
  }

  calculateTPNFluid(tfv: number, ivm: number): number {
    return tfv - ivm;
  }

  calculateTPNGlucose(gir: number, dosingWeight: number, glucoseInIvm: number): number {
    return (gir * dosingWeight * 1.44) - glucoseInIvm;
  }

  calculateFluidForGlucose(
    tpnFluid: number,
    lipid: number,
    aminoven: number,
    nacl: number,
    kcl: number,
    calcium: number,
    mvi: number,
    celcel: number
  ): number {
    return tpnFluid - lipid - aminoven - nacl - kcl - calcium - mvi - celcel;
  }
  calculateOsmolarity(
    lipid: number,
    aminoven: number,
    nacl: number,
    kcl: number,
    calcium: number,
    mgso4: number
  ): number {
    const totalComponents = lipid + aminoven + nacl + kcl + calcium + mgso4;
    if (totalComponents === 0) return 0;

    const osmolarity = (
      (0.26 * lipid) +
      (0.885 * aminoven) +
      (1.027 * nacl) +
      (4 * kcl) +
      (0.555 * calcium) +
      (2.78 * mgso4)
    ) / totalComponents * 1000;

    return this.safeRound(osmolarity);
  }


  calculateDextrosePercentage(tpnGlucose: number, totalVolume: number): number {
    return (tpnGlucose / totalVolume) * 100;
  }

  calculateCNR(gir: number, lipid: number, protein: number): number {
    return protein > 0 ? (6.25 * ((4.9 * gir) + (9 * lipid))) / protein : 0;
  }

  calculateCalories(
    protein: number,
    lipid: number,
    gir: number,  
    feed: number,
    feedType: string,
    preNanStrength: string
  ): number {
    let feedCalories = 0;
    if (feedType === 'EBM/PDHM') feedCalories = feed * 0.52;
    if (feedType === 'Formula') feedCalories = feed * 0.78;

    let preNanCalories = 0;
    switch (preNanStrength) {
      case 'Quarter': preNanCalories = feed * 0.04; break;
      case 'Half': preNanCalories = feed * 0.08; break;
      case 'Full': preNanCalories = feed * 0.16; break;
    }

    return (protein * 4) + (lipid * 9) + (gir * 5) + feedCalories + preNanCalories;
  }

  calculateProteins(a: number, feed: number, 
    feedType: string, preNanStrength: string): number {
let feedProt = 0;
let preNanProt = 0;

if (feedType === 'EBM/PDHM') feedProt = feed * 0.0095;
if (feedType === 'Formula') feedProt = feed * 0.019;

switch (preNanStrength) {
case 'Quarter': preNanProt = feed * 0.003; break;
case 'Half': preNanProt = feed * 0.006; break;
case 'Full': preNanProt = feed * 0.012; break;
}

return this.safeRound(a + feedProt + preNanProt);
}

  calculateNaInIvm(n5: number, n2: number, ns: number, dosingWeight: number): number {
    return ((n5 * 0.031) + (n2 * 0.077) + (ns * 0.154)) / dosingWeight;
  }

  calculateGlucoseInIvm(dex10: number): number {
    return dex10 * 0.1;
  }

  calculateKInPotphos(potPhos: number, dosingWeight: number): number {
    return (4.4 * potPhos) / dosingWeight;
  }

  calculatePotPhos(dosingWeight: number, po4Req: number): number {
    return (po4Req * dosingWeight) / 93;
  }

  safeRound(value: number, decimals: number = 2): number {
    return Number(Math.round(Number(value + 'e' + decimals)) + 'e-' + decimals);
  }

  validateIVM(ivmTotal: number, enteredIvm: number): string {
    return ivmTotal > enteredIvm ? 'Wrong IVM volume' : '';
  }
} 