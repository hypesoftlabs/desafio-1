import { NavBar } from './navbar'
import { Header } from './header'
import { Outlet } from 'react-router-dom';


export const Layout = () => {
    return(
        <div className="h-screen w-screen flex flex-col p-4">
        <Header/>
        <div className='flex flex-1'>
          <aside className='w-64 '>
            <NavBar/>
          </aside>
          <main className='flex-1 pl-2.5 flex flex-col gap-2'>
           <Outlet/>
          </main>
        </div>
      </div>
    )
    
}