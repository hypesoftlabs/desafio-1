import { Product } from '@/services/product.services';
import React from 'react';

type ProductCardProps = {
  product: Product,
  categoryName: string,
  onEdit: (product : Product) => void,
  onDelete: (product : Product) => void
};

export const ProductCard = ({ product, categoryName, onEdit, onDelete}: ProductCardProps) => {
  return (
    <div className="bg-white flex flex-col gap-3.5 rounded-lg shadow-sm p-6 relative">
      <div className="absolute top-4 right-4 flex space-x-2">
        <button onClick={() => onEdit(product)} className="text-gray-400 hover:text-emerald-600 p-1 rounded-full transition-colors duration-200 cursor-pointer">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-5 w-5"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            strokeWidth={2}
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M15.232 5.232l3.536 3.536m-2.036-5.036a2.5 2.5 0 113.536 3.536L6.5 21.036H3v-3.536L16.732 3.732z"
            />
          </svg>
        </button>
        
        <button onClick={() => onDelete(product)} className="text-gray-400 hover:text-red-500 p-1 rounded-full transition-colors duration-200 cursor-pointer">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            className="h-5 w-5"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            strokeWidth={2}
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16"
            />
          </svg>
        </button>
      </div>
      <h3 className="text-xl font-semibold text-emerald-950">
        {product.name}
      </h3>
      <span className="text-md font-medium text-emerald-600 bg-emerald-100 px-4 py-1 w-fit rounded-full">
        {categoryName}
      </span>
      <div className="flex justify-between items-baseline">
  
        <span className="text-2xl font-bold text-gray-900">
          R$ {product.price.toFixed(2)}
        </span>
        <span className="text-sm text-gray-600">
          {product.quantity} em estoque
        </span>
      </div>
    </div>
  );
};