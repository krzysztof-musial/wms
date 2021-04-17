import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.scss']
})
export class StatusComponent implements OnInit {

  date: string = '04.04.21';
  username: string = localStorage.getItem("username");
  // Test endpoint
  test: any;

  constructor(public auth: AuthService) { }

  ngOnInit(): void {
  }

  testEndpoint() {
    this.auth.test();
  }

}
