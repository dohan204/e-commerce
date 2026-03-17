import React, { type ReactNode, type ComponentProps } from 'react'
import { cn } from "@/lib/utils" // Thường có sẵn trong dự án shadcn/ui

// Kế thừa tất cả thuộc tính của thẻ div (onClick, onMouseEnter, style,...)
interface CustomCardProps extends ComponentProps<"div"> {
  children: ReactNode;
}

const CustomCard = ({ children, className, ...props }: CustomCardProps) => {
  return (
    <div 
      className={cn(
        className='w-full min-h-[160px] sm:min-h-[200px] p-6 flex flex-col items-center justify-center shadow-xl bg-gray-100 hover:bg-blue-400 rounded-xl transition-all', 
        className // Cho phép ghi đè hoặc thêm class từ bên ngoài
      )}
      {...props} // Truyền các props còn lại (như onClick, id...) vào thẻ div
    >
        {children}
    </div>
  )
}

export default CustomCard
