import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavModule } from './components/nav/nav.module';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NavModule,
    FormsModule
  ],
  providers: [
    { provide: "BASE_API_URL", useValue: environment.baseApiUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
