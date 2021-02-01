import { TestBed } from '@angular/core/testing';

import { DoctoresService } from './doctores.service';

describe('DoctoresService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DoctoresService = TestBed.get(DoctoresService);
    expect(service).toBeTruthy();
  });
});
