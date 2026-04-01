"use client" // Đảm bảo có dòng này nếu dùng Next.js App Router

import React, { useState } from 'react'
import { Button } from "@/components/ui/button"
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Controller, useForm } from 'react-hook-form'
import { zodResolver } from "@hookform/resolvers/zod"
import * as z from 'zod'
import { authService } from '@/services/authService'
import { toast } from 'sonner'
import { API_ENDPOINTS } from '@/constants/urls'

// Shadcn thường không có sẵn component Field/FieldError trừ khi ông tự build 
// hoặc dùng gói Form chuyên dụng của nó. Tôi sẽ dùng Label + Span lỗi cho đơn giản.

const formSchema = z.object({
  name: z.string().min(1, { message: "Tên không được để trống" }),
  image: z.any()
})

type FormValues = z.infer<typeof formSchema>

export default function Update({ refresh , item }: { refresh: () => void; item: any }) {
  const [open, setOpen] = useState<boolean>(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<Error | null>(null);
  // const [success, setSuccess] = useState<boolean>(false);
  const form = useForm<FormValues>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      name: ''
    }
  })
  const url = API_ENDPOINTS.CATEGORY.CREATE
  const onSubmit = async (data: FormValues) => {

    const formData = new FormData();

    formData.append('name', data.name);
    if(data.image)
      formData.append('file', data.image)
    setLoading(true)
    try {
      await fetch(url, {
        method: 'POST',
        headers: {
          // 'Content-type': 'application/json',
          'Authorization': `Bearer ${authService.getToken()}`
        },
        body: formData
      });
      toast.success('Tạo mới thành công', { position: 'top-center' });
      refresh();
      setOpen(false)
      form.reset();

    } catch (error) {
      setError(error as Error)
      toast.error('Tạo thất bại!')
    } finally {
      setLoading(false)
    }
  }
  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button variant="outline">Tạo mới</Button>
      </DialogTrigger>
      <DialogContent className="sm:max-w-sm">
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <DialogHeader>
            <DialogTitle>Tạo danh mục mới</DialogTitle>
            <DialogDescription>
              Nhập tên danh mục bạn muốn tạo ở đây. Nhấn Save để hoàn tất.
            </DialogDescription>
          </DialogHeader>

          <div className="grid gap-4 py-4">
            <Controller
              name='name'
              control={form.control}
              render={({ field, fieldState }) => (
                <div className="grid gap-2">
                  <Label htmlFor="name" className={fieldState.error ? "text-destructive" : ""}>
                    Tên Danh mục
                  </Label>
                  <Input
                    {...field}
                    id="name"
                    placeholder="Ví dụ: Đồ điện tử..."
                    className={fieldState.error ? "border-destructive focus-visible:ring-destructive" : ""}
                  />
                  {fieldState.error && (
                    <p className="text-sm font-medium text-destructive">
                      {fieldState.error.message}
                    </p>
                  )}
                </div>
              )}
            />
            <Controller
              name="image"
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
          </div>

          <DialogFooter>
            <DialogClose asChild>
              <Button type="button" variant="outline">Cancel</Button>
            </DialogClose>
            <Button type="submit">{loading ? 'Đang tạo' : 'Tạo mới'}</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}