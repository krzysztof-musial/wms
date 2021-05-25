import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// Components
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LogoComponent } from './shared/components/logo/logo.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { WarehouseComponent } from './app/warehouse/warehouse.component';
import { SetupComponent } from './app/setup/setup.component';
import { DashboardComponent } from './app/warehouse/dashboard/dashboard.component';
import { IconComponent } from './shared/components/icon/icon.component';
import { SettingsComponent } from './app/warehouse/settings/settings.component';
import { WorkersComponent } from './app/warehouse/workers/workers.component';
import { StorageComponent } from './app/warehouse/storage/storage.component';
import { ProductsComponent } from './app/warehouse/products/products.component';
import { ActionsComponent } from './app/warehouse/actions/actions.component';
// Other
import { AuthInterceptor } from './shared/interceptors/auth.interceptor';
import { AuthGuard } from './shared/guards/auth.guard';
import { NoAuthGuard } from './shared/guards/no-auth.guard';
import { HasWarehouseGuard } from './shared/guards/has-warehouse.guard';
import { NoWarehouseGuard } from './shared/guards/no-warehouse.guard';
import { HttpRequestInterceptor } from './shared/interceptors/http-request.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LogoComponent,
    LoginComponent,
    RegisterComponent,
    WarehouseComponent,
    SetupComponent,
    DashboardComponent,
    IconComponent,
    SettingsComponent,
    WorkersComponent,
    StorageComponent,
    ProductsComponent,
    ActionsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: HttpRequestInterceptor, multi: true},
    AuthGuard,
    NoAuthGuard,
    HasWarehouseGuard,
    NoWarehouseGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
