import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cart } from 'src/app/models/cart';
import { Country } from 'src/app/models/country';
import { CartService } from 'src/app/services/cart.service';
import { CountryService } from 'src/app/services/country.service';
import { OrderService } from 'src/app/services/order.service';

@Component({
  selector: 'checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  cart: Cart = {} as Cart;

  get isCartEmpty(): boolean {
    return !this.cart?.products || this.cart.products.length === 0;
  }

  currencySym = '$';
  fxRate = 1;

  constructor(
    private cartService: CartService,
    private router: Router,
    private route: ActivatedRoute,
    private countryService: CountryService,
    private orderService: OrderService
  ) { }

  ngOnInit(): void {
    this.countryService.onCountryUpdated()
      .subscribe(country => this.countryUpdated(country));

    this.cartService.onCartUpdated()
      .subscribe(cart => this.cart = cart);
  }

  placeOrder() {
    this.orderService.placeOrder(this.cart.products)
      .subscribe(result => {
        this.router.navigate(['completed'], {
          relativeTo: this.route,
          queryParams: { orderId: result.orderId }
        });
      });
  }

  private countryUpdated(country: Country) {
    this.currencySym = country.currencySym;
    this.fxRate = country.fxRate;
  }
}
