import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { Cart } from 'src/app/models/cart';
import { Product } from 'src/app/models/product';
import { AmountPipe } from 'src/app/pipes/amount.pipe';
import { CartService } from 'src/app/services/cart.service';
import { ProductService } from 'src/app/services/product.service';

import { ProductListComponent } from './product-list.component';

describe('ProductListComponent', () => {
  let component: ProductListComponent;
  let fixture: ComponentFixture<ProductListComponent>;
  let productService: ProductService;
  let cartService: CartService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductListComponent, AmountPipe],
      imports: [HttpClientModule, RouterTestingModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    productService = TestBed.inject(ProductService);
    cartService = TestBed.inject(CartService);
  });

  it('should get products and show non-zero quantity for any products that were already in cart', () => {
    const productList: Product[] = [
      { code: 'biscuit', description: 'Biscuit', imageUrl: 'url', price: 5.5, maxAvailable: 100, quantity: 0 },
      { code: 'milk', description: 'desc', imageUrl: 'url', price: 1.5, maxAvailable: 100, quantity: 0 }
    ];
    const cart: Cart = {
      products: [{ code: 'milk', description: 'desc', imageUrl: 'url', price: 1.5, maxAvailable: 100, quantity: 3 }],
      shippingCost: 10,
      total: 1.5
    };

    const productServiceSpy = spyOn(productService, 'getAll').and.returnValue(of(productList));
    const cartServiceSpy = spyOn(cartService, 'get').and.returnValue(of(cart));

    component.ngOnInit();

    expect(productServiceSpy).toHaveBeenCalled();
    expect(cartServiceSpy).toHaveBeenCalled();
    expect(component.products[1].quantity).toBe(3);
  });
});
