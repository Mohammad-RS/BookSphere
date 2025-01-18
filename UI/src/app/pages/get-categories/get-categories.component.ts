import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { BookService } from '../../services/book-service';
import { BusinessResult } from '../../models/business-result-model';
import { RouterLink } from '@angular/router';
import { CategoryModel } from '../../models/category-model';

@Component({
  selector: 'app-get-categories',
  imports: [CommonModule, RouterLink],
  templateUrl: './get-categories.component.html',
  styleUrl: './get-categories.component.css',
})
export class GetCategoriesComponent implements OnInit {
  constructor(private bookService: BookService) {}

  categories: CategoryModel[] = [];

  ngOnInit() {
    this.GetCategories();
  }

  GetCategories() {
    this.bookService
      .getCategories()
      .subscribe((response: BusinessResult<CategoryModel[]>) => {
        if (response.success) {
          this.categories = response.data;
        } else {
          alert(response.errorMessage);
        }
      });
  }
}
