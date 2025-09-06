import UpdateCategoryForm from "@/components/forms/UpdateCategoryForm";
import { useCategories } from "@/hooks/useCategories";
import { useNavigate, useParams } from "react-router-dom";

const UpdateCategory = () => {
    const { id } = useParams<{ id: string }>();
    const { data: categories } = useCategories();
    const navigate = useNavigate();

    // Encontrar a categoria pelo id
    const category = categories?.find(
      (c) => c.id === Number(id)
    );

    if (!category)
      return <p>Category not found.</p>;
  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">
        Edit Category
      </h1>
      <UpdateCategoryForm category={category} onSuccess={() => navigate("/categories")} />
    </div>
  );
};

export default UpdateCategory;
