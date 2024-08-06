import { Injectable } from '@angular/core';
import { ServerCategoryDTO } from '../DTOs/category/server-category-dto';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root'
})
export class CategoryConverterService {
  constructor() { }

  toCategoryModel(categoryDTO:ServerCategoryDTO) : Category {
    const category:Category = {
        id: categoryDTO.id,
        name: categoryDTO.name,
        productsNum: categoryDTO.productsNum
    }
    return category;
  }
}