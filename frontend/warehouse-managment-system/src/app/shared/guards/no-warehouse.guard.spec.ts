import { TestBed } from '@angular/core/testing';

import { NoWarehouseGuard } from './no-warehouse.guard';

describe('NoWarehouseGuard', () => {
  let guard: NoWarehouseGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(NoWarehouseGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
