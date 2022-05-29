import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AmountPipe } from 'src/app/pipes/amount.pipe';

import { CheckoutTickerComponent } from './checkout-ticker.component';

describe('CheckoutTickerComponent', () => {
  let component: CheckoutTickerComponent;
  let fixture: ComponentFixture<CheckoutTickerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CheckoutTickerComponent, AmountPipe],
      imports: [HttpClientModule, RouterTestingModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CheckoutTickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
