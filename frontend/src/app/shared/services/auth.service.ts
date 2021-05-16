import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'moment';
import jwt_decode from 'jwt-decode';
import { IToken } from '../models/interfaces';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  url: string = 'https://warehousmanagementuserservice.azurewebsites.net/api';

  constructor(private http: HttpClient, private router: Router) {
    if (!this.authCheck) {
      this.logout();
    }
  }

  register(user: any): void {
    this.http.post<any>(this.url + '/user/register', user).subscribe({
      next: data => {
          this.router.navigate(['/login']);
      },
      error: error => {
          console.error('There was an error!', error);
      }
    })
  }

  login(user: any): void {
    this.http.post<any>(this.url + '/user/login', user).subscribe({
      next: data => {
          localStorage.setItem('token', data.data.token);
          this.router.navigate(['/app']);
      },
      error: error => {
          console.error('There was an error!', error);
      }
    })
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/']);
  }

  public authCheck(): boolean {
    if (localStorage.getItem('token')) {
      return moment().isBefore(this.decodeToken().exp * 1000);
    } else {
      return false;
    }
  }

  decodeToken() {
    let token: IToken = jwt_decode(localStorage.getItem('token'));
    return token;
  }

}
