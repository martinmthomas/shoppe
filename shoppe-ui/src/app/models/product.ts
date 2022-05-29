export interface Product {
    code: string;
    description: string;
    imageUrl: string;
    price: number;
    maxAvailable: number;
    quantity?: number;
}