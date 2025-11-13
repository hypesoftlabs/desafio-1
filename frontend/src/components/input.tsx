import React from 'react';

// 1. Defina as Props
type RoundedInputProps = {

  icon?: React.ReactNode; 
} & React.InputHTMLAttributes<HTMLInputElement>; 

export const Input = ({ icon, className = '', ...props }: RoundedInputProps) => {
  return (

    <div 
      className={`
        flex items-center gap-2
        h-fit
        bg-gray-100 
        rounded-full 
        px-4 py-3 
        ${className}
      `}
    >

      {icon && (
        <span className="text-gray-400">
          {icon}
        </span>
      )}

      <input
        className="
          flex-1 
          w-full
          bg-transparent 
          focus:outline-none 
          placeholder:text-gray-500
        "
        {...props} 
      />
    </div>
  );
}