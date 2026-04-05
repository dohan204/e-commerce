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
import { useMutation, useQueryClient } from '@tanstack/react-query'
import axios from 'axios'
import { useState, type ReactNode } from 'react'
import { toast } from 'sonner'

const Delete = ({ children, item}: { children: ReactNode, item: any }) => {
    const [open, setOpen] = useState(false);
    const useClient = useQueryClient();
    const mutation = useMutation({
        mutationFn: async () => {
            const response = await axios.delete(API_ENDPOINTS.USER.DELETE(item.id));
            if(!response)
                throw new Error("lỗi rồi");
        }, onSuccess: () => {
            toast.success('Xóa người dùng thành công', { position: 'top-center'})
            useClient.invalidateQueries({queryKey: ['users']})
            setOpen(false)
        }, onError: () => {
            toast.error("Xóa người dùng thất bại,  ", {position: 'top-center'})
        }
    })
    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                {children}
            </DialogTrigger>
            <DialogContent className='min-h-min'>
                <form onSubmit={() => mutation.mutate()}>
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