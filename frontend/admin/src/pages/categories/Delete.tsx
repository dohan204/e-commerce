import { AlertDialog, AlertDialogAction, AlertDialogCancel, AlertDialogContent, AlertDialogDescription, AlertDialogFooter, AlertDialogHeader, AlertDialogMedia, AlertDialogTitle, AlertDialogTrigger } from '@/components/ui/alert-dialog'
import { Button } from '@/components/ui/button';
import { API_ENDPOINTS } from '@/constants/urls';
import { Trash } from 'lucide-react';
import React, { useState, type ReactNode } from 'react'
import { toast } from 'sonner';
import type { response } from './DataTable';
import { authService } from '@/services/authService';

const Delete = ({children, item, refresh}: {children: ReactNode, item: response, refresh: () => void}) => {
    const [open, setOpen] = useState(false);
    const handleSubmit = async () => {
        try {
            const res = await fetch(API_ENDPOINTS.CATEGORY.DELETE(item.id), {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`
                }
            })
            if(!res.ok)
                throw new Error("Delete failed")

            toast.success("Xóa thành công.");
            refresh();
            setOpen(false);
        } catch (err) {
            toast.error("Xóa thất bại.");
            console.error(err);
        }
    } 
  return (
    <AlertDialog open={open} onOpenChange={setOpen}>
        <AlertDialogTrigger asChild>
            <Button variant={'destructive'}>{children}</Button>
        </AlertDialogTrigger>
        <AlertDialogContent>
            <AlertDialogHeader>
                <AlertDialogMedia>
                    <Trash className='text-destructive bg-destructive/10 dark:bg-destructive/20 dark:text-destructive' />
                </AlertDialogMedia>
                <AlertDialogTitle>Bạn có chắc là xóa danh mục này?</AlertDialogTitle>
                <AlertDialogDescription>
                    Nếu xác nhận xóa, vui lòng nhấn nút xóa để xóa nhé!!
                </AlertDialogDescription>
            </AlertDialogHeader>
            <AlertDialogFooter>
                <AlertDialogCancel>Hủy</AlertDialogCancel>
                <AlertDialogAction 
                    variant={'destructive'}
                    onClick={handleSubmit}
                >Xóa
                </AlertDialogAction>
            </AlertDialogFooter>
        </AlertDialogContent>
    </AlertDialog>
  )
}

export default Delete