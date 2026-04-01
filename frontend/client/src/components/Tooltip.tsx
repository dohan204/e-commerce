import React, { type ReactNode } from 'react'
import { Tooltip, TooltipContent, TooltipTrigger } from './ui/tooltip'

const Tooltips = ({children, description}: {children: ReactNode, description: string}) => {
  return (
    <Tooltip>
        <TooltipTrigger>{children}</TooltipTrigger>
        <TooltipContent>
            <p>{description}</p>
        </TooltipContent>
    </Tooltip>
  )
}

export default Tooltips