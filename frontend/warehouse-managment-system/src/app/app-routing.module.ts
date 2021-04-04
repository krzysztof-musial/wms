import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApplicationComponent } from './application/application.component';
import { DashboardComponent } from './application/dashboard/dashboard.component';
import { SetupComponent } from './application/setup/setup.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'app', component: ApplicationComponent, canActivateChild: [AuthGuard], children: [
    {path: '', component: DashboardComponent},
    //   {path: 'settings', component: },
    //   {path: 'workers', component: },
    //   {path: 'storage', component: },
    //   {path: 'products', component: },
    //   {path: 'actions', component: },
    // Special route for wiring up your account with warehouse
    {path: 'setup', component: SetupComponent},
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
