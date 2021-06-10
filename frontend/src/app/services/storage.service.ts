import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  url: string = 'https://jannso.profipoint.pl:8228/tu/article';

  constructor(private http: HttpClient, private router: Router) { }

  getAllArticles() {
    return this.http.get(this.url);
  }

}
