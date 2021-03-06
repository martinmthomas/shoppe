import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { PlaceOrderRequest } from '../models/order';
import { ProductSlim } from '../models/product';
import { CartService } from './cart.service';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  private get userId(): string {
    return this.userService.getUserId();
  }

  constructor(
    private http: HttpClient,
    @Inject('BASE_API_URL') private baseUrl: string,
    private userService: UserService,
    private cartService: CartService) { }

  placeOrder(products: ProductSlim[]): Observable<any> {
    const request: PlaceOrderRequest = {
      userId: this.userId,
      products: products
    };

    return this.http.post<any>(`${this.baseUrl}/order`, request)
      .pipe(
        switchMap(orderResult => {
          this.cartService.clear();
          return of(orderResult);
        }),
        catchError(err => {
          console.log(err);
          this.cartService.clear();
          throw new Error(err);
        })
      );
  }
}
