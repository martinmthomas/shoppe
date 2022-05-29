import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { RouterModule, Routes } from '@angular/router';
import { CoreModule } from '../core/core.module';
import { AmountPipe } from 'src/app/pipes/amount.pipe';
import { OrderPlacedComponent } from './order-placed/order-placed.component';

const routes: Routes = [
  {
    path: '',
    component: CheckoutComponent
  },
  {
    path: 'completed',
    component: OrderPlacedComponent
  }
];

@NgModule({
  declarations: [
    CheckoutComponent,
    OrderPlacedComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    CoreModule
  ]
})
export class CheckoutModule { }
