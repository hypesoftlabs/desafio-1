import CreateCategoryForm from "@/components/forms/CreateCategoryForm";

const CreateCategory = () => {
  return (
    <div className="flex flex-col gap-5 p-5">
      <h1 className="text-lg font-semibold">
        Create a new category
      </h1>
      <CreateCategoryForm />
    </div>
  );
};

export default CreateCategory;
