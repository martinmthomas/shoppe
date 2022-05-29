import { Pipe, PipeTransform } from "@angular/core";
import { Country } from "../models/country";

@Pipe({ name: 'amount' })
export class AmountPipe implements PipeTransform {

    transform(value: any, symbol: string, fxRate: number) {
        if (value !== null && symbol && fxRate) {
            return `${symbol}${(value * fxRate).toFixed(2)}`;
        }

        return null;
    }

}