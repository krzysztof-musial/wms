import { Component } from '@angular/core';
import { AuthService } from './shared/services/auth.service';
import { LoadingService } from './shared/services/loading.service';
import {delay} from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
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
