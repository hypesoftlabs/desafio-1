import { LogOut, Search } from 'lucide-react'
import logo from '../assets/logo.png'
import { Input } from './input'

export const Header = () => {
return(
<header className='h-20 flex text-center items-center'>
    <div className='flex items-center  w-64'>
      <img src={logo} alt="logo" className='h-20' />
      <h1 className='text-2xl font-bold'>Shop</h1>
      <h1 className='text-2xl font-bold text-gray-600'>Manager</h1>
    </div>
    <Input icon={<Search/>} placeholder='Pesquisar Produto...'/>
    <div className='flex items-center justify-self-end ml-auto p-2 gap-6'>
      <span className='bg-emerald-500 rounded-full text-xl w-12 h-12 place-content-center text-center font-semibold text-white'>
        NM
      </span>
      <div className='flex items-start flex-col'>
        <h2 className='font-semibold'>Nome da Pessoa</h2>
        <h2 className='text-gray-500'>Role</h2>
      </div>
      <LogOut className='text-red-600 cursor-pointer'/>
  
    </div>
  </header>
)
}