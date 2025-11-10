import { Plus } from 'lucide-react'
import { Button } from '../components/button'
import { useCategories } from '../hooks/categories/useCategories'
import { CategoryCard } from '../components/cardCategory'
import { useState } from 'react'
import type { Category } from '../services/category.services'
import { CategoryModal } from '../components/CategoryModal'
import { useDeleteCategory } from '../hooks/categories/useDeleteCategories'
import { ConfirmModal } from '../components/ConfrmModal'
import { CircularLoader } from '../components/loading'

export const Categories = () => {
  const { data, isLoading, error } = useCategories();
  const [modalOpen, setModalOpen] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState<Category | null>(
    null
  );
  const [categoryToDelete, setCategoryToDelete] = useState<Category | null>(
    null
  );
  const deleteMutation = useDeleteCategory();

  function handleNew() {
    setSelectedCategory(null);
    setModalOpen(true);
  }

  const handleEdit = (category: Category) => {
    setSelectedCategory(category);
    setModalOpen(true);
  }

  function handleAskDelete(category: Category) {
    setCategoryToDelete(category);
  }

  function handleCancel() {
    setCategoryToDelete(null);
  }

  function handleConfirm() {
    if (!categoryToDelete) return;
    deleteMutation.mutate(categoryToDelete.id, {
      onSuccess: () => setCategoryToDelete(null),
    });
  }


  if (isLoading) return (
    <div className='flex h-full items-center justify-center'>
      <CircularLoader/>
    </div>);
  if (error) return <p>{error.message}</p>;

    return(
        <>
        <div className='flex gap-x-5 h-15 justify-between p-2'>
            <h2 className='text-2xl font-semibold text-gray-600'>Categorias</h2> 
            <Button icon={<Plus/>}  onClick={() => handleNew()}>
              Adicionar
            </Button>
    
          </div>
          <div className='bg-gray-100 flex-1 p-3 rounded-lg gap-2.5 flex flex-col'>
          {data?.map((category) => (
            <CategoryCard key={`${category.id}-${category.name}`} category={category} handleEdit={handleEdit} handleAskDelete={handleAskDelete}/>
          ))}
          </div>

          <CategoryModal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        category={selectedCategory}
      />

        <ConfirmModal
          open={!!categoryToDelete}
          onCancel={handleCancel}
          onConfirm={handleConfirm}
          loading={deleteMutation.isPending}
          title="Excluir categoria"
          message={
            categoryToDelete
              ? `Tem certeza que deseja excluir "${categoryToDelete.name}"?`
              : "Tem certeza que deseja excluir?"
          }
          confirmLabel="Excluir"
          cancelLabel="Cancelar"
      />
        
        </>
    )
}