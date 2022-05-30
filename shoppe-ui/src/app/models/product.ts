export interface Product extends ProductSlim {
    description: string;
    imageUrl: string;
}

export interface ProductSlim {
    code: string;
    price: number;
    maxAvailable: number;
    quantity?: number;
}