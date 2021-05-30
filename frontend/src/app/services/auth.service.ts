import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
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

  login(user: any): void {
    this.http.post<any>(this.url + '/user/login', user).subscribe({
      next: response => {
        localStorage.setItem('token', response.data.token);
        localStorage.setItem('refreshToken', response.data.refreshToken.token);
        this.router.navigate(['/app']);
      },
      error: error => {
        console.error('There was an error!', error);
      }
    })
  }

  register(user: any): void {
    this.http.post<any>(this.url + '/user/register', user).subscribe({
      next: data => {
        console.log(data);
          // this.router.navigate(['/login']);
          this.login(user);
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

  refreshToken() {
    const temp = {
      refreshToken: localStorage.getItem('refreshToken'),
      accessToken: localStorage.getItem('token'),
    }
    this.http.post<any>(this.url + '/user/refreshtoken', temp).subscribe({
      next: response => {
        // console.log(response);
        localStorage.setItem('token', response.data.token);
        localStorage.setItem('refreshToken', response.data.refreshToken.token);
        this.router.navigate(['/warehouse']);
      },
      error: error => {
        console.error('There was an error!', error);
        // this.router.navigate(['/warehouse']);
      }
    })
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
