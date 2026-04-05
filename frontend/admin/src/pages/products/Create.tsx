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
// import { type response } from '../categories/DataTable'
import { authService } from '@/services/authService'
import { toast } from 'sonner'
import { useState } from 'react'
import { data } from 'react-router'
const formData = z.object({
    name: z.string().nonempty(),
    description: z.string().optional(),
    stock: z.number(),
    price: z.number(),
    categoryId: z.coerce.number(),
    imageUrl: z.any(),
    tag: z.string(),
});


type schema = z.input<typeof formData>;


const Create = ({ refresh, categories }: { refresh: () => void, categories?: any[] }) => {
    const [open, setOpen] = useState<boolean>(false);
    const form = useForm<schema>({
        resolver: zodResolver(formData),
        defaultValues: {
            name: '',
            description: '',
            stock: 0,
            price: 0,
            categoryId: 0,
            imageUrl: null,
            tag: ''
        }
    })



    const submitForm: SubmitHandler<schema> = async (data: schema) => {


        const forms = new FormData();
        forms.append('name', data.name);
        forms.append('description', data.description || '');
        forms.append('stock', data.stock.toString());
        forms.append('price', data.price.toString());
        forms.append('categoryId', data.categoryId as string);
        forms.append('tag', data.tag);
        if (data.imageUrl) {
            forms.append('file', data.imageUrl);
        }

        console.log(Object.fromEntries(forms.entries()))
        try {
            await fetch("http://localhost:5255/api/product", {
                method: "POST",
                headers: {
                    'Authorization': `Bearer ${authService.getToken()}`
                },
                body: forms
            });

            toast.success("Tạo sản phẩm thành công", { position: 'top-center' });
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
                <Button variant={'outline'} className={'bg-lime-300'}>
                    <Icons name='plus' />
                    Tạo mới
                </Button>
            </DialogTrigger>
            <DialogContent className="w-[50vw] sm:max-w-5xl">
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
                                    <Select onValueChange={field.onChange} value={(field.value as string)} >
                                        <SelectTrigger className="w-full max-w-88">
                                            <SelectValue placeholder='Chọn danh mục' />
                                        </SelectTrigger>
                                        <SelectContent>
                                            {categories?.map((item) => (
                                                <SelectItem key={item.id} value={item.id.toString()}>{item.name}</SelectItem>
                                            ))}
                                        </SelectContent>
                                    </Select>
                                    {fieldState.error ? <p className='text-destructive font-sans text-sm'>{fieldState.error.message}</p> : ''}
                                </div>
                            )}
                        />
                        <Controller
                            name="imageUrl"
                            control={form.control}
                            render={({ field: { onChange, value, ...field } }) => (
                                <div className='grid gap-2'>
                                    <Label>Hình ảnh</Label>
                                    <Input
                                        type='file'
                                        accept='image/*'
                                        onChange={(e) => {
                                            const file = e.target.files?.[0];
                                            if (file) onChange(file)
                                        }}
                                        {...field}
                                    />
                                </div>
                            )}
                        />
                        <Controller
                            name='tag'
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div className='grid gap-2'>
                                    <Label>Số lượng</Label>
                                    <Input
                                        {...field}
                                        id='tag'
                                        placeholder='kiểu sản phẩm...'
                                        required
                                    />
                                    {fieldState.error ? <p className='text-destructive font-sans text-sm'>{fieldState.error.message}</p> : ''}
                                </div>
                            )}
                        />
                    </div>
                    <DialogFooter>
                        <DialogClose asChild>
                            <Button variant="outline">Hủy</Button>
                        </DialogClose>
                        <Button type="submit">Tạo mới</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}

export default Create