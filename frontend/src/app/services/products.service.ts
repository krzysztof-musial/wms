import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  url: string = 'https://jannso.profipoint.pl:8228/tu/product';

  constructor(private http: HttpClient, private router: Router) { }

  getAllProducts() {
    return this.http.get(this.url);
  }

  addProduct(product: any) {
    return this.http.post<any>(this.url, product);
  }

}
