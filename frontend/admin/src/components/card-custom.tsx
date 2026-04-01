import { type ReactNode, type ComponentProps } from 'react'
import { cn } from "@/lib/utils"

interface CustomCardProps extends ComponentProps<"div"> {
  title: string;
  value?: number | string;
  icon?: ReactNode;
}

const CustomCard = ({ title, value, icon, className, ...props }: CustomCardProps) => {
  return (
    <div
      className={cn(
        "w-full min-h-[140px] p-5 rounded-2xl shadow-md bg-emerald-500 text-white flex items-center justify-between transition-all hover:scale-[1.02] hover:shadow-xl",
        className
      )}
      {...props}
    >
      <div className="flex flex-col gap-1">
        <p className="text-sm opacity-80">{title}</p>
        <h2 className="text-2xl font-bold">{value ?? 0}</h2>
      </div>

      <div className="bg-white/20 p-3 rounded-full">
        {icon}
      </div>
    </div>
  )
}

export default CustomCard