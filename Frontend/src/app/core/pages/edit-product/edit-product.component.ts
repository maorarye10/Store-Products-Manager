import { Component, OnInit } from '@angular/core';
import { ProductDTO } from '../../DTOs/product/product-dto';
import { ProductsService } from '../../services/products.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductConverterService } from '../../services/product-conveter.service';
import { Product } from '../../models/product';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrl: './edit-product.component.css'
})
export class EditProductComponent implements OnInit {
  product!:Product;
  productId: number = 0;
  loaded:boolean = false;
  constructor(private productsService:ProductsService, private productsConterter:ProductConverterService, private route:ActivatedRoute, private router:Router) {}
  
  ngOnInit(): void {
    this.productId = Number(this.route.snapshot.params['id']);
    this.productsService.getProductById(this.productId).subscribe(data => {
      this.product = this.productsConterter.toProductModel(data);
      this.loaded = true;
    }, (error:HttpErrorResponse) => {
      console.log(error.message);
      this.router.navigateByUrl("/products");
    });
  }
  success: boolean = false

  onClickEvent(productDTO: ProductDTO) {
    this.productsService.editProduct(productDTO, this.productId).subscribe(data => {
      this.success = true;
      setTimeout(() => {
        this.success = false;
      }, 3000);
    });
  }
}
