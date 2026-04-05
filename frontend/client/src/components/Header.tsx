import { useUserContext } from '@/hooks/useUserContext'
import { Bell, ClockCheck, ShoppingCart, User } from 'lucide-react'
import SearchHeader from './Search';
import CartSheet from './CartSheet';
import { Link, NavLink } from 'react-router';
import Tooltips from './Tooltip';
// import React from 'react'

const Header = () => {
    const { user, loading } = useUserContext();
    return (
        <header className='w-full h-16 bg-white shadow-2xl px-6 flex items-center justify-around'>
            <div className='flex flex-1 flex-col justify-center items-center gap-2'>
                <h1 className='text-xl font-black font-serif'>Shop byHan</h1>
                <p className='text-emerald-400 font-medium'>From to han</p>
            </div>
            <SearchHeader />
            <div className='flex flex-1 justify-center items-center gap-4'>
                {
                    user && <div className='w-10 h-10 flex items-center justify-center  rounded-md relative'>
                        <Link to='/notifications'>
                            <Tooltips description='Thông báo của bạn'>
                                <Bell size={24} />
                            </Tooltips>
                        </Link>
                    </div>
                }
                <div className='w-10 h-10 flex items-center justify-center rounded-md'>
                    <CartSheet>
                        <Tooltips description='Giỏ hàng của bạn'>
                            <ShoppingCart />
                        </Tooltips>
                    </CartSheet>
                </div>
                {
                    user && <div className='w-10 h-10 flex items-center justify-center rounded-md'>
                        <Link to={'/orders'}>
                            <Tooltips description='Lịch sử đơn hàng'>
                                <ClockCheck />
                            </Tooltips>
                        </Link>
                    </div>
                }
                {
                    loading ? (
                        <div className='w-10 h-10 bg-gray-200 animate-pulse rounded-md' />
                    ) : user ? (
                        <div className='w-10 h-10 flex flex-row  items-center  rounded-md'>
                            <User />
                            <span className='text-sm font-medium '>{user.FullName}</span>
                        </div>
                    ) : (
                        <NavLink to='/login'>Đăng nhập</NavLink>
                    )
                }
            </div>
        </header>
    )
}

export default Header