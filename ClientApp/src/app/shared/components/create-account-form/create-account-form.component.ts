import { CommonModule } from '@angular/common';
import { Component, NgModule } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ValidationCallbackData } from 'devextreme-angular/common';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import { AuthService } from '../../services';
import notify from 'devextreme/ui/notify';


@Component({
  selector: 'app-create-account-form',
  templateUrl: './create-account-form.component.html',
  styleUrls: ['./create-account-form.component.scss'],
})
export class CreateAccountFormComponent {
  loading = false;
  formData: any = {};

  constructor(private authService: AuthService, private router: Router) {
  }

  async onSubmit(e: Event) {
    e.preventDefault();
    const {username, email, password} = this.formData;
    this.loading = true;
    this.authService.createAccount(username, email, password).subscribe({
      next: (res) => {
        notify("Вы успешно зарегистрировались", 'success', 2000)
        this.loading = false;
      },
      error: e => {
        console.log(e)
        // e.errors.forEach((e: any) => notify(e.message, 'error', 2000))
        this.loading = false;
      }
    })
  }

  confirmPassword = (e: ValidationCallbackData) => {
    return e.value === this.formData.password;
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule,
  ],
  declarations: [CreateAccountFormComponent],
  exports: [CreateAccountFormComponent],
})
export class CreateAccountFormModule {
}
