import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-logout',
  imports: [],
  templateUrl: './user-logout.component.html',
  styleUrl: './user-logout.component.css',
})
export class UserLogoutComponent {
  constructor(private router: Router) {}

  logout() {
    sessionStorage.removeItem('jwt');
    this.router.navigate(['/login']);
  }

  goBack() {
    this.router.navigate(['']);
  }
}
