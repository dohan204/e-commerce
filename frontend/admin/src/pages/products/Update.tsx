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
import React, { useState, type ReactNode } from 'react'

import { Controller, useForm } from 'react-hook-form'
import * as z from 'zod'
import { zodResolver } from '@hookform/resolvers/zod'
import { Label } from '@/components/ui/label'
import { Input } from '@/components/ui/input'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from '@/components/ui/select'
import { API_ENDPOINTS } from '@/constants/urls'
import { authService } from '@/services/authService'
import { toast } from 'sonner'
import { useMutation, useQueryClient } from '@tanstack/react-query'
// import { categories } from '../categories/DataTable'

/* ✅ FIX schema */
const schema = z.object({
  id: z.number().optional(),
  name: z.string().optional(),
  description: z.string().optional(),
  price: z.number().optional(),
  salePrice: z.number().optional(),
  stock: z.number().optional(),
  categoryId: z.number().optional(),

  /* 🔥 file phải là File */
  imageFile: z.any().optional()
})

type ModelUpdate = z.infer<typeof schema>

export default function Update({
  children,
  item,
  refresh,
  categories
}: {
  children: ReactNode
  item: any,
  refresh: () => void,
  categories?: any[]
}) {
  const form = useForm<ModelUpdate>({
    resolver: zodResolver(schema),
    defaultValues: {
      id: item.id,
      name: item.name,
      description: item.description!,
      price: item.price,
      salePrice: item.salePrice ?? 0,
      stock: item.stock ?? 0,


      categoryId: item.categoryId
    }
  })

  const [open, setOpen] = useState(false)



  const userClient = useQueryClient();
  const mutation = useMutation({
    mutationFn: async (values: ModelUpdate): Promise<void> => {
      const formData = new FormData()

      formData.append('id', String(item.id))
      formData.append('name', values.name ?? '')
      formData.append('description', values.description ?? '')
      formData.append('price', String(values.price ?? 0))
      formData.append('salePrice', String(values.salePrice ?? 0))
      formData.append('stock', String(values.stock ?? 0))
      formData.append('categoryId', String(values.categoryId ?? 0))

      if (values.imageFile) {
        formData.append('file', values.imageFile)
      }
      const response = await fetch(API_ENDPOINTS.PRODUCT.UPDATE, {
        method: 'PUT',
        headers: {
          // 'Content-Type': 'application/json',
          'Authorization': `Bearer ${authService.getToken()}`
        },
        body: formData
      })

      if (!response.ok)
        throw new Error("update failed");

    },
    onSuccess: () => {
      toast.success("Cập nhật thành công", { position: 'top-center' });
      userClient.invalidateQueries({ queryKey: ['products'] })
      setOpen(false);
    },
    onError: (error) => {
      toast.error("Cập nhật thất bại.", { position: 'top-center' });
      console.error(error);
    }
  })

  const onSubmit = async (values: ModelUpdate) => {
    mutation.mutate(values);
  }
  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>{children}</DialogTrigger>

      <DialogContent className="max-w-none w-[800px]">
        <form onSubmit={form.handleSubmit(onSubmit)}>
          <DialogHeader>
            <DialogTitle>Cập nhật sản phẩm</DialogTitle>
            <DialogDescription>
              Thực hiện thay đổi thông tin sản phẩm ở đây
            </DialogDescription>
          </DialogHeader>

          <div className="grid sm:grid-cols-1 md:grid-cols-2 gap-x-4 gap-y-2 mt-4">
            {/* NAME */}
            <Controller
              name="name"
              control={form.control}
              render={({ field }) => (
                <div className="grid gap-2">
                  <Label>Tên sản phẩm</Label>
                  <Input {...field} placeholder="Nhập tên sản phẩm" />
                </div>
              )}
            />

            {/* DESCRIPTION */}
            <Controller
              name="description"
              control={form.control}
              render={({ field }) => (
                <div className="grid gap-2">
                  <Label>Mô tả</Label>
                  <Input {...field} placeholder="Mô tả" />
                </div>
              )}
            />

            {/* PRICE */}
            <Controller
              name="price"
              control={form.control}
              render={({ field }) => (
                <div className="grid gap-2">
                  <Label>Giá</Label>
                  <Input
                    type="number"
                    value={field.value ?? ''}
                    onChange={(e) =>
                      field.onChange(Number(e.target.value))
                    }
                  />
                </div>
              )}
            />

            {/* SALE PRICE */}
            <Controller
              name="salePrice"
              control={form.control}
              render={({ field }) => (
                <div className="grid gap-2">
                  <Label>Giảm giá</Label>
                  <Input
                    type="number"
                    value={field.value ?? ''}
                    onChange={(e) =>
                      field.onChange(Number(e.target.value))
                    }
                  />
                </div>
              )}
            />

            {/* FILE */}
            <Controller
              name="imageFile"
              control={form.control}
              render={({ field: { onChange, value, ...field } }) => (
                <div className="grid gap-2">
                  <Label>Hình ảnh</Label>

                  {/* Hiển thị ảnh: Ưu tiên ảnh mới chọn (preview), nếu không có thì hiện ảnh cũ */}
                  {(value || item.imageUrl) && (
                    <img
                      src={value ? URL.createObjectURL(value) : item.imageUrl}
                      alt="Preview"
                      className="w-20 h-20 object-cover rounded border"
                    />
                  )}

                  <Input
                    type="file"
                    accept="image/*" // Chỉ cho phép chọn file ảnh
                    onChange={(e) => {
                      const file = e.target.files?.[0];
                      if (file) onChange(file); // Cập nhật file vào form state
                    }}
                    {...field} // Spread các props còn lại nhưng không bao gồm value
                  />
                </div>
              )}
            />

            {/* CATEGORY */}
            <Controller
              name="categoryId"
              control={form.control}
              render={({ field }) => (
                <div className="grid gap-2">
                  <Label>Danh mục</Label>
                  <Select
                    onValueChange={(val) =>
                      field.onChange(Number(val))
                    }
                    value={field.value?.toString()}
                  >
                    <SelectTrigger>
                      <SelectValue placeholder="Chọn danh mục" />
                    </SelectTrigger>
                    <SelectContent>
                      {categories?.map((c) => (
                        <SelectItem
                          key={c.id}
                          value={c.id.toString()}
                        >
                          {c.name}
                        </SelectItem>
                      ))}
                    </SelectContent>
                  </Select>
                </div>
              )}
            />
          </div>

          <DialogFooter className="mt-4">
            <DialogClose asChild>
              <Button type="button">Đóng</Button>
            </DialogClose>
            <Button type="submit">Cập nhật</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  )
}