import { CommonModule } from '@angular/common';
import { Component, NgModule } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { SingleCardModule } from './layouts';

@Component({
  selector: 'app-unauthenticated-content',
  template: `
    <app-single-card [title]="title" [description]="description">
      <router-outlet></router-outlet>
    </app-single-card>
  `,
  styles: [`
    :host {
      width: 100%;
      height: 100%;
    }
  `],
})
export class UnauthenticatedContentComponent {

  constructor(private router: Router) {
  }

  get title() {
    const path = this.router.url.split('/')[1];
    switch (path) {
      case 'login-form':
        return 'Вход';
      case 'reset-password':
        return 'Сброс пароля';
      case 'create-account':
        return 'Регистрация';
      case 'change-password':
        return 'Смена пароля';
      default:
        return '';
    }
  }

  get description() {
    const path = this.router.url.split('/')[1];
    switch (path) {
      case 'reset-password':
        return 'Пожалуйста, введите свой Email, который вы указывали при регистрации. На него мы вышлем Вам письмо с дальнешими инструкциями';
      default:
        return '';
    }
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    SingleCardModule,
  ],
  declarations: [UnauthenticatedContentComponent],
  exports: [UnauthenticatedContentComponent],
})
export class UnauthenticatedContentModule {
}
