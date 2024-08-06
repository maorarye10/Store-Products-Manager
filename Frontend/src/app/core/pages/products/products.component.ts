import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../services/products.service';
import { ProductConverterService } from '../../services/product-conveter.service';
import { Product } from '../../models/product';
import { QueryObject } from '../../helpers/query-object';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit {
  isLoading:boolean = true;
  products:Product[] = [];
  totalCount:number = 0;
  pagesCount:number = 0;
  query: QueryObject = {
    nameSearch: "", 
    categoryId: 0,
    sortBy: "", 
    isDecending: false, 
    pageNumber: 1, 
    pageSize: 3
  }
  authenticated:boolean = false;
  

  constructor(private router: Router, private productsService: ProductsService, private productConverter: ProductConverterService, private authService:AuthService) {}

  ngOnInit(): void {
    this.getProducts(this.query);
    this.authenticated = this.authService.isTokenValid();
  }

  onNextClick(){
    this.query.pageNumber++;
    this.getProducts(this.query);
  }

  onPreviousClick(){
    this.query.pageNumber--;
    this.getProducts(this.query);
  }

  filterAndSortProducts(query:QueryObject) {
    this.query = query;
    this.getProducts(this.query);
  }

  onDeleteEvent(id:number){
    this.productsService.deleteProduct(id).subscribe(result => {
      this.getProducts(this.query);
    })
  }

  onEditEvent(id:number){
    this.router.navigateByUrl(`/edit-product/${id}`);
  }

  onAddProductClick(){
    this.router.navigateByUrl("/add-product");
  }

  private getProducts(query:QueryObject){
    this.isLoading = true;
    this.productsService.getProducts(query).subscribe(data => {
      this.products = data.products.map(pDTO => this.productConverter.toProductModel(pDTO));
      this.totalCount = data.totalCount;
      this.pagesCount = Math.ceil(this.totalCount / this.query.pageSize);
      this.isLoading = false;
    });
  }
  
  onLogOutClick() {
    this.authService.logOut();
    this.router.navigateByUrl("/");
  }
  
}
