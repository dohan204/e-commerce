import { Button } from '@/components/ui/button'
import { Dialog, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from '@/components/ui/dialog'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import React, { useState, type ReactNode } from 'react'
import { Controller, useForm } from 'react-hook-form'
import { convertStatus } from '.'
import { Label } from '@/components/ui/label'
import type { OrderResponse } from '@/models/orders'
import { API_ENDPOINTS } from '@/constants/urls'
import { toast } from 'sonner'
import { authService } from '@/services/authService'

export type StatusOrder = {
    status: number,
}

const Update = ({ children, item, refresh }: { children: ReactNode, item: OrderResponse, refresh: () => void }) => {
    const [open, setOpen] = useState<boolean>(false);
    const { control, handleSubmit } = useForm<StatusOrder>({
        defaultValues: {
            status: item.status
        }
    })

    const onSubmit = async (data: StatusOrder) => {
        console.log("Dữ liệu cập nhật:",item.id + " " + data);
        try {
            const response = await fetch(API_ENDPOINTS.ORDER.UPDATE(item.id), {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    "Authorization": `Bearer ${authService.getToken()}`
                },
                body: JSON.stringify(data)
            });

            if(!response.ok)
                throw new Error("lỗi cập nhật");

            toast.success("Cập nhật thành công!", {position: 'top-center'});
            refresh();
            setOpen(false);
        } catch (err) {
            toast.error("Cập nhật Thất bại", {position: 'top-center'});
            console.error(err)
        }
    }

    const convertInputData = Object.entries(convertStatus).map(([key, value]) => ({
        key, value
    }))

    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                {children}
            </DialogTrigger>
            <DialogContent>
                <form onSubmit={handleSubmit(onSubmit)}>
                    <DialogHeader>
                        <DialogTitle>Cập nhật đơn hàng</DialogTitle>
                        <DialogDescription>
                            Trước khi đóng, nhớ nhấn Cập nhật đơn hàng nhé ^^
                        </DialogDescription>
                    </DialogHeader>

                    <div className="py-4">
                        <Controller
                            name='status'
                            control={control}
                            render={({ field }) => (
                                <div className='grid gap-4'>
                                    <Label htmlFor="status">Trạng thái</Label>
                                    <Select
                                        // Quan trọng: Convert string value từ Select về number cho Form
                                        onValueChange={(value) => field.onChange(Number(value))}
                                        value={field.value?.toString()}
                                    >
                                        <SelectTrigger id="status">
                                            <SelectValue placeholder="Chọn Trạng thái" />
                                        </SelectTrigger>
                                        <SelectContent>
                                            {convertInputData.map(item => (
                                                <SelectItem key={item.key} value={item.key.toString()}>
                                                    {item.value}
                                                </SelectItem>
                                            ))}
                                        </SelectContent>
                                    </Select>
                                </div>
                            )}
                        />
                    </div>

                    <DialogFooter>
                        <Button type="submit">Cập nhật đơn hàng</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}

export default Update