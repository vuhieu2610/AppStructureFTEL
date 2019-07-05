import { TestBed } from '@angular/core/testing';

import { DeactivateGuardService } from './deactivate-guard.service';

describe('DeactivateGuardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DeactivateGuardService = TestBed.get(DeactivateGuardService);
    expect(service).toBeTruthy();
  });
});
