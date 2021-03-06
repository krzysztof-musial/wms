import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { HasRoleManagerGuard } from './guards/has-role-manager.guard';
import { HasWarehouseGuard } from './guards/has-warehouse.guard';
import { NoAuthGuard } from './guards/no-auth.guard';
import { NoWarehouseGuard } from './guards/no-warehouse.guard';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { SetupComponent } from './pages/setup/setup.component';
import { ActionsComponent } from './pages/warehouse/actions/actions.component';
import { ExportComponent } from './pages/warehouse/actions/export/export.component';
import { ImportComponent } from './pages/warehouse/actions/import/import.component';
import { SelectComponent } from './pages/warehouse/actions/select/select.component';
import { TransferComponent } from './pages/warehouse/actions/transfer/transfer.component';
import { DashboardComponent } from './pages/warehouse/dashboard/dashboard.component';
import { LocationsComponent } from './pages/warehouse/locations/locations.component';
import { ProductsComponent } from './pages/warehouse/products/products.component';
import { SettingsComponent } from './pages/warehouse/settings/settings.component';
import { StorageComponent } from './pages/warehouse/storage/storage.component';
import { WarehouseComponent } from './pages/warehouse/warehouse.component';
import { WorkersComponent } from './pages/warehouse/workers/workers.component';

const routes: Routes = [
  {path: '', component: HomeComponent, canActivate: [NoAuthGuard]},
  {path: 'login', component: LoginComponent, canActivate: [NoAuthGuard]},
  {path: 'register', component: RegisterComponent, canActivate: [NoAuthGuard]},
  // Auth pages
  {path: 'warehouse', component: WarehouseComponent, canActivateChild: [AuthGuard, HasWarehouseGuard], children: [
    {path: '', component: DashboardComponent},
    {path: 'settings', component: SettingsComponent},
    {path: 'workers', component: WorkersComponent},
    {path: 'storage', component: StorageComponent},
    {path: 'locations', component: LocationsComponent},
    {path: 'products', component: ProductsComponent},
    {path: 'actions', component: ActionsComponent, children: [
      {path: '', component: SelectComponent},
      {path: 'import', component: ImportComponent, canActivate: [HasRoleManagerGuard]},
      {path: 'transfer', component: TransferComponent},
      {path: 'export', component: ExportComponent, canActivate: [HasRoleManagerGuard]}
    ]},
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
