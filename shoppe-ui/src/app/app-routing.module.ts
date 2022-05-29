import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./components/product-list/product-list.module').then(m => m.ProductListModule)
  },
  {
    path: 'checkout',
    loadChildren: () => import('./components/checkout/checkout.module').then(m => m.CheckoutModule)
  },
  {
    path:'**',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
