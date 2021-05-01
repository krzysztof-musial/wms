import { Component, OnDestroy, OnInit } from '@angular/core';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  title: string = 'Dashboard';

  constructor(private data: DataService) {
    this.data.changeTitle(this.title);
  }

  ngOnInit(): void {}

}
