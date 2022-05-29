import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'checkout-ticker',
  templateUrl: './checkout-ticker.component.html',
  styleUrls: ['./checkout-ticker.component.scss']
})
export class CheckoutTickerComponent implements OnInit {

  totalItems = 0;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cartService.onCartUpdated()
      .subscribe(cart => this.totalItems = cart?.products?.length);
  }
}
