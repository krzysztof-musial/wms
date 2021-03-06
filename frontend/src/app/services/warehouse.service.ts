import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  url: string = 'https://warehousmanagementuserservice.azurewebsites.net/api';

  constructor(private http: HttpClient, private router: Router, private auth: AuthService) { }

  createWarehouse(warehouse: any) {
    this.http.post<any>(this.url + '/warehouses', warehouse).subscribe({
      next: data => {
        this.auth.refreshToken();
      },
      error: error => {
        console.error('There was an error!', error);
        this.auth.refreshToken();
      }
    })
  }

  joinWarehouse(warehouse: any) {
    this.http.post<any>(this.url + '/invitation', warehouse).subscribe({
      next: data => {
        this.auth.refreshToken();
      },
      error: error => {
        console.error('There was an error!', error);
        this.auth.refreshToken();
      }
    })
  }

  getWarehouse() {
    return this.http.get(this.url + '/warehouses/' + this.auth.decodeToken().warehouseId);
  }

  warehouseMembers() {
    return this.http.get(this.url + '/warehouses/getwarehousemembers/' + this.auth.decodeToken().warehouseId);
  }

  getCandidates() {
    return this.http.get(this.url + '/invitation/getallwarehouseactiveinvitations/');
  }

  acceptCandidate(id) {
    const candidate = {
      Id: id
    }
    return this.http.post<any>(this.url + '/invitation/approveinvitation/', candidate);
  }

  declineCandidate(id) {
    const candidate = {
      Id: id
    }
    return this.http.post<any>(this.url + '/invitation/declineinvitation/', candidate);
  }

  changeRole(worker: any) {
    return this.http.post(this.url + '/role/AssignRole', worker);
  }

}