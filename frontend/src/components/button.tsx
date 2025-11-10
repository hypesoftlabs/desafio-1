import React from 'react';

// 1. Defina as props que o seu botão vai aceitar
type GradientButtonProps = {
  children: React.ReactNode; // O texto (ex: "Adicionar")
  icon?: React.ReactNode;   // O ícone (ex: <Plus />)
} & React.ButtonHTMLAttributes<HTMLButtonElement>; // Aceita props de botão (onClick, type, disabled)

export const Button = ({ 
  children, 
  icon, 
  ...props          // Passa todas as outras props (onClick, disabled, etc.)
}: GradientButtonProps) => {
  
  return (
    <button
      className={`                              
        bg-linear-to-r from-emerald-600 to-lime-500 
        cursor-pointer
        rounded-3xl
        text-white font-bold
        px-6                                  
        shadow-lg                                    
        
        /* --- Animações --- */
        transition-all duration-200 ease-in-out
        hover:-translate-y-0.5                       
        hover:shadow-xl                               
        active:translate-y-0 active:scale-95          

        /* --- Estado Desabilitado --- */
        disabled:opacity-50 disabled:cursor-not-allowed
        disabled:hover:-translate-y-0
      `}
      {...props} 
    >
    
      <div className='flex justify-center gap-2'>
        {icon && <span>{icon}</span>}
        <span>{children}</span>
      </div>
    </button>
  );
}