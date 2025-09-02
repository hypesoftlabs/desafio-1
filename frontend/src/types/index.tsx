export interface Product {
  id: number
  name: string
  description: string
  price: number
  stock: number
  categoryId: number
}

export interface Category {
  id: number
  name: string
}