import { Component, EventEmitter, Input, NgModule, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthService, IUser } from '../../services';
import { UserPanelModule } from '../user-panel/user-panel.component';
import { DxButtonModule } from 'devextreme-angular/ui/button';
import { DxToolbarModule } from 'devextreme-angular/ui/toolbar';

import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: 'header.component.html',
  styleUrls: ['./header.component.scss'],
})

export class HeaderComponent implements OnInit {
  @Output()
  menuToggle = new EventEmitter<boolean>();

  @Input()
  menuToggleEnabled = false;

  @Input()
  title!: string;

  user: IUser | null = {username: '', email: ''};

  userMenuItems = [{
    text: 'Профиль',
    icon: 'user',
    onClick: () => {
      this.router.navigate(['/profile']);
    },
  },
    {
      text: 'Выход',
      icon: 'runner',
      onClick: () => {
        this.authService.logOut();
      },
    }];

  constructor(private authService: AuthService, private router: Router) {
  }

  ngOnInit() {
    this.authService.getUser().subscribe({
      next: user => {
        this.user = {
          username: user.userName,
          email: user.email,
        }
      }
    })
  }

  toggleMenu = () => {
    this.menuToggle.emit();
  }
}

@NgModule({
  imports: [
    CommonModule,
    DxButtonModule,
    UserPanelModule,
    DxToolbarModule,
  ],
  declarations: [HeaderComponent],
  exports: [HeaderComponent],
})
export class HeaderModule {
}
