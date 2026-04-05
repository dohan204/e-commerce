import { Dialog, DialogContent, DialogHeader, DialogTitle } from '@/components/ui/dialog'
import { API_ENDPOINTS } from '@/constants/UrlGlobal'
import { zodResolver } from '@hookform/resolvers/zod'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { useNavigate } from 'react-router'
import { toast } from 'sonner'
import * as z from 'zod'
import { useQueryClient } from '@tanstack/react-query';

const schema = z.object({
    phone: z.string().min(10, 'Số điện thoại không hợp lệ'),
    province: z.string().nonempty('Vui lòng nhập tỉnh/thành'),
    district: z.string().nonempty('Vui lòng nhập quận/huyện'),
    ward: z.string().nonempty('Vui lòng nhập phường/xã'),
})

type AddressForm = z.infer<typeof schema>

type Props = {
    isOpen: boolean
    close: () => void
}

const AddressCreate = ({ isOpen, close }: Props) => {
    const [loading, setLoading] = useState<boolean>(false);
    const navigate = useNavigate();
    const queryClient = useQueryClient();

    const { register, handleSubmit,reset, formState: { errors } } = useForm<AddressForm>({
        resolver: zodResolver(schema),
        defaultValues: { phone: '', province: '', district: '', ward: '' }
    })
    const token = localStorage.getItem('token');

    const onSubmit = async (data: AddressForm): Promise<void> => {
        console.log(data)
        setLoading(true)
        try {
            const res = await fetch(API_ENDPOINTS.ADDRESS.CREATE, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify(data)
            })

            if(!res.ok)
                throw new Error('failed');

            queryClient.invalidateQueries({queryKey: ['add']})

            toast.success("Đăng ký thành công.");
            reset();
            close()
        } catch (err) {
            toast.error('Đăng ký thất bại')
            console.log(err)
        } finally {
            setLoading(false)
        }
        
    }

    return (
        <Dialog open={isOpen} onOpenChange={close}>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Đăng ký địa chỉ</DialogTitle>
                </DialogHeader>

                <form onSubmit={handleSubmit(onSubmit)} className="flex flex-col gap-4">
                    <div>
                        <label className="text-sm font-medium">Số điện thoại</label>
                        <input
                            {...register('phone')}
                            placeholder="Số điện thoại..."
                            className="w-full px-3 py-2 border rounded-md mt-1"
                        />
                        {errors.phone && <p className="text-red-500 text-xs mt-1">{errors.phone.message}</p>}
                    </div>

                    <div>
                        <label className="text-sm font-medium">Tỉnh/Thành phố</label>
                        <input
                            {...register('province')}
                            placeholder="Tỉnh/Thành phố..."
                            className="w-full px-3 py-2 border rounded-md mt-1"
                        />
                        {errors.province && <p className="text-red-500 text-xs mt-1">{errors.province.message}</p>}
                    </div>

                    <div>
                        <label className="text-sm font-medium">Quận/Huyện</label>
                        <input
                            {...register('district')}
                            placeholder="Quận/Huyện..."
                            className="w-full px-3 py-2 border rounded-md mt-1"
                        />
                        {errors.district && <p className="text-red-500 text-xs mt-1">{errors.district.message}</p>}
                    </div>

                    <div>
                        <label className="text-sm font-medium">Phường/Xã</label>
                        <input
                            {...register('ward')}
                            placeholder="Phường/Xã..."
                            className="w-full px-3 py-2 border rounded-md mt-1"
                        />
                        {errors.ward && <p className="text-red-500 text-xs mt-1">{errors.ward.message}</p>}
                    </div>

                    <button
                        type="submit"
                        disabled={loading}
                        className="w-full py-2 bg-orange-500 
                        text-white font-bold rounded-lg hover:bg-orange-600 
                        transition "
                    >
                        {loading ? 'Đang xử lý' : 'Đăng ký'}
                    </button>
                </form>
            </DialogContent>
        </Dialog>
    )
}

export default AddressCreate