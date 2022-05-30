import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Cart, CartUpdateRequest } from '../models/cart';
import { Product, ProductSlim } from '../models/product';
import { ProductService } from './product.service';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  get emptyCart(): Cart {
    return { products: [], shippingCost: 0, total: 0 };
  }

  private cart: Cart = this.emptyCart;

  private cartSubject: BehaviorSubject<Cart> | null = null;

  private get userId(): string {
    return this.userService.getUserId();
  }

  constructor(
    private http: HttpClient,
    @Inject('BASE_API_URL') private baseUrl: string,
    private productService: ProductService,
    private userService: UserService) {
    this.init();
  }

  /** Gets user's cart. */
  get(): Observable<Cart> {
    return this.http.get<Cart>(`${this.baseUrl}/cart/${this.userId}`);
  }

  /** Clears all products from the cart. */
  clear() {
    this.setCart({ products: [], shippingCost: 0, total: 0 });
  }

  /** Notifies all consumers whenever cart is updated. */
  onCartUpdated(): Observable<Cart> {
    this.initCartSubject(this.cart);
    return this.cartSubject!.asObservable();
  }

  private init() {
    this.productService.onProductUpdated()
      .subscribe(product => this.productUpdated(product));

    this.get()
      .subscribe(cart => this.setCart(cart));
  }

  /** This initialises the CartSubject. Since we need to provide a default value based on Cart api call, we 
   * are using BehaviourSubject instead of Subject.
   */
  private initCartSubject(cart: Cart) {
    if (!this.cartSubject) {
      this.cartSubject = new BehaviorSubject<Cart>(cart);
    }
  }

  /** Updates cart state and then notifies subscribers. */
  private setCart(cart: Cart) {
    this.cart = cart;
    this.initCartSubject(cart);
    this.cartSubject!.next(cart);
  }

  /** Keeps track of product changes in the cart and notifies subscribers. */
  private productUpdated(product: Product) {
    if (product.quantity! > 0) {
      this.updateProductInCart(product);
    } else {
      this.removeProductFromCart(product);
    }

    this.saveCart();
  }

  private saveCart() {
    const request: CartUpdateRequest = {
      userId: this.userId,
      products: this.cart.products
    };

    this.http.post<Cart>(`${this.baseUrl}/cart`, request)
      .subscribe(updatedCart => this.setCart(updatedCart));
  }

  private updateProductInCart(product: Product) {
    let index = this.cart.products.findIndex(p => p.code == product.code);

    let productToUpdate = this.convertToProductSlim(product);
    if (index >= 0) {
      this.cart.products[index] = productToUpdate;
    }
    else {
      this.cart.products.push(productToUpdate);
    }
  }

  private removeProductFromCart(product: Product) {
    let index = this.cart.products.findIndex(p => p.code == product.code);

    if (index >= 0) {
      this.cart.products.splice(index, 1);
    }
  }

  private convertToProductSlim(product: Product) {
    const productSlim: ProductSlim = {
      code: product.code,
      maxAvailable: product.maxAvailable,
      price: product.price,
      quantity: product.quantity
    };

    return productSlim;
  }
}
