import { Product } from "./product";

export interface Cart {
    products: Product[];
    shippingCost: number;
    total: number;
}