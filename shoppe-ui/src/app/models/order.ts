import { ProductSlim } from "./product";

export interface PlaceOrderRequest {
    userId: string;
    products: ProductSlim[];
}