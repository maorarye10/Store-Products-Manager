import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { QueryObject } from '../../helpers/query-object';
import { CategoriesService } from '../../services/categories.service';
import { CategoryConverterService } from '../../services/category-converter.service';
import { Category } from '../../models/category';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-filter-sort-form',
  templateUrl: './filter-sort-form.component.html',
  styleUrl: './filter-sort-form.component.css'
})
export class FilterSortFormComponent implements OnInit {
  sortFilterForm = new FormGroup({
    name: new FormControl(''),
    category: new FormControl(0),
    sortBy: new FormControl(''),
    isDecending: new FormControl(false)
  })
  @Output() formSubmitEvent = new EventEmitter<QueryObject>();
  categories:Category[] = [];

  constructor(private categoriesServies: CategoriesService, private categoryConverter: CategoryConverterService) {}

  ngOnInit(): void {
    this.categoriesServies.getCategories().subscribe(data => {
      this.categories = data.map(c => this.categoryConverter.toCategoryModel(c));
    });
  }

  onFormSubmit() {
    const query:QueryObject = {
      nameSearch: this.sortFilterForm.value.name ?? "",
      categoryId: this.sortFilterForm.value.category ?? 0,
      sortBy: this.sortFilterForm.value.sortBy ?? "",
      isDecending: this.sortFilterForm.value.isDecending ?? false,
      pageNumber: 1,
      pageSize: 3
    }
    this.formSubmitEvent.emit(query);
  }
}
