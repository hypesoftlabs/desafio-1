import { useProducts } from "../../hooks/useProducts";

export default function ListProducts() {
  const { data, isLoading, error } =
    useProducts();

  if (isLoading) return <p>Loading...</p>;
  if (error instanceof Error)
    return <p>Error: {error.message}</p>;

  return (
    <ul>
      {data?.map((product) => (
        <li key={product.id}>
          {product.name} - ${product.price}
        </li>
      ))}
    </ul>
  );
}
