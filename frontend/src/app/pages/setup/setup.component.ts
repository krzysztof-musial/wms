import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { WarehouseService } from 'src/app/services/warehouse.service';

@Component({
  selector: 'app-setup',
  templateUrl: './setup.component.html',
  styleUrls: ['./setup.component.scss']
})
export class SetupComponent implements OnInit {

  createWarehouseForm: FormGroup;
  joinWarehouseForm: FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService, private ws: WarehouseService) {
    this.createWarehouseForm = this.fb.group({
      name: ['', [Validators.required] ]
    });
    this.joinWarehouseForm = this.fb.group({
      warehouseId: ['', [Validators.required] ]
    });
  }

  ngOnInit(): void {
  }

  createWarehouse(form: any): void {
    this.ws.createWarehouse(form);
  }

  joinWarehouse(form: any): void {
    this.ws.joinWarehouse(form);
  }

  refreshToken() {
    this.auth.refreshToken();
  }

  logout() {
    this.auth.logout();
  }

}
