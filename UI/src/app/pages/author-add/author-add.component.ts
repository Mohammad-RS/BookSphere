import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AddAuthorModel } from '../../models/add-author-model';
import { BookService } from '../../services/book-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-author-add',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './author-add.component.html',
  styleUrl: './author-add.component.css',
})
export class AuthorAddComponent {
  constructor(private bookService: BookService, private router: Router) {}

  addAuthorForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    bio: new FormControl(''),
  });

  get name() {
    return this.addAuthorForm.get('name') as FormControl;
  }
  get bio() {
    return this.addAuthorForm.get('bio') as FormControl;
  }

  addAuthor() {
    if (this.addAuthorForm.valid) {
      let request: AddAuthorModel = {
        name: this.addAuthorForm.value.name as string,
        bio: this.addAuthorForm.value.bio as string,
      };

      this.bookService.postAddAuthor(request).subscribe((response) => {
        if (response.success) {
          alert('Author added successfully.');
          this.router.navigate(['/authors']);
        } else {
          alert(response.errorMessage);
        }

        this.addAuthorForm.reset();
      });
    } else {
      this.addAuthorForm.markAllAsTouched();
    }
  }
}
