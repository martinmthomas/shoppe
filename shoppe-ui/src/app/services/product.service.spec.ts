import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { Product } from '../models/product';

import { ProductService } from './product.service';

describe('ProductService', () => {
  let service: ProductService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    });

    service = TestBed.inject(ProductService);
  });

  it('updateProduct should notify all subscribers', () => {
    const productUpdated: Product =
      { code: 'milk', description: 'desc', imageUrl: 'url', price: 1.5, quantity: 1 };

    service.updateProduct(productUpdated);

    service.onProductUpdated()
      .subscribe(product => {
        expect(productUpdated).toEqual(product);
      });
  });
});
