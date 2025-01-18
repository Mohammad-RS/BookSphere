import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AuthorModel } from '../../models/author-model';
import { BookService } from '../../services/book-service';
import { BusinessResult } from '../../models/business-result-model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-get-authors',
  imports: [CommonModule, RouterLink],
  templateUrl: './get-authors.component.html',
  styleUrl: './get-authors.component.css',
})
export class GetAuthorsComponent implements OnInit {
  constructor(private bookService: BookService) {}

  authors: AuthorModel[] = [];

  ngOnInit() {
    this.GetAuthors();
  }

  GetAuthors() {
    this.bookService
      .getAuthors()
      .subscribe((response: BusinessResult<AuthorModel[]>) => {
        if (response.success) {
          this.authors = response.data;
        } else {
          alert(response.errorMessage);
        }
      });
  }
}
