import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { Product } from '../models/product';
import { CartService } from './cart.service';

import { OrderService } from './order.service';

describe('OrderService', () => {
  let httpClient: HttpClient;
  let cartService: CartService;
  let orderService: OrderService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    });

    httpClient = TestBed.inject(HttpClient);
    cartService = TestBed.inject(CartService);
    orderService = TestBed.inject(OrderService);
  });

  it('placeOrder should clear cart once done', () => {
    const productsToBuy: Product[] = [
      { code: 'milk', description: 'desc', imageUrl: 'url', price: 1.5, quantity: 1 }
    ]

    const httpSpy = spyOn(httpClient, 'post').and.returnValue(of({}));
    const cartServiceSpy = spyOn(cartService, 'clear');

    orderService.placeOrder(productsToBuy)
      .subscribe(r => {
        expect(cartServiceSpy).toHaveBeenCalled();
        expect(httpSpy).toHaveBeenCalled();
      });
  });
});
