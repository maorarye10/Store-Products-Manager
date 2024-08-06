import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductCardComponent } from './product-card/product-card.component';
import { FilterSortFormComponent } from './filter-sort-form/filter-sort-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { ProductFormComponent } from './product-form/product-form.component';
import { NavbarComponent } from './navbar/navbar.component';



@NgModule({
  declarations: [ProductCardComponent, FilterSortFormComponent, ProductFormComponent, NavbarComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    ProductCardComponent,
    FilterSortFormComponent,
    ProductFormComponent,
  ]
})
export class ComponentsModule { }
