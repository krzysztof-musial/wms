import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
// Components
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { StatusComponent } from './shared/components/status/status.component';
import { LogoComponent } from './shared/components/logo/logo.component';
import { ApplicationComponent } from './application/application.component';
import { SetupComponent } from './application/setup/setup.component';
import { DashboardComponent } from './application/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    StatusComponent,
    LogoComponent,
    ApplicationComponent,
    SetupComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
