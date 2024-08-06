import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from '../../models/product';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent implements OnInit {
  @Input() product!:Product;
  @Output() deleteEvent = new EventEmitter<number>();
  @Output() editEvent = new EventEmitter<number>();
  authenticated:boolean = false;

  constructor(private authService:AuthService) {}

  ngOnInit(): void {
    this.authenticated = this.authService.isTokenValid()
  }

  onDeleteClick() {
    this.deleteEvent.emit(this.product.id);
  }
  onEditClick() {
    this.editEvent.emit(this.product.id);
  }
}
