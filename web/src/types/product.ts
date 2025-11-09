export interface Product {
  id?: string;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  categoryId: string;
  category?: {
    id: string;
    name: string;
  };
}