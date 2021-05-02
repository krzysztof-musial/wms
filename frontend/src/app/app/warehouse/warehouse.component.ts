import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/shared/services/auth.service';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.scss']
})
export class WarehouseComponent implements OnInit, OnDestroy {

  title: string;
  subscription: Subscription;
  menuMobile: boolean = false;
  menuShrinked: boolean = false;

  constructor(private data: DataService, private auth: AuthService) { }

  ngOnInit(): void {
    this.subscription = this.data.currentTitle.subscribe((title) => this.title = title);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  triggerMenuMobile() {
    this.menuMobile = !this.menuMobile;
  }

  triggerMenu() {
    this.menuShrinked = !this.menuShrinked;
  }

  logout() {
    this.auth.logout();
  }

}
