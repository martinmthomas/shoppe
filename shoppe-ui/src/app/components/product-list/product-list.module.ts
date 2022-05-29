import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from './product-list.component';
import { CoreModule } from '../core/core.module';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: ProductListComponent
  }
];

@NgModule({
  declarations: [
    ProductListComponent
  ],
  exports: [
    ProductListComponent
  ],
  imports: [
    CommonModule,
    CoreModule,
    RouterModule.forChild(routes)
  ]
})
export class ProductListModule { }
