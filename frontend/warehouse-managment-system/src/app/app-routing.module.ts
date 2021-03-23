import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  // {path: 'app', component: , children: [
  //   {path: 'setup', component: },
  //   {path: 'dashboard', component: },
  //   {path: 'settings', component: },
  //   {path: 'workers', component: },
  //   {path: 'storage', component: },
  //   {path: 'products', component: },
  //   {path: 'actions', component: },
  // ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
