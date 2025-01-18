import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book-service';
import { BookModel } from '../../models/book-model';
import { BusinessResult } from '../../models/business-result-model';
import { ActivatedRoute } from '@angular/router';
import { BookDetailsModel } from '../../models/book-details-model';

@Component({
  selector: 'app-book-details',
  imports: [CommonModule],
  templateUrl: './book-details.component.html',
  styleUrl: './book-details.component.css',
})
export class BookDetailsComponent implements OnInit {
  constructor(
    private bookService: BookService,
    private route: ActivatedRoute
  ) {}

  book: BookDetailsModel | null = null;
  bookId = 0;

  ngOnInit() {
    this.bookId = parseInt(this.route.snapshot.paramMap.get('bookId') ?? '0');

    this.GetBookDetails();
  }

  GetBookDetails() {
    this.bookService.getBookDetails(this.bookId).subscribe((response) => {
      if (response.success) {
        this.book = response.data;
      } else {
        alert(response.errorMessage);
      }
    });
  }
}
