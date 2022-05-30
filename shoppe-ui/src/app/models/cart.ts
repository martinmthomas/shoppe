import { Product, ProductSlim } from "./product";

export interface Cart {
    products: ProductSlim[];
    shippingCost: number;
    total: number;
}

export interface CartUpdateRequest {
    userId: string;
    products: ProductSlim[];
}