
import React from 'react';

type NavlinkProps = {
  children: React.ReactNode; 
  href: string;             
}

export const Navlink = ({ children, href }: NavlinkProps) => {
  return (
  
    <a href={href} className='flex space-x-6 hover:bg-green-50 p-3.5 rounded-lg'>
      {children}
    </a>
  );
}