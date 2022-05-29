import { Component, OnInit } from '@angular/core';
import { Country } from 'src/app/models/country';
import { CountryService } from 'src/app/services/country.service';

@Component({
  selector: 'country-picker',
  templateUrl: './country-picker.component.html',
  styleUrls: ['./country-picker.component.scss']
})
export class CountryPickerComponent implements OnInit {

  selectedCountry: Country | null = null;

  countries: Country[] = [];

  constructor(private countryService: CountryService) { }

  ngOnInit(): void {
    this.countryService.getAll()
      .subscribe(countries => {
        this.countries = countries;
        const country = this.countryService.getSelectedCountry() ?? this.countries[0];
        this.selectCountry(country);
      });
  }

  selectCountry(country: Country) {
    this.selectedCountry = country;
    this.countryService.setCountry(country);
  }
}
