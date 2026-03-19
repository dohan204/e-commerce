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
import type { response } from '.'
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
import useFetchs from '@/hooks/use-fetch'

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
  item
}: {
  children: ReactNode
  item: response
}) {
  const { data = [] } = useFetchs<response>(
    'http://localhost:5255/api/category/alls'
  )

  const form = useForm<ModelUpdate>({
    resolver: zodResolver(schema),
    defaultValues: {
      id: item.id,
      name: item.name,
      description: item.description!,
      price: item.price,
      salePrice: item.salePrice ?? 0,
      stock: item.stock ?? 0,

      /* ❌ fix bug ngu vl lúc nãy */
      categoryId: item.id
    }
  })

  const [open, setOpen] = useState(false)

  /* ✅ handle submit */
  const onSubmit = (values: ModelUpdate) => {
    console.log('form values:', values)

    /* 🔥 nếu có file → dùng FormData */
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

    // 👉 call API ở đây
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
              render={({ field }) => (
                <div className="grid gap-2">
                  <Label>Hình ảnh</Label>

                  {/* 🔥 hiển thị ảnh cũ */}
                  {item.imageUrl && (
                    <img
                      src={item.imageUrl}
                      className="w-20 h-20 object-cover rounded"
                    />
                  )}

                  <Input
                    type="file"
                    onChange={(e) =>
                      field.onChange(e.target.files?.[0])
                    }
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
                      {data.map((c) => (
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