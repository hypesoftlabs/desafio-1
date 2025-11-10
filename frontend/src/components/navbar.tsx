import { FolderOpen } from "lucide-react"
import { Box, Warehouse } from "lucide-react"
import { ChartBarBig } from "lucide-react"
import { Navlink } from "./navlink"

export const NavBar = () => {
return(
    <nav className='flex flex-col space-y-4 p-2'>
            <Navlink href='/'>
              <ChartBarBig />
              <p className='font-semibold'>Dashboard</p>
            </Navlink>
            <Navlink href='#'>
              <Warehouse />
              <p className='font-semibold'>Estoque</p>
            </Navlink>
            <Navlink href='#'>
              <Box />
              <p className='font-semibold'>Produtos</p>
            </Navlink>
            <Navlink href='#'>
              <FolderOpen />
              <p className='font-semibold'>Categorias</p>
            </Navlink>
          </nav>
)
}