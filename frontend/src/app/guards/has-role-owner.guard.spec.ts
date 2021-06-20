import { TestBed } from '@angular/core/testing';

import { HasRoleOwnerGuard } from './has-role-owner.guard';

describe('HasRoleOwnerGuard', () => {
  let guard: HasRoleOwnerGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(HasRoleOwnerGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
