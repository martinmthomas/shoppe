import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Cart } from 'src/app/models/cart';
import { Country } from 'src/app/models/country';
import { Product, ProductSlim } from 'src/app/models/product';
import { CartService } from 'src/app/services/cart.service';
import { CountryService } from 'src/app/services/country.service';
import { OrderService } from 'src/app/services/order.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  cart: Cart = {} as Cart;
  cartProducts: Product[] = [];

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
    private orderService: OrderService,
    private productService: ProductService
  ) { }

  ngOnInit(): void {
    this.countryService.onCountryUpdated()
      .subscribe(country => this.countryUpdated(country));

    this.cartService.onCartUpdated()
      .subscribe(cart => this.setCart(cart));
  }

  placeOrder() {
    const productsToUpdate: ProductSlim[] = [];
    this.cartProducts.forEach(cp => {
      productsToUpdate.push({
        code: cp.code,
        maxAvailable: cp.maxAvailable,
        price: cp.price,
        quantity: cp.quantity
      });
    });

    this.orderService.placeOrder(productsToUpdate)
      .subscribe(result => {
        this.router.navigate(['completed'], {
          relativeTo: this.route,
          queryParams: { orderId: result.orderId }
        });
      },
        err => {
          this.router.navigate(['']);
        });
  }

  private setCart(cart: Cart) {
    this.cart = cart;
    this.cartProducts = [];

    this.productService.getAll()
      .subscribe(products => {
        cart.products.forEach(p => {
          const product = products.find(prod => prod.code === p.code);
          if (product) {
            this.cartProducts.push({
              code: p.code,
              description: product.description,
              imageUrl: product.imageUrl,
              maxAvailable: product.maxAvailable,
              price: product.price,
              quantity: p.quantity
            });
          }
        });
      });
  }

  private countryUpdated(country: Country) {
    this.currencySym = country.currencySym;
    this.fxRate = country.fxRate;
  }
}
