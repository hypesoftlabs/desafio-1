import CreateProductForm from "@/components/forms/CreateProductForm";

const CreateProduct = () => {
  return (
    <div className="flex flex-col gap-5 p-5">
      <h2 className="text-lg font-semibold">Create a new product</h2>
      <CreateProductForm />
    </div>
  );
};

export default CreateProduct;
