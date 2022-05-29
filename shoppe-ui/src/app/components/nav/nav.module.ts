import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutTickerComponent } from './checkout-ticker/checkout-ticker.component';
import { CountryPickerComponent } from './country-picker/country-picker.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    CheckoutTickerComponent,
    CountryPickerComponent
  ],
  exports: [
    CheckoutTickerComponent,
    CountryPickerComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ]
})
export class NavModule { }
