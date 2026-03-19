
export interface BaseResponse<T> {
  message: string,
  data: T[]
}
export interface Product {
  id: number,
  name: string,
  description: string | null,
  stock: number,
  sold: number
  price: number,
  salePrice: number | null,
  imageUrl: string | null,
  avgRating: number,
  reviewCount: number
  action: any
}

export type TopProductsSales = Pick<Product, "id" | "name" | "price" | "sold">; 

export type RatingProducts = Pick<Product, "id"| "name" | "description" | "avgRating" | "reviewCount">

export type TopSaleChart = {
  name: string,
  quantity: number
}