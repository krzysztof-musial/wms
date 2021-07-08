import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IResponse } from 'src/app/models/interfaces';
import { AuthService } from 'src/app/services/auth.service';
import { WarehouseService } from 'src/app/services/warehouse.service';

@Component({
  selector: 'app-workers',
  templateUrl: './workers.component.html',
  styleUrls: ['./workers.component.scss']
})
export class WorkersComponent implements OnInit {

  workers;
  ownersCount;
  managersCount;
  workersCount;
  candidates;
  user;
  changeRoleForm: FormGroup;

  constructor(private ws: WarehouseService, private auth: AuthService, private fb: FormBuilder) {
    this.user = this.auth.decodeToken();
    this.changeRoleForm = this.fb.group({
      role: ['', [Validators.required] ]
    });
  }

  ngOnInit(): void {
    this.ws.warehouseMembers().subscribe((data) => {
      this.workers = data;
      this.counter();
    })
    this.ws.getCandidates().subscribe((data: IResponse) => {
      this.candidates = data.data;
      console.log(this.candidates)
    })
  }

  acceptCandidate(id) {
    this.ws.acceptCandidate(id).subscribe((data) => {
      this.ws.warehouseMembers().subscribe((data) => {
        this.workers = data;
        this.counter();
      })
      this.ws.getCandidates().subscribe((data: IResponse) => {
        this.candidates = data.data;
      })
    });
  }

  declineCandidate(id) {
    this.ws.declineCandidate(id).subscribe((data) => {
      this.ws.warehouseMembers().subscribe((data) => {
        this.workers = data;
        this.counter();
      })
      this.ws.getCandidates().subscribe((data: IResponse) => {
        this.candidates = data.data;
      })
    });
  }

  changeRole(id, role) {
    const newRole = {
      userId: id,
      role: role.role
    }
    this.ws.changeRole(newRole).subscribe((data) => {
      this.ws.warehouseMembers().subscribe((data) => {
        this.workers = data;
        this.counter();
        // this.auth.refreshToken();
        // this.user = this.auth.decodeToken();
      })
    });
  }

  counter() {
    this.workersCount = 0;
    this.managersCount = 0;
    this.ownersCount = 0;
    this.workers.forEach(worker => {
      if (worker.role === 0) {
        this.workersCount++
      } else if (worker.role === 1) {
        this.managersCount++
      } else if (worker.role === 2) {
        this.ownersCount++
      }
    });
  }

}
