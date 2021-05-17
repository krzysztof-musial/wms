import { trigger, transition, style, animate } from '@angular/animations';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/shared/services/auth.service';
import { DataService } from 'src/app/shared/services/data.service';

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.scss'],
  animations: [
    trigger('slideInOut', [
      transition(':enter', [
        style({ transform: 'translateX(-100%)' }),
        animate('.5s cubic-bezier(0.33, 1, 0.68, 1)', style({ transform: 'translateX(0)' }))
      ]),
      transition(':leave', [
        style({ transform: 'translateX(0)' }),
        animate('.5s cubic-bezier(0.33, 1, 0.68, 1)', style({ transform: 'translateX(-100%)' }))
      ])
    ])
  ],
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
