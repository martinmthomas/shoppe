import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { Country } from 'src/app/models/country';
import { AmountPipe } from 'src/app/pipes/amount.pipe';
import { CountryService } from 'src/app/services/country.service';
import { OrderService } from 'src/app/services/order.service';

import { CheckoutComponent } from './checkout.component';

describe('CheckoutComponent', () => {
  let component: CheckoutComponent;
  let fixture: ComponentFixture<CheckoutComponent>;
  let orderService: OrderService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CheckoutComponent, AmountPipe],
      imports: [HttpClientModule, RouterTestingModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckoutComponent);
    orderService = TestBed.inject(OrderService);
    router = TestBed.inject(Router);
    
    const countryService = TestBed.inject(CountryService);
    spyOn(countryService, 'onCountryUpdated').and.returnValue(of({ code: 'au', currencySym: '$', fxRate: 1 } as Country));
    
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('placeOrder should redirect to Thank You page', () => {
    spyOn(orderService, 'placeOrder').and.returnValue(of({ orderId: '123' }));
    const routerSpy = spyOn(router, 'navigate');

    component.placeOrder();

    expect(routerSpy).toHaveBeenCalled();
  });
});
