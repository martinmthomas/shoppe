import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Product } from 'src/app/models/product';
import { AmountPipe } from 'src/app/pipes/amount.pipe';
import { ProductService } from 'src/app/services/product.service';

import { ProductComponent } from './product.component';

describe('ProductComponent', () => {
  let component: ProductComponent;
  let fixture: ComponentFixture<ProductComponent>;
  let productService: ProductService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ProductComponent, AmountPipe],
      imports: [HttpClientModule, RouterTestingModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    productService = TestBed.inject(ProductService);
  });

  it('priceFormatted should return formatted text', () => {
    component.currencySym = '¥';
    component.fxRate = 0.5

    component.product.price = 3.35;
    expect(component.priceFormatted).toBe('¥1.68'); //1.675 rounded to 2 decimal places is 1.68

    component.product.price = 2;
    expect(component.priceFormatted).toBe('¥1.00');
  });

  it('update should update product details in product service', () => {
    const productServiceSpy = spyOn(productService, 'updateProduct');

    component.product.code = 'abc'
    component.product.quantity = 33;

    component.update();

    expect(productServiceSpy).toHaveBeenCalledWith({ code: 'abc', quantity: 33 } as Product);
  });

  it('update should reset quantity if it is above maxAvailable', () => {
    const productServiceSpy = spyOn(productService, 'updateProduct');

    component.product.code = 'abc'
    component.product.quantity = 33;
    component.product.maxAvailable = 30

    component.update();

    expect(productServiceSpy).toHaveBeenCalledWith({ code: 'abc', quantity: 30, maxAvailable: 30 } as Product);
  });

  it('remove should update product details in product service', () => {
    const productServiceSpy = spyOn(productService, 'updateProduct');

    component.product.code = 'abc'
    component.product.quantity = 33;

    component.remove();

    expect(productServiceSpy).toHaveBeenCalledWith({ code: 'abc', quantity: 0 } as Product);
  });
});
