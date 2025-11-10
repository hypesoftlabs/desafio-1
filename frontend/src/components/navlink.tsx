
import React from 'react';
import { NavLink } from 'react-router-dom';

type NavlinkProps = {
  children: React.ReactNode; 
  href: string;             
}

export const Navlink = ({ children, href }: NavlinkProps) => {


  const baseClasses = 'flex space-x-6 p-3.5 rounded-lg';
    return (
    <NavLink 
          to={href} 
          className={({ isActive }) => 
            `${baseClasses} ${ isActive ? 'bg-green-50 text-emerald-700' : 'hover:bg-gray-100' }`
          }
        >
        {children}
    </NavLink> );
}