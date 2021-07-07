import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ActionsService {

  url: string = 'https://jannso.profipoint.pl:8282/tam/';

  constructor(private http: HttpClient, private router: Router) { }

  import(data: any) {
    return this.http.post<any>(this.url + 'import', data);
  }

  transfer(data: any) {
    return this.http.post<any>(this.url + 'move', data);
  }

  export(data: any) {
    return this.http.post<any>(this.url + 'export', data);
  }
  
}
