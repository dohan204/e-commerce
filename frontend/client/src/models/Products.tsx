export type product = {
    id: number,
    name: string,
    description: string,
    stock: number,
    sold: number,
    price: number,
    salePrice: number,
    avgRatings: number,
    categoryId: number,
    imageUrl: string,
    reviewCount: number
}

export type ProductCard = Pick<product,'id'| 'name' | 'price' | 'avgRatings' | 'imageUrl'>