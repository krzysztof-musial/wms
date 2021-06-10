import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductsService } from 'src/app/services/products.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  addProductForm: FormGroup;
  products;

  constructor(private ps: ProductsService, private fb: FormBuilder) {
    this.ps.getAllProducts().subscribe((products) => {
      this.products = products;
    })
    this.addProductForm = this.fb.group({
      product_name: ['', [Validators.required] ],
      product_code: ['', [Validators.required] ],
      product_description: ['', [Validators.required] ]
    });
  }

  ngOnInit(): void {
  }

  addProduct(form) {
    this.ps.addProduct(form);
  }

}
