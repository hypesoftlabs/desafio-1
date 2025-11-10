import { type ClassValue, clsx } from "clsx"
import { twMerge } from "tailwind-merge"

/**
 * Combina nomes de classe de forma inteligente.
 * @param {...ClassValue} inputs - As classes a serem combinadas.
 * @returns {string} Uma string com as classes mescladas e resolvidas.
 */
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}