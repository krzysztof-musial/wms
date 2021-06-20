import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { LocationsService } from 'src/app/services/locations.service';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.scss']
})
export class LocationsComponent implements OnInit {

  user;
  addLocationForm: FormGroup;
  locations;

  constructor(private ls: LocationsService, private fb: FormBuilder, private auth: AuthService) {
    this.user = this.auth.decodeToken();
    this.ls.getAllLocations().subscribe((locations) => {
      this.locations = locations;
    })
    this.addLocationForm = this.fb.group({
      // location_code: ['', [Validators.required] ]
      location_symbol: ['', [Validators.required] ],
      location_index: ['', [Validators.required] ]
    });
  }

  ngOnInit(): void {
  }

  addLocation(form) {
    let index;
    if (form.location_index < 10) {
      index = "00" + form.location_index;
    } else if (form.location_index < 100) {
      index = "0" + form.location_index;
    } else {
      index = form.location_index;
    }
    const location = {
      location_code: form.location_symbol + index
    }
    this.ls.addLocation(location).subscribe((data) => {
      this.ls.getAllLocations().subscribe((locations) => {
        this.locations = locations;
        this.addLocationForm.reset();
      })
    })
  }

}
