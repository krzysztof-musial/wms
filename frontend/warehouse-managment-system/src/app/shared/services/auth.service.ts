import { Injectable } from '@angular/core';
import { IUser } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  user: IUser;
  isLoggedIn: Boolean;

  constructor() { }

  // Temporary:
  login(): void {
    this.isLoggedIn = true;
    this.user = {
      user_id: 1,
      warehouse_id: null,
      role_id: null,
      user_first_name: 'Jan',
      user_last_name: 'Nowak',
      user_email: 'jan@nowak.pl',
      user_is_deleted: false
    };
  }

  logout(): void {
    this.isLoggedIn = false;
    this.user = null;
  }

}
