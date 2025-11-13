
import { cn } from "../lib/utils";

type CircularLoaderProps = {
  size?: number;      
  strokeWidth?: number; 
  className?: string;
};

export function CircularLoader({
  size = 32,
  strokeWidth = 4,
  className,
}: CircularLoaderProps) {
  const radius = (size - strokeWidth) / 2;
  const circumference = 2 * Math.PI * radius;

  return (
    <div
      className={cn(
        "inline-flex items-center justify-center",
        className
      )}
      style={{ width: size, height: size }}
      role="status"
      aria-label="Carregando"
    >
      <svg
        className="animate-spin"
        width={size}
        height={size}
      >
   
        <circle
          className="text-slate-200"
          stroke="currentColor"
          strokeWidth={strokeWidth}
          fill="transparent"
          r={radius}
          cx={size / 2}
          cy={size / 2}
        />
     
        <circle
          className="text-emerald-500"
          stroke="currentColor"
          strokeWidth={strokeWidth}
          strokeLinecap="round"
          fill="transparent"
          r={radius}
          cx={size / 2}
          cy={size / 2}
          strokeDasharray={circumference}
          strokeDashoffset={circumference * 0.75} 
        />
      </svg>
    </div>
  );
}
