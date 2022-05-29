import { Component, Input, OnInit } from '@angular/core';
import { combineLatest, of } from 'rxjs';
import { Cart } from 'src/app/models/cart';
import { Product } from 'src/app/models/product';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  products: Product[] = [];

  constructor(
    private productService: ProductService,
    private cartService: CartService) { }

  ngOnInit(): void {
    combineLatest([
      this.productService.getAll(),
      this.cartService.get()
    ])
      .subscribe(([products, cart]) => {
        this.products = products;

        this.updateBasedOnAlreadySavedProducts(cart);
      });
  }

  /** Updates products quantity details based on products already to cart in a previous session. */
  private updateBasedOnAlreadySavedProducts(cart: Cart) {
    cart?.products?.forEach(cartProduct => {
      const index = this.products.findIndex(p => p.code === cartProduct.code);
      if (index >= 0) {
        this.products[index].quantity = cartProduct.quantity;
      }
    });
  }
}
