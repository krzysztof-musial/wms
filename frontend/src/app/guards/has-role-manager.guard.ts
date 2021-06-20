import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class HasRoleManagerGuard implements CanActivate, CanActivateChild {

  constructor(private auth: AuthService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

      if (this.auth.decodeToken().role === 'Manager' || this.auth.decodeToken().role === 'Owner') {
        return true;
      }
      alert('Brak uprawnień')
      this.router.navigate(['/warehouse/actions']);
      return false;

  }
  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
      if (this.auth.decodeToken().role === 'Manager' || this.auth.decodeToken().role === 'Owner') {
        return true;
      }
      alert('Brak uprawnień')
      this.router.navigate(['/warehouse/actions']);
      return false;

  }
  
}
