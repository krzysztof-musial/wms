import { Component, OnInit } from '@angular/core';
import { IResponse } from 'src/app/models/interfaces';
import { WarehouseService } from 'src/app/services/warehouse.service';

@Component({
  selector: 'app-workers',
  templateUrl: './workers.component.html',
  styleUrls: ['./workers.component.scss']
})
export class WorkersComponent implements OnInit {

  workers;
  candidates;

  constructor(private ws: WarehouseService) { }

  ngOnInit(): void {
    this.ws.warehouseMembers().subscribe((data) => {
      this.workers = data;
    })
    this.ws.getCandidates().subscribe((data: IResponse) => {
      this.candidates = data.data;
    })
  }

  acceptCandidate(id) {
    this.ws.acceptCandidate(id);
  }

  declineCandidate(id) {
    this.ws.declineCandidate(id);
  }

}
