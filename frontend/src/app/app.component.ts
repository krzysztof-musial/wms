import { Component } from '@angular/core';
import {delay} from 'rxjs/operators';
import { trigger, transition, style, animate } from '@angular/animations';
import { AuthService } from './services/auth.service';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [
    trigger('fadeSlideInOut', [
      transition(':enter', [
        style({ transform: 'translateY(-50px)' }),
        animate('100ms', style({ transform: 'translateY(0)' })),
      ]),
      transition(':leave', [
        animate('300ms 500ms', style({ transform: 'translateY(-50px)' })),
      ]),
    ]),
  ]
})
export class AppComponent {
  
  loading: boolean = false;

  constructor(public auth: AuthService, private _loading: LoadingService) {}

  ngOnInit() {
    this.listenToLoading();
  }

  /**
   * Listen to the loadingSub property in the LoadingService class. This drives the
   * display of the loading spinner.
   */
  listenToLoading(): void {
  this._loading.loadingSub
    .pipe(delay(0)) // This prevents a ExpressionChangedAfterItHasBeenCheckedError for subsequent requests
    .subscribe((loading) => {
      this.loading = loading;
    });
}

}
