import { Button } from '@/components/ui/button';
import { Dialog, DialogClose, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog'
import { authService } from '@/services/authService';
import React, { type ReactNode } from 'react'
// import { useForm } from 'react-hook-form';
import { toast } from 'sonner';

const Delete = ({ children, item, refresh }: { children: ReactNode, item: any, refresh: () => void }) => {
    const [open, setOpen] = React.useState<boolean>(false);

    const submitForm = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const res = await fetch(`http://localhost:5255/api/product/${item.id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`
                }
            });

            if(!res.ok) throw new Error("Delete Faild");
            toast.success("Xóa thành công", {position: 'top-center'});
            refresh();
            setOpen(false);
        } catch (err) {
            toast.error("Xóa thất bại", { position: 'top-center' });
            console.log(err);
        }
    }
    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                {children}
            </DialogTrigger>
            <DialogContent className='min-h-min'>
                <form onSubmit={submitForm}>
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