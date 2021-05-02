import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  title: string = 'Products';

  constructor(private data: DataService) {
    this.data.changeTitle(this.title);
  }

  ngOnInit(): void {}

}
