
import { NavBar } from './components/navbar'
import { Header } from './components/header'
import { Plus, Search } from 'lucide-react'
import { ProductCard } from './components/card'
import { Button } from './components/button'
import { Input } from './components/input'

function App() {
  return (
    <div className="h-screen w-screen flex flex-col p-4">
      <Header/>
      <div className='flex flex-1'>
        <aside className='w-64 '>
          <NavBar/>
        </aside>
        <main className='flex-1 pl-2.5 flex flex-col gap-2'>
          <div className='flex gap-x-5 h-15 justify-between p-2'>
            <h2 className='text-2xl font-semibold text-gray-600'>Produtos</h2>
            <Input icon={<Search/>} placeholder='Procurar...'/>
            <Button icon={<Plus/>}>
              Adicionar
            </Button>
    
          </div>
          <div className='bg-gray-100 flex-1 p-3 rounded-lg'>
            
            <ProductCard name="Teste" category='Teste' price={2} stock={2} />
          </div>
        
        </main>
      </div>
    </div>
  )
}

export default App
