import { HttpClientModule } from '@angular/common/http';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { Country } from 'src/app/models/country';
import { AmountPipe } from 'src/app/pipes/amount.pipe';
import { CountryService } from 'src/app/services/country.service';

import { CountryPickerComponent } from './country-picker.component';

describe('CountryPickerComponent', () => {
  let component: CountryPickerComponent;
  let fixture: ComponentFixture<CountryPickerComponent>;
  let countryService: CountryService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CountryPickerComponent, AmountPipe],
      imports: [HttpClientModule, RouterTestingModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CountryPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();

    countryService = TestBed.inject(CountryService);
  });

  it('selectCountry should update country', () => {
    const country: Country = { code: 'us', name: 'USA', currencySym: '$', fxRate: 1.5 };

    const countryServiceSpy = spyOn(countryService, 'setCountry');

    component.selectCountry(country);

    expect(component.selectedCountry).toEqual(country);
    expect(countryServiceSpy).toHaveBeenCalledOnceWith(country);
  });
});
