export interface Product {
  id?: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: number;
}

export interface Category {
  id: number;
  name: string;
}

export interface CategoryCreate {
  name: string;
}

export interface CategoryUpdate {
  id: number;
  name: string;
}

export interface TotalProductsResponse {
  totalProducts: number;
}

export interface ProductsByCategoryResponse {
  productsByCategory: Record<string, number>; // ou array de objetos se você quiser nome + qtd
}

export interface TotalStockValueResponse {
  totalStockValue: number;
}

export interface LowStockResponse {
  lowStockProducts: number;
}