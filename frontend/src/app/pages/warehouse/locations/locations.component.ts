import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LocationsService } from 'src/app/services/locations.service';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.scss']
})
export class LocationsComponent implements OnInit {

  addLocationForm: FormGroup;
  locations;

  constructor(private ls: LocationsService, private fb: FormBuilder) {
    this.ls.getAllLocations().subscribe((locations) => {
      this.locations = locations;
    })
    this.addLocationForm = this.fb.group({
      location_code: ['', [Validators.required] ]
    });
  }

  ngOnInit(): void {
  }

  addLocation(form) {
    this.ls.addLocation(form);
  }

}
