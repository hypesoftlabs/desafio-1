import { Plus } from 'lucide-react'
import { ProductCard } from '../components/card'
import { Button } from '../components/button'
import { StockCard } from '../components/stockCard'


export const Stock = () => {
    return(
        <>
        <div className='flex gap-x-5 h-15 justify-between p-2'>
            <h2 className='text-2xl font-semibold text-gray-600'>Estoque</h2> 
          </div>
          <div className='bg-gray-100 flex-1 p-3 rounded-lg'>
            
            <StockCard productName="Camiseta Básica P" initialQuantity={5} />
          </div>
        
        </>
    )
}