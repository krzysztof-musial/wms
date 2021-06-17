import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LocationsService } from 'src/app/services/locations.service';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-transfer',
  templateUrl: './transfer.component.html',
  styleUrls: ['./transfer.component.scss']
})
export class TransferComponent implements OnInit {

  transferForm: FormGroup;
  locations;
  products;

  constructor(private fb: FormBuilder, private ls: LocationsService, private ps: ProductsService) {
    this.transferForm = this.fb.group({
      product: ['', [Validators.required] ],
      amount: ['', [Validators.required] ],
      locationFrom: ['', [Validators.required] ],
      locationTo: ['', [Validators.required] ]
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

  transfer(form) {
    console.log(form);
  }

}
