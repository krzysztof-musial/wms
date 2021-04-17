import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IToken, IUserLogin, IUserRegister } from '../models/interfaces';
import jwt_decode from 'jwt-decode';
import * as moment from 'moment';
import { Router } from '@angular/router';

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

  register(user: IUserRegister): void {
    this.http.post<any>(this.url + '/user/register', user).subscribe({
      next: data => {
          console.log(data);
          this.router.navigate(['/login']);
      },
      error: error => {
          console.error('There was an error!', error);
      }
    })
  }

  login(user: IUserLogin): void {
    this.http.post<any>(this.url + '/user/login', user).subscribe({
      next: data => {
          console.log(data);
          this.setUser(data.token);
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

  private setUser(jwt: any) {
    let token: IToken = jwt_decode(jwt);
    localStorage.setItem('token', jwt);
    localStorage.setItem("exp", JSON.stringify(token.exp.valueOf()));
    localStorage.setItem("iat", JSON.stringify(token.iat.valueOf()));
    localStorage.setItem("nbf", JSON.stringify(token.nbf.valueOf()));
    localStorage.setItem('userId', token.userId);
    localStorage.setItem('email', token.userEmail);
  }

  public authCheck(): boolean {
    let expirationDate = JSON.parse(localStorage.getItem("exp")) * 1000;
    return moment().isBefore(expirationDate);
  }

  // Test endpoint
  public test() {
    this.http.get<any>(this.url + '/values').subscribe({
      next: data => {
          console.log(data);
      },
      error: error => {
          console.error('There was an error!', error);
      }
    })
  }

}
