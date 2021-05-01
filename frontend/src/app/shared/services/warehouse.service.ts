import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class WarehouseService {

  url: string = 'https://warehousmanagementuserservice.azurewebsites.net/api';

  constructor(private http: HttpClient, private router: Router) { }

  createWarehouse(warehouse: any) {
    this.http.post<any>(this.url + '/warehouses', warehouse).subscribe({
      next: data => {
          // console.log(data);
          this.router.navigate(['/warehouse']);
      },
      error: error => {
          console.error('There was an error!', error);
      }
    })
  }

}
