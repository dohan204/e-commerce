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
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select"
import { DiscountType, schema, type VoucherCreate } from "@/models/vouchers"
import { zodResolver } from "@hookform/resolvers/zod"
import { useState, type ReactNode } from "react"
import { Controller, useForm } from "react-hook-form"
import { format } from "date-fns"
import { Calendar as CalendarIcon } from "lucide-react"
import { cn } from "@/lib/utils"
import { Calendar } from "@/components/ui/calendar"
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover"
import { API_ENDPOINTS } from "@/constants/urls"
import { authService } from "@/services/authService"
import { toast } from "sonner"

export function Create({ children, refresh }: { children: ReactNode, refresh: () => void }) {
    const [open, setOpen] = useState<boolean>(false);
    const form = useForm<VoucherCreate>({
        resolver: zodResolver(schema),
        defaultValues: {
            discountType: 1,
            value: 0,
            minOrder: 0,
            maxUsage: 0,
            expiryDate: null
        }
    })

    const onSubmit = async (data: VoucherCreate) => {
        const convertInput = {
            ...data,
            expiryDate: new Date(data.expiryDate).toISOString().slice(0, 10)
        }
        
        const url = API_ENDPOINTS.VOUCHER.CREATE
        console.log(url)
        try {
            const response = await fetch(url, {
                method: 'POST',
                headers: {
                    "Content-Type": 'application/json',
                    "Authorization": `Bearer ${authService.getToken()}`
                },
                body: JSON.stringify(convertInput)
            })
            
            if(!response.ok)
                throw new Error(response.statusText)

            toast.success("Tạo Mã giảm giá thành công.", {position: 'top-center'})
            refresh();
            setOpen(false);
            form.reset();
        } catch (err) {
            toast.error('Tạo mã giảm giá thất bại', {position: 'top-center'})
            console.error(err);
        }
    };

    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <Button>
                    {children}
                </Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-sm">
                <form onSubmit={form.handleSubmit(onSubmit)}>
                    <DialogHeader>
                        <DialogTitle>Tạo Mã Giảm giả mới</DialogTitle>
                        <DialogDescription>
                            Nhập thông tin mã giảm giá, và nhớ phải nhấn tạo nhé^^
                        </DialogDescription>
                    </DialogHeader>
                    <div className="py-4">
                        <Controller
                            name='discountType'
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div>
                                    <Label>Kiểu giảm giá</Label>
                                    <Select
                                        // Quan trọng: Convert string value từ Select về number cho Form
                                        onValueChange={(value) => field.onChange(Number(value))}
                                        value={field.value?.toString()}

                                    >
                                        <SelectTrigger className="w-full">
                                            <SelectValue placeholder="Chọn Trạng thái" />
                                        </SelectTrigger>
                                        <SelectContent className="w-max">
                                            {DiscountType.map(item => (
                                                <SelectItem key={item.lable} value={item.value.toString()}>
                                                    {item.lable}
                                                </SelectItem>
                                            ))}
                                        </SelectContent>
                                    </Select>
                                    {fieldState.error && <p className="font-medium text-destructive">{fieldState.error.message}</p>}
                                </div>
                            )}
                        />
                        <Controller
                            name="value"
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div>
                                    <Label>Giá trị</Label>
                                    <Input
                                        {...field}
                                        placeholder="Nhập giá trị"
                                        type='number'
                                        value={field.value}
                                        onChange={value => field.onChange(Number(value.target.value))}
                                    />
                                    {fieldState.error && <p className="font-medium text-destructive">{fieldState.error.message}</p>}
                                </div>
                            )}
                        />
                        <Controller
                            name="minOrder"
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div>
                                    <Label>Giá trị đơn hàng</Label>
                                    <Input
                                        {...field}
                                        placeholder="Giá trị đơn hàng"
                                        type='number'
                                        value={field.value}
                                        onChange={value => field.onChange(Number(value.target.value))}
                                    />
                                    {fieldState.error && <p className="font-medium text-destructive">{fieldState.error.message}</p>}
                                </div>
                            )}
                        />
                        <Controller
                            name="maxUsage"
                            control={form.control}
                            render={({ field, fieldState }) => (
                                <div>
                                    <Label>Số lần sử dụng</Label>
                                    <Input
                                        {...field}
                                        placeholder="Số lần sử dụng..."
                                        type='number'
                                        value={field.value}
                                        onChange={value => field.onChange(Number(value.target.value))}
                                    />
                                    {fieldState.error && <p className="font-medium text-destructive">{fieldState.error.message}</p>}
                                </div>
                            )}
                        />
                        <Controller
                            name="expiryDate"
                            control={form.control}
                            render={({ field }) => (
                                <div className="flex flex-col gap-2">
                                    <Label>Ngày hết hạn</Label>
                                    <Popover>
                                        <PopoverTrigger asChild>
                                            <Button
                                                variant={"outline"}
                                                className={cn(
                                                    "w-full justify-start text-left font-normal",
                                                    !field.value && "text-muted-foreground"
                                                )}
                                            >
                                                <CalendarIcon className="mr-2 h-4 w-4" />
                                                {field.value ? (
                                                    format(new Date(field.value), "dd/MM/yyyy")
                                                ) : (
                                                    <span>Chọn ngày hết hạn</span>
                                                )}
                                            </Button>
                                        </PopoverTrigger>
                                        <PopoverContent className="w-auto p-0" align="start">
                                            <Calendar
                                                mode="single"
                                                selected={field.value ? new Date(field.value) : undefined}
                                                onSelect={field.onChange}
                                                disabled={(date) =>
                                                    date < new Date(new Date().setHours(0, 0, 0, 0))
                                                }
                                                
                                            />
                                        </PopoverContent>
                                    </Popover>
                                </div>
                            )}
                        />
                    </div>
                    <DialogFooter>
                        <DialogClose asChild>
                            <Button variant="outline">Cancel</Button>
                        </DialogClose>
                        <Button type="submit">Save changes</Button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}
