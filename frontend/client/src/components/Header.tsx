import { useUserContext } from '@/hooks/useUserContext'
import { Bell, ClockCheck, Search, ShoppingCart, User } from 'lucide-react'
import SearchHeader from './Search';
import CartSheet from './CartSheet';
import { Link, NavLink } from 'react-router';
import Tooltips from './Tooltip';
import { useNotification } from '@/hooks/useNotificationContext';
// import React from 'react'

const Header = () => {
    const { user, loading } = useUserContext();
    const {unreadCount} = useNotification();
    return (
        <header className='w-full h-16 bg-white shadow-2xl px-6 flex items-center justify-around'>
            <div className='flex flex-1 flex-col justify-center items-center gap-2'>
                <h1 className='text-xl font-black font-serif'>Shop byHan</h1>
                <p className='text-emerald-400 font-medium'>From to han</p>
            </div>
            {/* search bar */}
            <SearchHeader />
            {/* authenticatin */}
            <div className='flex flex-1 justify-center items-center gap-4'>
                <div className='w-[40px] h-[40px] flex items-center justify-center  rounded-md relative'>
                    <Link to='/notifications'>
                        <Tooltips description='Thông báo của bạn'>
                            <Bell size={24} />
                            <div className='absolute w-1 h-1 top-0 left-2 '>
                                {unreadCount > 9 ? '9++' : unreadCount}
                            </div>
                        </Tooltips>
                    </Link>
                </div>
                <div className='w-[40px] h-[40px] flex items-center justify-center rounded-md'>
                    <CartSheet>
                        <Tooltips description='Giỏ hàng của bạn'>
                            <ShoppingCart />
                        </Tooltips>
                    </CartSheet>
                </div>
                <div className='w-[40px] h-[40px] flex items-center justify-center rounded-md'>
                    <Link to={'/orders'}>
                        <Tooltips description='Lịch sử đơn hàng'>
                            <ClockCheck />
                        </Tooltips>
                    </Link>
                </div>
                {
                    loading ? (
                        <div className='w-[40px] h-[40px] bg-gray-200 animate-pulse rounded-md' />
                    ) : user ? (
                        <div className='w-[40px] h-[40px] flex flex-row  items-center  rounded-md'>
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