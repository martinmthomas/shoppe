import { Component, Input, OnInit } from '@angular/core';
import { Country } from 'src/app/models/country';
import { Product } from 'src/app/models/product';
import { CountryService } from 'src/app/services/country.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  @Input() product: Product = {} as Product;
  @Input() isLineView = false;

  currencySym = '$';
  fxRate = 1;

  public get priceFormatted(): string {
    return `${this.currencySym}${(this.product.price * this.fxRate).toFixed(2)}`;
  }

  constructor(
    private countryService: CountryService,
    private productService: ProductService) { }

  ngOnInit(): void {
    this.product.quantity ??= 0;
    
    this.countryService.onCountryUpdated()
      .subscribe(country => this.countryUpdated(country))
  }

  update() {
    this.productService.updateProduct(this.product);
  }

  remove() {
    this.product.quantity = 0;
    this.productService.updateProduct(this.product);
  }

  private countryUpdated(country: Country) {
    this.currencySym = country.currencySym;
    this.fxRate = country.fxRate;
  }
}
