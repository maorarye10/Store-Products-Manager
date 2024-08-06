import { Injectable } from '@angular/core';
import { ServerProductDTO } from '../DTOs/product/server-product-dto';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductConverterService {
  constructor() { }

  toProductModel(productDTO:ServerProductDTO) : Product {
    const product:Product = {
        id: productDTO.id,
        name: productDTO.name,
        price: productDTO.price,
        unitsInStock: productDTO.unitsInStock,
        categoryId: productDTO.categoryId,
        categoryName: productDTO.categoryName
    }
    return product;
  }
}