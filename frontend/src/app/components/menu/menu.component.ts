import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  @Input() size: string;
  @Input() mobile: boolean;

  constructor(private auth: AuthService, public router: Router) { }

  ngOnInit(): void {
  }

  logout() {
    this.auth.logout();
  }

}
