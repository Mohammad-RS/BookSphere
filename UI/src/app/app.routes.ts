import { Routes } from '@angular/router';
import { UserLoginComponent } from './pages/user-login/user-login.component';
import { UserProfileComponent } from './pages/user-profile/user-profile.component';
import { UserRegisterComponent } from './pages/user-register/user-register.component';
import { UserProfileVisitComponent } from './pages/user-profile-visit/user-profile-visit.component';
import { UserLogoutComponent } from './pages/user-logout/user-logout.component';
import { AuthorAddComponent } from './pages/author-add/author-add.component';
import { CategoryAddComponent } from './pages/category-add/category-add.component';
import { BookAddComponent } from './pages/book-add/book-add.component';
import { GetAuthorsComponent } from './pages/get-authors/get-authors.component';
import { GetBooksComponent } from './pages/get-books/get-books.component';
import { GetCategoriesComponent } from './pages/get-categories/get-categories.component';
import { BookDetailsComponent } from './pages/book-details/book-details.component';

export const routes: Routes = [
  { path: 'register', component: UserRegisterComponent },
  { path: 'login', component: UserLoginComponent },

  { path: 'profile', component: UserProfileComponent },
  { path: 'logout', component: UserLogoutComponent },

  { path: 'add-author', component: AuthorAddComponent },
  { path: 'add-category', component: CategoryAddComponent },
  { path: 'add-book', component: BookAddComponent },

  { path: 'authors', component: GetAuthorsComponent },
  { path: 'categories', component: GetCategoriesComponent },
  { path: 'books', component: GetBooksComponent },
  { path: 'books/:bookId', component: BookDetailsComponent },
];
