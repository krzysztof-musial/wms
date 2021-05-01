import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// Guards
import { AuthGuard } from './shared/guards/auth.guard';
import { HasWarehouseGuard } from './shared/guards/has-warehouse.guard';
import { NoWarehouseGuard } from './shared/guards/no-warehouse.guard';
// Components
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SetupComponent } from './app/setup/setup.component';
import { WarehouseComponent } from './app/warehouse/warehouse.component';
import { DashboardComponent } from './app/warehouse/dashboard/dashboard.component';
import { SettingsComponent } from './app/warehouse/settings/settings.component';
import { ActionsComponent } from './app/warehouse/actions/actions.component';
import { ProductsComponent } from './app/warehouse/products/products.component';
import { StorageComponent } from './app/warehouse/storage/storage.component';
import { WorkersComponent } from './app/warehouse/workers/workers.component';
import { NoAuthGuard } from './shared/guards/no-auth.guard';

const routes: Routes = [
  {path: '', component: HomeComponent, canActivate: [NoAuthGuard]},
  {path: 'login', component: LoginComponent, canActivate: [NoAuthGuard]},
  {path: 'register', component: RegisterComponent, canActivate: [NoAuthGuard]},
  // Auth routes
  {path: 'warehouse', component: WarehouseComponent, canActivateChild: [AuthGuard, HasWarehouseGuard], children: [
    {path: '', component: DashboardComponent},
    {path: 'settings', component: SettingsComponent},
    {path: 'workers', component: WorkersComponent},
    {path: 'storage', component: StorageComponent},
    {path: 'products', component: ProductsComponent},
    {path: 'actions', component: ActionsComponent},
  ]},
  {path: 'setup', component: SetupComponent, canActivate: [AuthGuard, NoWarehouseGuard]},
  // 404
  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
