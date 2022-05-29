import { HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { Country } from '../models/country';

import { CountryService } from './country.service';

describe('CountryService', () => {
  let service: CountryService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule],
      providers: [
        { provide: "BASE_API_URL", useValue: 'https://someenvironment' }
      ]
    });
    service = TestBed.inject(CountryService);
  });

  it('getSelectedCountry should return the last selected country from localStorage', () => {
    const setLocalStgSpy = spyOn(localStorage, 'setItem').and.callThrough();
    const getLocalStgSpy = spyOn(localStorage, 'getItem').and.callThrough();

    const country: Country = { code: 'us', name: 'USA', currencySym: '$', fxRate: 1.5 };
    service.setCountry(country);

    const selectedCountry = service.getSelectedCountry();

    expect(selectedCountry).toEqual(country);
    expect(setLocalStgSpy).toHaveBeenCalled();
    expect(getLocalStgSpy).toHaveBeenCalled();
  });
});
