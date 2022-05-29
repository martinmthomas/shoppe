import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './product/product.component';
import { FormsModule } from '@angular/forms';
import { AmountPipe } from 'src/app/pipes/amount.pipe';

@NgModule({
  declarations: [
    ProductComponent,
    AmountPipe
  ],
  exports: [
    ProductComponent,
    AmountPipe
  ],
  imports: [
    CommonModule,
    FormsModule
  ]
})
export class CoreModule { }
