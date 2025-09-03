"use client";

import {
  useParams,
  useNavigate,
} from "react-router-dom";
import { useProducts } from "@/hooks/useProducts";
import { UpdateProductForm } from "@/components/forms/updateProductForm";

export default function UpdateProduct() {
  const { id } = useParams<{ id: string }>();
  const { data: products } = useProducts();
  const navigate = useNavigate();

  // Encontrar o produto pelo id
  const product = products?.find(
    (p) => p.id === Number(id)
  );

  if (!product) return <p>Product not found.</p>;

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">
        Edit Product
      </h1>
      <UpdateProductForm
        product={product}
        onSuccess={() => navigate("/products")}
      />
    </div>
  );
}
