import Icons from '@/components/icons'
import { Button } from '@/components/ui/button'
import {
    Dialog,
    DialogClose,
    DialogContent,
    DialogDescription,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger
} from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { zodResolver } from '@hookform/resolvers/zod'
// import React from 'react'
import { Controller, useForm, type SubmitHandler } from 'react-hook-form'
import * as z from 'zod';
import useFetch from '@/hooks/use-fetch'
import { type response } from '../categories/DataTable'
import { authService } from '@/services/authService'
import { toast } from 'sonner'
import { useState } from 'react'
const formData = z.object({
    name: z.string().nonempty(),
    description: z.string().optional(),
    stock: z.number(),
    price: z.number(),
    categoryId: z.string()
});


type schema = z.infer<typeof formData>;


const Create = ({refresh}: {refresh: () => void}) => {
    const { data} = useFetch<response>('http://localhost:5255/api/category/alls');
    const [open, setOpen] = useState<boolean>(false);
    const form = useForm<schema>({
        resolver: zodResolver(formData),
        defaultValues: {
            name: '',
            description: '',
            stock: 0,
            price: 0,
            categoryId: ''
        }
    })

    const submitForm: SubmitHandler<schema> = async (data: schema) => {
        const convert = { ...data, categoryId: +data.categoryId }

        try {
            await fetch("http://localhost:5255/api/product", {
                method: "POST",
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${authService.getToken()}`
                },
                body: JSON.stringify(convert)
            });

            toast.success("Tạo sản phẩm thành công", {position: 'top-center'});
            refresh()
            setOpen(true);
            form.reset();
        } catch (error) {
            console.log(error)
        }
    }
    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <Button variant={'outline'}>
                    <Icons name='plus' />
                    Tạo mới
                </Button>
            </DialogTrigger>
            <DialogContent className='w-max'>
                <form onSubmit={form.handleSubmit(submitForm)}>
                    <DialogHeader>
                        <DialogTitle>Thêm sản phẩm mới</DialogTitle>
                        <DialogDescription>Thêm sản phẩm mới tại đây, sau đó click tạo để tạo mới.</DialogDescription>
                    </DialogHeader>
                    <div className='flex flex-col gap-4'>
                        <Controller
                            name='name'
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div className='grid gap-2'>
                                    <Label>Tên Sản phẩm</Label>
                                    <Input
                                        {...field}
                                        id='name'
                                        placeholder='Nhập tên sản phẩm'
                                        required
                                    />
                                    {fieldState.error ? <p className='text-destructive font-sans text-sm'>{fieldState.error.message}</p> : ''}
                                </div>
                            )}
                        />
                        <Controller
                            name='description'
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div className='grid gap-2'>
                                    <Label>Mô tả sản phẩm</Label>
                                    <Input
                                        {...field}
                                        id='description'
                                        placeholder='Mô tả'
                                        // required
                                    />
                                    {fieldState.error ? <p className='text-destructive font-sans text-sm'>{fieldState.error.message}</p> : ''}
                                </div>
                            )}
                        />
                        <Controller
                            name='price'
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div className='grid gap-2'>
                                    <Label>Giá bán</Label>
                                    <Input
                                        {...field}
                                        id='price'
                                        type='number'
                                        value={field.value}
                                        onChange={(value) => field.onChange(+value.target.value)}
                                        placeholder='Nhập giá bán'
                                        required
                                    />
                                    {fieldState.error ? <p className='text-destructive font-sans text-sm'>{fieldState.error.message}</p> : ''}
                                </div>
                            )}
                        />
                        <Controller
                            name='stock'
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div className='grid gap-2'>
                                    <Label>Số lượng</Label>
                                    <Input
                                        {...field}
                                        id='stock'
                                        type='number'
                                        value={field.value}
                                        onChange={(value) => field.onChange(+value.target.value)}
                                        placeholder='Số lượng'
                                        required
                                    />
                                    {fieldState.error ? <p className='text-destructive font-sans text-sm'>{fieldState.error.message}</p> : ''}
                                </div>
                            )}
                        />
                        <Controller
                            name='categoryId'
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div className='grid gap-2'>
                                    <Label>Danh mục</Label>
                                    <Select onValueChange={field.onChange} value={field.value} >
                                        <SelectTrigger className="w-full max-w-88">
                                            <SelectValue placeholder='Chọn danh mục' />
                                        </SelectTrigger>
                                        <SelectContent>
                                            {data.map((item) => (
                                                <SelectItem key={item.id} value={item.id.toString()}>{item.name}</SelectItem>
                                            ))}
                                        </SelectContent>
                                    </Select>
                                    {fieldState.error ? <p className='text-destructive font-sans text-sm'>{fieldState.error.message}</p> : ''}
                                </div>
                            )}
                        />
                    </div>
                    <DialogFooter>
                        <DialogClose asChild>
                            <Button variant="outline">Hủy</Button>
                        </DialogClose>
                        <Button type="submit" onClick={() => alert('hello')}>Tạo mới</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}

export default Create