import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { ProductsQueryResultDTO } from '../DTOs/product/products-query-result-dto';
import { ProductDTO } from '../DTOs/product/product-dto';
import { ServerProductDTO } from '../DTOs/product/server-product-dto';
import { QueryObject } from '../helpers/query-object';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {
  private _productsApiUrl:string = environment.API_URL + "/products";
  constructor(private httpClient:HttpClient) { }

  /* getProducts(name:string = "", categoryId:number = 0, sortBy:string = "", isDecsending:boolean = false, pageNumber:number = 1, pageSize:number = 3) {
    return this.httpClient.get<ProductsQueryResultDTO>(this._productsApiUrl, {params: {
      name,
      categoryId,
      sortBy,
      isDecsending,
      pageNumber,
      pageSize
    }});
  } */

  getProducts(query:QueryObject) {
    return this.httpClient.get<ProductsQueryResultDTO>(this._productsApiUrl, {params: {
      name: query.nameSearch,
      categoryId: query.categoryId,
      sortBy: query.sortBy,
      isDecsending: query.isDecending,
      pageNumber: query.pageNumber,
      pageSize: query.pageSize
    }});
  }

  getProductById(id: number) {
    return this.httpClient.get<ServerProductDTO>(`${this._productsApiUrl}/${id}`);
  }

  deleteProduct(id: number) {
    return this.httpClient.delete<void>(`${this._productsApiUrl}/${id}`);
  }

  addProduct(productDTO: ProductDTO){
    return this.httpClient.post<ServerProductDTO>(this._productsApiUrl, {
      name: productDTO.name, 
      categoryId: productDTO.categoryId, 
      price: productDTO.price, 
      unitsInStock: productDTO.unitsInStock});
  }

  editProduct(productDTO: ProductDTO, id: number){
    return this.httpClient.put<ServerProductDTO>(`${this._productsApiUrl}/${id}`, productDTO);
  }
}
