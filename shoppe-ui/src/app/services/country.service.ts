import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { Country } from '../models/country';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private countrySubject: BehaviorSubject<Country>;

  private readonly countryKey = "COUNTRY";

  constructor(
    private http: HttpClient,
    @Inject('BASE_API_URL') private baseUrl: string
  ) {
    const country = this.getSelectedCountry();
    this.countrySubject = new BehaviorSubject<Country>(country);
  }

  getSelectedCountry(): Country {
    const json = localStorage.getItem(this.countryKey);
    return json ? JSON.parse(json) : null;
  }

  getAll(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.baseUrl}/country/all`);
  }

  /** Sets user's country. This notifies all concerned components to update their 
   * display accordingly.
   */
  setCountry(country: Country) {
    localStorage.setItem(this.countryKey, JSON.stringify(country));
    this.countrySubject.next(country);
  }

  /** Subsribe to this observable to get notified whenever user's country is updated. */
  onCountryUpdated(): Observable<Country> {
    return this.countrySubject.asObservable();
  }
}
