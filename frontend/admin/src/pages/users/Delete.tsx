import { Button } from '@/components/ui/button'
import { Dialog, 
    DialogTrigger,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogDescription,
    DialogClose,
    DialogFooter,
} from '@/components/ui/dialog'
import { API_ENDPOINTS } from '@/constants/urls'
import { authService } from '@/services/authService'
import React, { useState, type ReactNode } from 'react'
import { toast } from 'sonner'

const Delete = ({ children, item, refresh }: { children: ReactNode, item: any, refresh: () => void }) => {

    const [open, setOpen] = useState<boolean>(false)
    const url =  API_ENDPOINTS.USER.DELETE(item.id)
    const submit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            var isDelete = await fetch(url, {
                method: 'DELETE',
                headers: {
                    'Authorization': `${authService.getToken()}`
                }
            });
            if(!isDelete.ok)
                throw new Error("Xóa thất bại");
            toast.success("Xóa thành công", {position: 'top-center'});
            setOpen(false);
            refresh();
        } catch (err) {
            toast.error('Xóa không thành công');
            console.error(err)
        }
    }
    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                {children}
            </DialogTrigger>
            <DialogContent className='min-h-min'>
                <form onSubmit={submit}>
                    <DialogHeader>
                        <DialogTitle>
                            Xóa sản phẩm {item.name}?
                        </DialogTitle>
                        <DialogDescription>
                            Khi bạn đã xác nhận xóa thì vui lòng bấm XÓA!, còn không thì thôi.
                        </DialogDescription>
                    </DialogHeader>
                    <DialogFooter>
                        <DialogClose asChild>
                            <Button>
                                Hủy
                            </Button>
                        </DialogClose>
                        <Button type='submit'>
                            Xóa
                        </Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}

export default Delete