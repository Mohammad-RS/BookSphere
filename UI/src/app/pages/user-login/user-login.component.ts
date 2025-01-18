import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { UserService } from '../../services/user-service';
import { Router } from '@angular/router';
import { UserLoginModel } from '../../models/user-login-model';

@Component({
  selector: 'app-user-login',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './user-login.component.html',
  styleUrl: './user-login.component.css',
})
export class UserLoginComponent {
  constructor(private userService: UserService, private router: Router) {}

  loginForm = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(5),
      Validators.maxLength(50),
      Validators.pattern(/^[a-zA-Z0-9_]+$/),
      Validators.pattern(/^(?!.*[@ -]).*$/),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(16),
      Validators.pattern(/(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])/),
    ]),
  });

  get username() {
    return this.loginForm.get('username') as FormControl;
  }

  get password() {
    return this.loginForm.get('password') as FormControl;
  }

  login() {
    if (this.loginForm.valid) {
      let credentials: UserLoginModel = {
        username: this.loginForm.value.username as string,
        password: this.loginForm.value.password as string,
      };

      this.userService.postLogin(credentials).subscribe((response) => {
        if (response.success) {
          sessionStorage.setItem('jwt', response.data);
          this.router.navigate(['profile']);
        } else {
          alert(response.errorMessage);
        }
      });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }
}
