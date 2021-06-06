import { trigger, transition, style, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { WarehouseService } from 'src/app/services/warehouse.service';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.scss'],
  animations: [
    trigger('slideInOut', [
      transition(':enter', [
        style({ transform: 'translateX(-100%)' }),
        animate('.5s cubic-bezier(0.33, 1, 0.68, 1)', style({ transform: 'translateX(0)' }))
      ]),
      transition(':leave', [
        style({ transform: 'translateX(0)' }),
        animate('.5s cubic-bezier(0.33, 1, 0.68, 1)', style({ transform: 'translateX(-100%)' }))
      ])
    ])
  ],
})
export class WarehouseComponent implements OnInit {

  open: boolean = true;
  size: string = 'big';
  warehouse;
  user;

  constructor(private ws: WarehouseService, private auth: AuthService) { }

  ngOnInit(): void {
    this.ws.getWarehouse().subscribe((data) => {
      this.warehouse = data;
    })
    this.user = this.auth.decodeToken();
  }

  // For mobile
  toggleMenu() {
    this.open = !this.open;
  }
  // For desktop
  toggleSize() {
    if (this.size === 'big')
      this.size = 'small'
    else 
      this.size = 'big'
  }

}
