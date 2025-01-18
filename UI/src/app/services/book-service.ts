import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BusinessResult } from '../models/business-result-model';
import { AddAuthorModel } from '../models/add-author-model';
import { _env } from '../env/env';
import { AddCategoryModel } from '../models/add-category-model';
import { AddBookModel } from '../models/add-book-model';
import { Observable } from 'rxjs';
import { AuthorModel } from '../models/author-model';
import { BookModel } from '../models/book-model';
import { CategoryModel } from '../models/category-model';
import { BookDetailsModel } from '../models/book-details-model';

@Injectable({ providedIn: 'root' })
export class BookService {
  constructor(private http: HttpClient) {}

  postAddAuthor(request: AddAuthorModel) {
    let url = `${_env.baseUrl}/book/add-author`;
    return this.http.post<BusinessResult<Number>>(url, request);
  }

  postAddCategory(request: AddCategoryModel) {
    let url = `${_env.baseUrl}/book/add-category`;
    return this.http.post<BusinessResult<Number>>(url, request);
  }

  postAddBook(request: AddBookModel) {
    let url = `${_env.baseUrl}/book/add-book`;
    return this.http.post<BusinessResult<Number>>(url, request);
  }

  getAuthors(): Observable<BusinessResult<AuthorModel[]>> {
    let url = `${_env.baseUrl}/book/authors`;
    return this.http.get<BusinessResult<AuthorModel[]>>(url);
  }

  getCategories(): Observable<BusinessResult<CategoryModel[]>> {
    let url = `${_env.baseUrl}/book/categories`;
    return this.http.get<BusinessResult<CategoryModel[]>>(url);
  }

  getBooks(): Observable<BusinessResult<BookModel[]>> {
    let url = `${_env.baseUrl}/book/books`;
    return this.http.get<BusinessResult<BookModel[]>>(url);
  }

  getBookDetails(bookId: number) {
    let url = `${_env.baseUrl}/book/books/${bookId}`;
    return this.http.get<BusinessResult<BookDetailsModel>>(url);
  }
}
