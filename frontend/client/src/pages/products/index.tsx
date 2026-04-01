import { useUserContext } from '@/hooks/useUserContext'
import type { product } from '@/models/Products'
import { useEffect, useState } from 'react'
import { useLocation, useNavigate } from 'react-router';
import {
    Breadcrumb,
    BreadcrumbItem,
    BreadcrumbLink,
    BreadcrumbList,
    BreadcrumbPage,
    BreadcrumbSeparator,
} from "@/components/ui/breadcrumb"
import { API_ENDPOINTS, url } from '@/constants/UrlGlobal';
import { useCartStore } from '@/store/useCartStore';
import useFetch from '@/hooks/useFetch';
import type { Address } from '@/models/Address';
import AddressCreate from '../payments/AddressCreate';
import { useAddressDialog } from '@/store/useAddressDialog';
import EvaluationForm from './EvaluationForm';
import { Button } from '@/components/ui/button';
import { useRatingStore } from '@/store/useRatingStore';
import type { Review } from '@/models/Review';
import ReviewDisplay from './reviewForm';
import type { Base } from '@/models/response/base';
import { toast } from 'sonner';
import { useReviewStore } from '@/store/useReviewStore';

const DetailsProduct = () => {
    // Để mặc định là 1 cho chuẩn
    const [quantity, setQuantity] = useState<number>(1);
    const { user } = useUserContext();
    const navigate = useNavigate();
    const location = useLocation();
    const { key } = useReviewStore();
    const { triggerRefresh } = useCartStore();
    const { isOpen, open, close } = useAddressDialog();
    const { isOpen: isRatingOpen, openForm, closeForm } = useRatingStore();
    // Bảo vệ app nếu user reload trang (state bị mất)
    const product = location.state as product;

    if (!product) {
        return <div className="p-10 text-center">Không tìm thấy sản phẩm. <button onClick={() => navigate('/categories')}>Quay lại</button></div>;
    }
    useEffect(() => {
        window.scrollTo(0, 0);
    }, [])
    // Thêm async ở đây
    const AddProductToCart = async () => {
        const token = localStorage.getItem('token');
        if (!user && !token) {
            navigate('/login');
            return;
        }
        try {
            const res = await fetch(API_ENDPOINTS.CART.CREATE, {
                method: 'POST',
                headers: {
                    "Content-Type": 'application/json',
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify({ productId: product.id, quantity, price: product.price })
            });
            console.log(`Đã thêm ${quantity} sản phẩm ${product.name} vào giỏ`);
            if (!res.ok) {
                // Nếu token hết hạn (401), bắt user login lại
                if (res.status === 401) {
                    localStorage.removeItem('token');
                    navigate('/login');
                    return;
                }
                throw new Error("Không thể thêm vào giỏ hàng");
            }
            toast.success("Thêm sản phẩm thành công.", { position: 'top-center'});
            triggerRefresh();
        } catch (error) {
            toast.error("Thêm sản phẩm thất bại", {position: 'top-center'})
            console.error("Lỗi thêm giỏ hàng", error);
        }
    };
    console.log(user?.sub);
    // check address user with function();
    const { data: address } = useFetch<Address>(API_ENDPOINTS.ADDRESS.GET(user?.sub ?? ''))
    const { data: reviews } = useFetch<Base<Review>>(API_ENDPOINTS.Review.GET(product.id), [key])
    const hasAddress = !!address;
    console.log(address)

    const dataReviews = reviews?.data ?? []
    const data = [{ ...product, quantity }]



    return (
        <div className="w-full bg-gray-100 min-h-screen p-6">
            <Breadcrumb className="mb-6">
                <BreadcrumbList>
                    <BreadcrumbItem>
                        <BreadcrumbLink className="cursor-pointer" onClick={() => navigate('/categories')}>Danh mục</BreadcrumbLink>
                    </BreadcrumbItem>
                    <BreadcrumbSeparator />
                    <BreadcrumbItem>
                        <BreadcrumbPage>{product.name}</BreadcrumbPage>
                    </BreadcrumbItem>
                </BreadcrumbList>
            </Breadcrumb>

            <div className="max-w-6xl mx-auto bg-white rounded-xl shadow-sm p-8 grid grid-cols-1 md:grid-cols-2 gap-12">
                {/* LEFT - IMAGE */}
                <div>
                    <div className="w-full aspect-square bg-gray-50 rounded-lg overflow-hidden border">
                        <img
                            src={url + product.imageUrl}
                            alt={product.name}
                            className='w-full h-full object-contain hover:scale-105 transition-transform'
                        />
                    </div>
                </div>

                {/* RIGHT - INFO */}
                <div className="flex flex-col gap-6">
                    <h1 className="text-2xl font-bold text-gray-800">
                        {product.name}
                    </h1>

                    <div className="flex items-center gap-4 text-sm">
                        <span className="text-orange-500 font-bold">⭐ {product.avgRatings || 0}</span>
                        <span className="text-gray-400">| {product.reviewCount || 0} Đánh giá</span>
                        <span className="text-gray-400">| Đã bán {product.sold || 0}</span>
                    </div>

                    <div className="bg-orange-50 p-4 rounded-lg">
                        <p className="text-3xl text-orange-600 font-bold">
                            {new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(product.price)}
                        </p>
                    </div>

                    <div className="space-y-4">
                        <div>
                            <p className="font-medium mb-2 text-gray-700">Số lượng:</p>
                            <div className="flex items-center gap-3">
                                <button
                                    className="w-8 h-8 border rounded-full hover:bg-gray-100"
                                    onClick={() => setQuantity(prev => Math.max(1, prev - 1))}
                                >-</button>
                                <input
                                    type="number"
                                    value={quantity}
                                    onChange={(e) => setQuantity(Math.max(1, parseInt(e.target.value) || 1))}
                                    className="w-14 text-center border-b font-semibold outline-none"
                                />
                                <button
                                    className="w-8 h-8 border rounded-full hover:bg-gray-100"
                                    onClick={() => setQuantity(prev => prev + 1)}
                                >+</button>
                            </div>
                        </div>

                        <div className="flex gap-4 pt-6">
                            <button
                                className="flex-1 px-6 py-3 border-2 border-orange-500 text-orange-600 font-bold rounded-lg hover:bg-orange-50 transition-colors"
                                onClick={AddProductToCart}
                            >
                                Thêm vào giỏ hàng
                            </button>
                            <button className="flex-1 px-6 py-3 bg-orange-500 text-white font-bold rounded-lg hover:bg-orange-600 shadow-lg shadow-orange-200 transition-all"
                                onClick={() => {
                                    if (!hasAddress) {
                                        open();
                                        return;
                                    }
                                    navigate('/order', { state: data })
                                }}
                            >
                                Đặt hàng
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div className='flex flex-col'>
                {/* Mô tả sản phẩm */}
                <h2 className="text-xl font-bold text-gray-800 mt-10 mb-4">Mô tả sản phẩm</h2>
                <p className="text-gray-700 leading-relaxed whitespace-pre-line">{product.description}</p>
            </div>
            <div className='flex flex-col mt-10'>
                {/* Đánh giá sản phẩm */}
                <div className='flex w-full items-center'>
                    <h2 className="flex-1 text-xl font-bold text-gray-800 mb-4">Đánh giá sản phẩm</h2>
                    <div className='flex-2'>
                        <Button onClick={openForm} className='float-end'>
                            Tạo đánh giá
                        </Button>
                    </div>
                </div>
                <div>
                    {dataReviews?.map((review) => (
                        <ReviewDisplay review={review} />
                    ))}
                </div>
            </div>
            <AddressCreate isOpen={isOpen} close={close} />
            <EvaluationForm isOpen={isRatingOpen} closeForm={closeForm} id={product.id} />
        </div>
    );
};
export default DetailsProduct