export type product = {
    id: number,
    name: string,
    description: string,
    stock: number,
    sold: number,
    price: number,
    salePrice: number,
    avgRatings: number,
    reviews: Review[],
    categoryId: number,
    imageUrl: string,
    reviewCount: number
}

export type Review = {
    id: number,
    userId: string,
    productEntityId: number,
    rating: number,
    comment: string
}
export type ProductCard = Pick<product,'id'| 'name' | 'price' | 'avgRatings' | 'imageUrl'>