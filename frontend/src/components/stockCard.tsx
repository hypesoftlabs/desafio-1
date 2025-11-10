import { Minus, Plus } from 'lucide-react';
import React, { useState } from 'react';
import { Input } from './input';

type StockCardProps = {
  productName: string;
  initialQuantity?: number; 
};

export const StockCard = ({ productName, initialQuantity = 0 }: StockCardProps) => {
  const [quantity, setQuantity] = useState(initialQuantity);

  const handleDecrease = () => {
    setQuantity(prevQuantity => Math.max(0, prevQuantity - 1)); 
  };

  const handleIncrease = () => {
    setQuantity(prevQuantity => prevQuantity + 1);
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseInt(e.target.value, 10);
    setQuantity(value);

  };

  return (
    <div className="bg-white shadow-sm rounded-lg p-6 max-w-sm mx-auto border border-gray-200">
      <h3 className="text-xl font-semibold text-gray-800 mb-4 text-center">
        {productName}
      </h3>

      <div className="flex items-center justify-center space-x-2">
        <button
          onClick={handleDecrease}
          className="bg-red-500 hover:bg-red-600 cursor-pointer text-white font-bold py-2 px-4 rounded-full transition-colors duration-200 disabled:opacity-50 disabled:cursor-not-allowed"
          disabled={quantity === 0} 
        >
          <Minus/>
        </button>

        <Input 
          type="number"
          min="0"
          value={quantity}
          onChange={handleChange}/>
          <button
            onClick={handleIncrease}
            className="bg-emerald-500 hover:bg-emerald-600 text-white font-bold py-2 px-4 rounded-full transition-colors cursor-pointer duration-200"
          >
          <Plus/>
        </button>
      </div>

    </div>
  );
};

