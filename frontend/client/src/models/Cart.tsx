export interface Cart {
    totalQuantity: number,
    totalPrice: number,
    items: CartItem[]
}

export interface CartItem {
    productId: number,
    name: string,
    quantity: number,
    price: number,
    imageUrl: string
}