import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  NavigationEnd,
  Router,
  RouterLink,
  RouterOutlet,
} from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  title: string = "UI";
  isLoggedIn: boolean = false;

  constructor(private router: Router) {}

  ngOnInit() {
    this.updateLoginStatus();

    this.router.events
      .pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe(() => {
        this.updateLoginStatus();
      });
  }

  private updateLoginStatus() {
    this.isLoggedIn = !!sessionStorage.getItem('jwt');
  }
}
