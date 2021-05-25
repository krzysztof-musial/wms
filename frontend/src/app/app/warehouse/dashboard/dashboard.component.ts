import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthService } from 'src/app/shared/services/auth.service';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  title: string = 'Dashboard';
  name: string = '';

  constructor(private data: DataService, private auth: AuthService) {
    this.data.changeTitle(this.title);
  }

  ngOnInit(): void {
    this.name = this.auth.decodeToken().userFirstName + ' ' + this.auth.decodeToken().userLastName;
  }

}
