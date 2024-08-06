import { Component, EventEmitter, Input, OnChanges, OnInit, Output, output, SimpleChange, SimpleChanges } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { CategoriesService } from '../../services/categories.service';
import { Category } from '../../models/category';
import { CategoryConverterService } from '../../services/category-converter.service';
import { ProductDTO } from '../../DTOs/product/product-dto';
import { Product } from '../../models/product';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css'
})
export class ProductFormComponent implements OnInit, OnChanges {
  
  @Input() product:Product = {
    id: 0,
    name: "",
    price: 0,
    unitsInStock: 0,
    categoryId: 0,
    categoryName: ""
  };
  @Input() btnActionName!:string;
  @Input() clearFormOnSubmit!:boolean;
  @Output() clickEvent = new EventEmitter<ProductDTO>()

  productForm = new FormGroup({
    name: new FormControl(this.product.name,[
      Validators.required,
      Validators.minLength(3)
    ]),
    price: new FormControl(this.product.price,[
      Validators.required,
      Validators.pattern('[+]?([0-9]+(?:[\\.][0-9]*)?|\\.[0-9]+)')
    ]),
    unitsInStock: new FormControl(this.product.unitsInStock,[
      Validators.required,
      Validators.pattern('([1-9][0-9]*)')
    ]),
    categoryId: new FormControl(this.product.categoryId,[
      Validators.required
    ])
  });
  categories:Category[] = [];
  showError:boolean = false;
  errorMessage:string = "";
  success:boolean = false

  constructor(private router:Router, private categoriesService: CategoriesService, private categoryConverter: CategoryConverterService) {}
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['product'] && changes['product'].currentValue) {
      const newProduct = changes['product'].currentValue;

      this.productForm.patchValue({
        name: newProduct.name,
        price: newProduct.price,
        unitsInStock: newProduct.unitsInStock,
        categoryId: newProduct.categoryId
      });
    }
  }
  
  ngOnInit(): void {
    this.categoriesService.getCategories().subscribe(data => {
      this.categories = data.map(cDTO => this.categoryConverter.toCategoryModel(cDTO));
    });
  }

  onGoBackClick() {
    this.router.navigateByUrl("/products");
  }

  onSubmit() {
    if (this.productForm.invalid){
      this.showError = true;
    }
    else
    {
      this.showError = false;
      const productDTO:ProductDTO = {
        name: this.productForm.value.name ?? "",
        price: this.productForm.value.price ?? 0,
        unitsInStock: this.productForm.value.unitsInStock ?? 0,
        categoryId: this.productForm.value.categoryId ?? 0,
      };
      if (this.clearFormOnSubmit){
        this.productForm.reset();
      }
      this.clickEvent.emit(productDTO);
    }
  }
}
