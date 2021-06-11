import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { WarehouseService } from 'src/app/services/warehouse.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  user;
  warehouse;

  constructor(private ws: WarehouseService, private auth: AuthService) { }

  ngOnInit(): void {
    this.user = this.auth.decodeToken();
    this.ws.getWarehouse().subscribe((data) => {
      this.warehouse = data;
    })
  }

}
