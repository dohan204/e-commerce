import { Button } from "@/components/ui/button"
import {
    Sheet,
    SheetClose,
    SheetContent,
    SheetDescription,
    SheetFooter,
    SheetHeader,
    SheetTitle,
    SheetTrigger,
} from "@/components/ui/sheet"
import { API_ENDPOINTS } from "@/constants/UrlGlobal"
import useFetch from "@/hooks/useFetch"
import { useUserContext } from "@/hooks/useUserContext"
import type { Cart, CartItem } from "@/models/Cart"
import { useCartStore } from "@/store/useCartStore"
import { useState, type ReactNode } from "react"
import { useNavigate } from "react-router"
import { Checkbox } from "./ui/checkbox"

const CartSheet = ({ children }: { children: ReactNode }) => {
    const [selectedRows, setSelectedRows] = useState<Set<CartItem>>(new Set());
    const { isOpen } = useCartStore();
    const { user } = useUserContext();
    const navigate = useNavigate();
    const {refreshKey} = useCartStore();
    const { data, refresh } = useFetch<Cart>(API_ENDPOINTS.CART.GET(user?.sub), [refreshKey])

    const selectAll = data?.items && data.items.length > 0 && data.items.length === selectedRows.size;

    const handleSelectAll = (checked: boolean) => {
        if(checked && data?.items) {
            setSelectedRows(new Set(data.items))
        } else {
            setSelectedRows(new Set());
        }
    }

    const handleSelectedRow = (item: any, checked: boolean) => {
        const newSelected = new Set(selectedRows);
        if(checked) {
            newSelected.add(item);
        } else {
            newSelected.delete(item);
        }

        setSelectedRows(newSelected);
    }
    const totalAmountSelect = Array.from(selectedRows).reduce(
        (item, value) => item + (value.quantity * value.price), 0
    )

    return (
        <Sheet
            open={isOpen}
            // Khi người dùng bấm ra ngoài hoặc bấm nút đóng, cập nhật lại Store
            onOpenChange={(open) => {
                if (!open) useCartStore.getState().closeCart();
                else useCartStore.getState().openCart();
            }}
        >
            <SheetTrigger asChild>
                {/* Children có thể là icon giỏ hàng hoặc text */}
                <div onClick={() => useCartStore.getState().openCart()}>
                    {children}
                </div>
            </SheetTrigger>
            <SheetContent showCloseButton={false} side="right">
                <SheetHeader>
                    <SheetTitle>Giỏ hàng</SheetTitle>
                    <SheetDescription>
                        Hãy cùng thêm các mặt hàng bạn yêu thích vào nhé ^^
                    </SheetDescription>
                </SheetHeader>
                {user ? (<div className="flex flex-col px-2">
                    <div className='w-full flex'>
                        <div className="flex-1">
                            <Checkbox 
                                checked={selectAll}
                                onCheckedChange={handleSelectAll}
                            />
                        </div>
                        <div className="flex-2">
                            <p>Tên sản phẩm</p>
                        </div>
                        <div className="flex-1">
                            <p>Số lượng</p>
                        </div>
                        <div className="flex-1">
                            <p>Giá </p>
                        </div>
                    </div>
                    {data?.items?.map((item, i) => (
                        <div className="w-full flex" key={i}>
                             <div className="flex-1">
                                <Checkbox
                                    checked={selectedRows.has(item)}
                                    onCheckedChange={(checked) => handleSelectedRow(item, checked === true)}
                                />
                            </div>
                            <div className="flex-2">
                                {item.name}
                            </div>
                            <div className="flex-1">
                                x{item.quantity}
                            </div>
                            <div className="flex-1">
                                {item.price.toLocaleString('vi-VN', {style: 'currency', currency: 'VND'})}
                            </div>
                        </div>
                    ))}
                </div>) : (<div className="w-full">
                    <h4 className="text-sm text-center font-light px-2">Vui lòng đăng nhập để thực hiện chức năng này</h4>
                </div>)}
                <SheetFooter>
                    {user && (<div className="flex flex-row justify-around">
                        <div>
                            <p>Sản phẩm: {selectedRows.size}</p>
                        </div>
                        <div>
                            <p>Tổng: {totalAmountSelect}</p>
                        </div>
                    </div>)}
                    {
                        user && <Button
                            onClick={() => navigate('/order', { state: [...selectedRows]})}
                        >
                        Thanh toán
                    </Button> 
                    }
                    <SheetClose>
                        Đóng
                    </SheetClose>
                </SheetFooter>
            </SheetContent>
        </Sheet>
    )
}

export default CartSheet