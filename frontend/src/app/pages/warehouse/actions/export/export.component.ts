import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActionsService } from 'src/app/services/actions.service';
import { LocationsService } from 'src/app/services/locations.service';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-export',
  templateUrl: './export.component.html',
  styleUrls: ['./export.component.scss']
})
export class ExportComponent implements OnInit {

  exportForm: FormGroup;
  locations;
  products;

  constructor(private fb: FormBuilder, private ls: LocationsService, private ps: ProductsService, private as: ActionsService) {
    this.exportForm = this.fb.group({
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

  export(form) {
    console.log(form);
    this.as.export(form).subscribe((data) => {
      console.log(data);
    })
  }

}
