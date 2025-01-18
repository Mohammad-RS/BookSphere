import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { UserService } from '../../services/user-service';
import { Router } from '@angular/router';
import { UserRegisterModel } from '../../models/user-register-model';
import { CommonModule } from '@angular/common';
import { MatchingPasswords } from '../../utility/validation';

@Component({
  selector: 'app-user-register',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.css',
})
export class UserRegisterComponent {
  constructor(private userService: UserService, private router: Router) {}

  avatarData: string = '';
  showAvatar: boolean = false;

  registerForm = new FormGroup(
    {
      username: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50),
        Validators.pattern(/^[a-zA-Z0-9_]+$/),
        Validators.pattern(/^(?!.*[@ -]).*$/),
      ]),
      password1: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(16),
        Validators.pattern(/(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])/),
      ]),
      password2: new FormControl('', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(16),
        Validators.pattern(/(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])/),
      ]),
      email: new FormControl('', [
        Validators.required,
        Validators.email,
        Validators.maxLength(100),
        Validators.pattern(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/),
      ]),
      fullname: new FormControl('', [Validators.maxLength(50)]),
      avatar: new FormControl(''),
    },
    { validators: MatchingPasswords('password1', 'password2') }
  );

  get username() {
    return this.registerForm.get('username') as FormControl;
  }
  get email() {
    return this.registerForm.get('email') as FormControl;
  }
  get password1() {
    return this.registerForm.get('password1') as FormControl;
  }
  get password2() {
    return this.registerForm.get('password2') as FormControl;
  }
  get fullname() {
    return this.registerForm.get('fullname') as FormControl;
  }
  get avatar() {
    return this.registerForm.get('avatar') as FormControl;
  }

  Show(event: any) {
    let img = event.target.files[0];
    let reader = new FileReader();

    reader.readAsDataURL(img);
    reader.onload = (e) => {
      if (e.target) {
        this.avatarData = e.target?.result as string;
      }
      this.showAvatar = true;
    };
  }

  register() {
    if (this.registerForm.valid) {
      let request: UserRegisterModel = {
        username: this.registerForm.value.username as string,
        password1: this.registerForm.value.password1 as string,
        password2: this.registerForm.value.password2 as string,
        email: this.registerForm.value.email as string,
        fullname: this.registerForm.value.fullname as string,
        avatar: this.avatarData,
      };

      this.userService.postRegister(request).subscribe((response) => {
        if (response.success) {
          this.router.navigate(['login']);
        } else {
          alert(response.errorMessage);
        }
      });
    } else {
      this.registerForm.markAllAsTouched();
    }
  }
}
