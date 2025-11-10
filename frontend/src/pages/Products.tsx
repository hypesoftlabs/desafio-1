import { Plus } from 'lucide-react'
import { ProductCard } from '../components/card'
import { Button } from '../components/button'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "../components/select"
import { useProducts } from '../hooks/useProducts'

export const Products = () => {

  const data = useProducts()
  console.log(data)
    return(
        <>
        <div className='flex gap-x-5 h-15 justify-between p-2'>
            <h2 className='text-2xl font-semibold text-gray-600'>Produtos</h2>

            <Select>
              <SelectTrigger className="w-[180px]">
                <SelectValue placeholder="Theme" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="light">Light</SelectItem>
                <SelectItem value="dark">Dark</SelectItem>
                <SelectItem value="system">System</SelectItem>
              </SelectContent>
            </Select>
            
            <Button icon={<Plus/>}>
              Adicionar
            </Button>
    
          </div>
          <div className='bg-gray-100 flex-1 p-3 rounded-lg'>
            
            <ProductCard name="Teste" category='Teste' price={2} stock={2} />
          </div>
        
        </>
    )
}