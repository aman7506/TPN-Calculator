import { TestBed } from '@angular/core/testing';

import { TpnCalculationService } from './tpn-calculation.service';

describe('TpnCalculationService', () => {
  let service: TpnCalculationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TpnCalculationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
