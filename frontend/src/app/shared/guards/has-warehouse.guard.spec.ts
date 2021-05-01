import { TestBed } from '@angular/core/testing';

import { HasWarehouseGuard } from './has-warehouse.guard';

describe('HasWarehouseGuard', () => {
  let guard: HasWarehouseGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(HasWarehouseGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
