import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/shared/services/auth.service';
import { WarehouseService } from 'src/app/shared/services/warehouse.service';

@Component({
  selector: 'app-setup',
  templateUrl: './setup.component.html',
  styleUrls: ['./setup.component.scss']
})
export class SetupComponent implements OnInit {

  page: number = 1;
  createWarehouseForm: FormGroup;

  constructor(private fb: FormBuilder, private ws: WarehouseService, private auth: AuthService) {
    this.createWarehouseForm = this.fb.group({
      name: ['', [Validators.required] ]
    });
  }

  ngOnInit(): void {
  }

  createWarehouse(form: any): void {
    console.log(form);
    this.ws.createWarehouse(form);
    // this.loginForm.reset();
  }

  logout() {
    this.auth.logout();
  }

}
