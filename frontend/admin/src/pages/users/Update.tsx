import { Dialog, DialogTrigger } from '@/components/ui/dialog'
import React, { type ReactNode } from 'react'

const Update = ({children, item}: {children: ReactNode, item: any}) => {
  return (
    <Dialog>
        <DialogTrigger asChild>
            {children}
        </DialogTrigger>
    </Dialog>
  )
}

export default Update