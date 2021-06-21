import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActionsService } from 'src/app/services/actions.service';
import { LocationsService } from 'src/app/services/locations.service';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.scss']
})
export class ImportComponent implements OnInit {

  importForm: FormGroup;
  locations;
  products;

  constructor(private fb: FormBuilder, private ls: LocationsService, private ps: ProductsService, private as: ActionsService) {
    this.importForm = this.fb.group({
      product: ['', [Validators.required] ],
      amount: ['', [Validators.required] ],
      location: ['', [Validators.required] ]
    });
    this.ls.getAllLocations().subscribe((locations) => {
      this.locations = locations;
    })
    this.ps.getAllProducts().subscribe((products) => {
      this.products = products;
    })
  }

  ngOnInit(): void {
  }

  import(form) {
    console.log(form);
    this.as.import(form).subscribe((data) => {
      console.log(data);
    })
  }

}
