import api from "../lib/api";

export interface Category {
  id: string;
  name: string;
}

export type CreateCategoryInput = {
  name: string;
};

export type UpdateCategoryInput = {
  id: string;
  name: string;
};

export async function getCategories(): Promise<Category[]> {
  const { data } = await api.get<Category[]>("/api/category");
  return data;
}

export async function createCategory(
  input: CreateCategoryInput
): Promise<Category> {
  const { data } = await api.post<Category>("/api/category", input);
  return data;
}

export async function updateCategory(
  input: UpdateCategoryInput
): Promise<Category> {
  const { data } = await api.put<Category>(
    `/api/category/${input.id}`,
    {   id: input.id,
        name: input.name }
  );
  return data;
}

export async function deleteCategory(id: string): Promise<void> {
  await api.delete(`/api/category/${id}`);
}
