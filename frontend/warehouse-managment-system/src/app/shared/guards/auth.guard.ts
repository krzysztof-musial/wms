import { Injectable } from '@angular/core';
import { CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivateChild {

  constructor(private auth: AuthService, private router: Router) {}

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

      // working
      // if (!this.auth.authCheck()) {
      //   this.router.navigate(['/login']);
      //   return false;
      // }
      // return true;

      if (this.auth.authCheck()) {
        return true;
      }
      this.router.navigate(['/login']);
      return false;

  }
  
}
