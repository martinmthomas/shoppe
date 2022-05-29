import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { empty, EMPTY, of } from 'rxjs';
import { Cart } from '../models/cart';
import { Product } from '../models/product';

import { CartService } from './cart.service';
import { ProductService } from './product.service';

describe('CartService', () => {
  let cartService: CartService;
  let productService: ProductService;
  let httpClient: HttpClient;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    });

    httpClient = TestBed.inject(HttpClient);
    productService = TestBed.inject(ProductService);
  });

  // sets up a cart object to be returned when the http call is made.
  const expectedCart: Cart = {
    products: [{ code: 'milk', description: 'desc', imageUrl: 'url', price: 1.5, quantity: 1 }],
    shippingCost: 0,
    total: 1.5
  };

  const setupMocks = function () {
    // mocks onProductUpdated call with a dummy.
    let productServiceSpy = spyOn(productService, 'onProductUpdated');
    productServiceSpy.and.returnValue(EMPTY);

    let httpSpy = spyOn(httpClient, 'get');
    httpSpy.and.returnValue(of(expectedCart));

    cartService = TestBed.inject(CartService);
  }

  it('onCartUpdated should return cart details and initialise the service', () => {
    setupMocks();

    cartService.onCartUpdated()
      .subscribe(cart => {
        expect(cart).toEqual(expectedCart);
      });
  });

  it('clear should reset the cart', () => {
    setupMocks();

    cartService.clear();

    cartService.onCartUpdated()
      .subscribe(cart => {
        expect(cart).toEqual(cartService.emptyCart);
      });
  });
});
