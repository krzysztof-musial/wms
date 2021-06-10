import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LocationsService {

  url: string = 'http://jannso.profipoint.pl:8228/tu/locations';

  constructor(private http: HttpClient, private router: Router) { }

  getAllLocations() {
    return this.http.get(this.url);
  }

  addLocation(location: any) {
    console.log(location);
    this.http.post<any>(this.url, location).subscribe({
      next: data => {
        console.log(data);
      },
      error: error => {
        console.error('There was an error!', error);
      }
    })
  }

}
