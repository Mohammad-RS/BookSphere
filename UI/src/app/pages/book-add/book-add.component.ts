import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { BookService } from '../../services/book-service';
import { Router } from '@angular/router';
import { AddBookModel } from '../../models/add-book-model';
import { nonNegativeNumberValidator } from '../../utility/validation';

@Component({
  selector: 'app-book-add',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './book-add.component.html',
  styleUrl: './book-add.component.css',
})
export class BookAddComponent {
  constructor(private bookService: BookService, private router: Router) {}

  avatarData: string = '';
  showImage: boolean = false;

  addBookForm = new FormGroup({
    author: new FormControl('', [Validators.required]),
    category: new FormControl('', [Validators.required]),
    title: new FormControl('', [
      Validators.required,
      Validators.maxLength(100),
    ]),
    summary: new FormControl(''),
    isbn: new FormControl('', [Validators.maxLength(20)]),
    availableCopies: new FormControl('', [
      Validators.required,
      nonNegativeNumberValidator(),
    ]),
    coverImage: new FormControl(''),
  });

  get author() {
    return this.addBookForm.get('author') as FormControl;
  }
  get category() {
    return this.addBookForm.get('category') as FormControl;
  }
  get title() {
    return this.addBookForm.get('title') as FormControl;
  }
  get summary() {
    return this.addBookForm.get('summary') as FormControl;
  }
  get isbn() {
    return this.addBookForm.get('isbn') as FormControl;
  }
  get availableCopies() {
    return this.addBookForm.get('availableCopies') as FormControl;
  }
  get coverImage() {
    return this.addBookForm.get('coverImage') as FormControl;
  }

  Show(event: any) {
    let img = event.target.files[0];
    let reader = new FileReader();

    reader.readAsDataURL(img);
    reader.onload = (e) => {
      if (e.target) {
        this.avatarData = e.target?.result as string;
      }
      this.showImage = true;
    };
  }

  addBook() {
    if (this.addBookForm.valid) {
      let request: AddBookModel = {
        author: this.addBookForm.value.author as string,
        category: this.addBookForm.value.category as string,
        title: this.addBookForm.value.title as string,
        summary: this.addBookForm.value.summary as string,
        isbn: this.addBookForm.value.isbn as string,
        availableCopies: Number(this.addBookForm.value.availableCopies),
        coverImage: this.avatarData,
      };

      this.bookService.postAddBook(request).subscribe((response) => {
        if (response.success) {
          alert('Book added successfully.');
          // this.router.navigate(['/Books']);
        } else {
          alert(response.errorMessage);
        }

        this.addBookForm.reset();
      });
    } else {
      this.addBookForm.markAllAsTouched();
    }
  }
}
