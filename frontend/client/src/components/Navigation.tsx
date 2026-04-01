import React, { useState } from 'react'
import { NavLink, useLocation } from 'react-router';

const menu = [
    { title: 'Trang chủ', link: '/' },
    { title: 'Danh mục', link: '/categories' },
    { title: 'Khuyến mãi', link: '/promotions' },
    { title: 'Liên hệ', link: '/contact' }
]

const Navigation = () => {
    const [click, setClick] = useState<number>(0);
    const location = useLocation();
    const currentPath = location.pathname;
    
    return (
        <nav className='w-full bg-white h-12 shadow-sm'>
            <ul className='flex items-center justify-center gap-6 h-full'>
                {menu.map((item, i) => (
                    <NavLink
                        to={item.link}
                        key={item.title}
                        onClick={() => setClick(i)}
                        className='relative group cursor-pointer px-4 py-2 text-lg'
                    >
                        {/* text */}
                        <span
                            className={`transition ${
                                click === i && currentPath === item.link
                                    ? "text-blue-500 font-semibold"
                                    : "text-gray-600 group-hover:text-black"
                            }`}
                        >
                            {item.title}
                        </span>

                        {/* underline animation */}
                        <span
                            className={`absolute left-0 bottom-0 h-[2px] w-full bg-blue-500 
                            origin-left transition-transform duration-300
                            ${
                                click === i && currentPath === item.link
                                    ? "scale-x-100"
                                    : "scale-x-0 group-hover:scale-x-100"
                            }`}
                        />
                    </NavLink>
                ))}
            </ul>
        </nav>
    )
}

export default Navigation