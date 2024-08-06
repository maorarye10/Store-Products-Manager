import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { ServerCategoryDTO } from '../DTOs/category/server-category-dto';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {
  private _productsApiUrl:string = environment.API_URL + "/categories";
  constructor(private httpClient:HttpClient) { }

  getCategories() {
    return this.httpClient.get<ServerCategoryDTO[]>(this._productsApiUrl);
  }
}