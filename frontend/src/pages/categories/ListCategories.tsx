import { useCategories } from "@/hooks/useCategories";

export default function ListCategories() {
  const { data, isLoading, error } =
    useCategories();

  if (isLoading) return <p>Loading...</p>;
  if (error instanceof Error)
    return <p>Error: {error.message}</p>;

  return (
    <ul>
      {data?.map((category) => (
        <li key={category.id}>
          {category.name}
        </li>
      ))}
    </ul>
  );
}
