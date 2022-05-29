import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Product } from '../models/product';
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

  placeOrder(products: Product[]): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/order/${this.userId}`, products)
      .pipe(
        switchMap(orderResult => {
          this.cartService.clear();
          return of(orderResult);
        })
      );
  }
}
