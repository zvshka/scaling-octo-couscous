import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  ChangePasswordFormComponent,
  CreateAccountFormComponent,
  LoginFormComponent,
  ResetPasswordFormComponent,
} from './shared/components';
import { AuthGuardService } from './shared/services';
import { HomeComponent } from './pages/home/home.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { DevicesComponent } from './pages/devices/devices.component';
import { DxButtonModule, DxDataGridModule, DxFormModule, DxToolbarModule } from 'devextreme-angular';
import { NgForOf } from '@angular/common';
import { MeasurementsComponent } from './shared/components/measurements/measurements.component';

const routes: Routes = [
  {
    path: 'devices',
    component: DevicesComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'profile',
    component: ProfileComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'home',
    component: HomeComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'login-form',
    component: LoginFormComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'reset-password',
    component: ResetPasswordFormComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'create-account',
    component: CreateAccountFormComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'change-password/:recoveryCode',
    component: ChangePasswordFormComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: '**',
    redirectTo: 'home',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: false}), DxDataGridModule, DxFormModule, DxToolbarModule, NgForOf, DxButtonModule, MeasurementsComponent],
  providers: [AuthGuardService],
  exports: [RouterModule],
  declarations: [
    HomeComponent,
    ProfileComponent,
    DevicesComponent,
  ],
})
export class AppRoutingModule {
}
