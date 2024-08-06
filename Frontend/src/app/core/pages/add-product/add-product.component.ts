import { Component } from '@angular/core';
import { ProductDTO } from '../../DTOs/product/product-dto';
import { ProductsService } from '../../services/products.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.css'
})
export class AddProductComponent {
  constructor(private productsService:ProductsService) {}
  success: boolean = false

  onClickEvent(productDTO: ProductDTO) {
    this.productsService.addProduct(productDTO).subscribe(data => {
      this.success = true;
      setTimeout(() => {
        this.success = false;
      }, 3000);
    });
  }
}
