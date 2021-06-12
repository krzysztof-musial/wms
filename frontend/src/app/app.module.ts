import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { SvgComponent } from './components/svg/svg.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { WarehouseComponent } from './pages/warehouse/warehouse.component';
import { DashboardComponent } from './pages/warehouse/dashboard/dashboard.component';
import { SettingsComponent } from './pages/warehouse/settings/settings.component';
import { WorkersComponent } from './pages/warehouse/workers/workers.component';
import { StorageComponent } from './pages/warehouse/storage/storage.component';
import { ProductsComponent } from './pages/warehouse/products/products.component';
import { ActionsComponent } from './pages/warehouse/actions/actions.component';
import { SetupComponent } from './pages/setup/setup.component';
import { AuthGuard } from './guards/auth.guard';
import { HasWarehouseGuard } from './guards/has-warehouse.guard';
import { NoAuthGuard } from './guards/no-auth.guard';
import { NoWarehouseGuard } from './guards/no-warehouse.guard';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { HttpRequestInterceptor } from './interceptors/http-request.interceptor';
import { MenuComponent } from './components/menu/menu.component';
import { LocationsComponent } from './pages/warehouse/locations/locations.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SvgComponent,
    LoginComponent,
    RegisterComponent,
    WarehouseComponent,
    DashboardComponent,
    SettingsComponent,
    WorkersComponent,
    StorageComponent,
    ProductsComponent,
    ActionsComponent,
    SetupComponent,
    MenuComponent,
    LocationsComponent
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
