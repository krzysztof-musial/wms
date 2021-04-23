import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-status',
  templateUrl: './status.component.html',
  styleUrls: ['./status.component.scss']
})
export class StatusComponent implements OnInit {

  version: string = 'v.0.1';
  date: string = '18.04.21';

  constructor(public auth: AuthService) { }

  ngOnInit(): void {}

  testEndpoint() {
    this.auth.test();
  }

}
