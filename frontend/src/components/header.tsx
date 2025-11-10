import logo from '../assets/logo.png'

export const Header = () => {
return(
<header className='h-20 '>
    <div className='flex items-center  w-64'>
    <img src={logo} alt="logo" className='h-20' />
    <h1 className='text-2xl font-bold'>Shop</h1>
    <h1 className='text-2xl font-bold text-gray-600'>Manager</h1>
    </div>
  </header>
)
}