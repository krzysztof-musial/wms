import { TestBed } from '@angular/core/testing';

import { HasRoleManagerGuard } from './has-role-manager.guard';

describe('HasRoleManagerGuard', () => {
  let guard: HasRoleManagerGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(HasRoleManagerGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
