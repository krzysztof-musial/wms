import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  url: string = 'http://jannso.profipoint.pl:8228/tu/product';

  constructor(private http: HttpClient, private router: Router) { }

  getAllProducts() {
    return this.http.get(this.url);
  }

  addProduct(product: any) {
    console.log(product);
    this.http.post<any>(this.url, product).subscribe({
      next: data => {
        console.log(data);
      },
      error: error => {
        console.error('There was an error!', error);
      }
    })
  }

}
