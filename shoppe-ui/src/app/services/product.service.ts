import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, of, Subject } from 'rxjs';
import { Product } from '../models/product';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private productSubject = new Subject<Product>();

  constructor(
    private http: HttpClient,
    @Inject('BASE_API_URL') private baseUrl: string
  ) { }

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/product/all`);
  }

  updateProduct(product: Product) {
    this.productSubject.next(product);
  }

  onProductUpdated(): Observable<Product> {
    return this.productSubject.asObservable();
  }
}
