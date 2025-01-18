import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book-service';
import { BusinessResult } from '../../models/business-result-model';
import { BookModel } from '../../models/book-model';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-get-books',
  imports: [CommonModule, RouterLink, RouterOutlet],
  templateUrl: './get-books.component.html',
  styleUrls: ['./get-books.component.css'],
})
export class GetBooksComponent implements OnInit {
  books: BookModel[] = [];
  filteredBooks: BookModel[] = [];

  constructor(private bookService: BookService) {}

  ngOnInit() {
    this.GetBooks();
  }

  GetBooks() {
    this.bookService
      .getBooks()
      .subscribe((response: BusinessResult<BookModel[]>) => {
        if (response.success) {
          this.books = response.data;
          this.filteredBooks = this.books;
        } else {
          alert(response.errorMessage);
        }
      });
  }

  filterResults(text: string) {
    if (!text) {
      this.filteredBooks = this.books;
      return;
    }

    this.filteredBooks = this.books.filter(
      (book) =>
        book?.title.toLowerCase().includes(text.toLowerCase()) ||
        book?.isbn.toLowerCase().includes(text.toLowerCase())
    );
  }
}
