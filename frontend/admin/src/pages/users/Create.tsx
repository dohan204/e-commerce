import Icons from "@/components/icons";
import { Button } from "@/components/ui/button";
import { Dialog, DialogClose, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog"
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { authService } from "@/services/authService";
import { zodResolver } from "@hookform/resolvers/zod";
import { useState } from "react"
import { useForm } from "react-hook-form";
import { toast } from "sonner";
import { schema, type User } from "@/models/users";
import { API_ENDPOINTS } from "@/constants/urls";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import axios from "axios";

const Create = () => {
    const [open, setOpen] = useState<boolean>(false);
    const [error, setError] = useState<Error | null>(null)
    const form = useForm<User>({
        resolver: zodResolver(schema),
        defaultValues: {
            userName: '',
            email: '',
            password: '',
        }
    })

    const client = useQueryClient();
    const mutation = useMutation({
        mutationFn: async (data: User) => {
            const response = await axios.post(API_ENDPOINTS.USER.CREATEUSER, data, {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }
            });

            if(!response)
                throw new Error("Lỗi rồi");
        },
        onSuccess: () => {
            toast.success("Tạo người dùng thành công.", {position: 'top-center'});
            client.invalidateQueries({queryKey: ['users']});
            form.reset();
            setOpen(false);
        },
        onError: () => {
            toast.error("Tạo người dùng thất bại.", {position: 'top-center'});
        }
    })

    const onSumbit = (data: User) => {
        mutation.mutate(data);
    }
    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <Button variant={'outline'}>
                    <Icons name='plus' />
                    Tạo mới
                </Button>
            </DialogTrigger>
            <DialogContent>
                
                <form onSubmit={form.handleSubmit(onSumbit)}>
                    <DialogHeader>
                        <DialogTitle>
                            Tạo người dùng
                        </DialogTitle>
                        <DialogDescription>
                            Xác nhận thì nhấn tạo nhé ^^
                        </DialogDescription>
                    </DialogHeader>
                    <div className="grid gap-4 mt-4">
                        {/* Username */}
                        <div className="grid gap-2">
                            <Label htmlFor="userName">Tài khoản</Label>
                            <Input id="userName" {...form.register("userName")} placeholder="nhập tài khoản" />
                            {form.formState.errors.userName && (
                                <p className="text-sm text-destructive">{form.formState.errors.userName.message}</p>
                            )}
                        </div>

                        {/* Email */}
                        <div className="grid gap-2">
                            <Label htmlFor="email">Email</Label>
                            {error && error.message === 'Conflict' ? <span className="text-destructive text-error">Email đã tồn tại</span> : ''}
                            <Input id="email" type="email" {...form.register("email")} placeholder="example@gmail.com" />
                            {form.formState.errors.email && (
                                <p className="text-sm text-destructive">{form.formState.errors.email.message}</p>
                            )}
                        </div>

                        {/* Password */}
                        <div className="grid gap-2">
                            <Label htmlFor="password">Mật khẩu</Label>
                            <Input id="password" type="password" {...form.register("password")} placeholder="******" />
                            {form.formState.errors.password && (
                                <p className="text-sm text-destructive">{form.formState.errors.password.message}</p>
                            )}
                        </div>
                    </div>
                    <DialogFooter>
                        <DialogClose asChild>
                            <Button type="button">Hủy</Button>
                        </DialogClose>
                        <Button type='submit' disabled={mutation.isPending}>{mutation.isPending ? 'Đang tạo...' : 'Tạo mới' }</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}

export default Create