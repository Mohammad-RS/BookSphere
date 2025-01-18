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
import { AddCategoryModel } from '../../models/add-category-model';

@Component({
  selector: 'app-category-add',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './category-add.component.html',
  styleUrl: './category-add.component.css',
})
export class CategoryAddComponent {
  constructor(private bookService: BookService, private router: Router) {}

  addCategoryForm = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    summary: new FormControl(''),
  });

  get name() {
    return this.addCategoryForm.get('name') as FormControl;
  }
  get summary() {
    return this.addCategoryForm.get('summary') as FormControl;
  }

  addCategory() {
    if (this.addCategoryForm.valid) {
      let request: AddCategoryModel = {
        name: this.addCategoryForm.value.name as string,
        summary: this.addCategoryForm.value.summary as string,
      };

      this.bookService.postAddCategory(request).subscribe((response) => {
        if (response.success) {
          alert('Category added successfully.');
          this.router.navigate(['categories']);
        } else {
          alert(response.errorMessage);
        }

        this.addCategoryForm.reset();
      });
    } else {
      this.addCategoryForm.markAllAsTouched();
    }
  }
}
