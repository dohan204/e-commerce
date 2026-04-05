import React from 'react';
import type { Base } from '@/models/response/base';
import type { Notifications } from '@/models/Notification';
import { API_ENDPOINTS } from '@/constants/UrlGlobal';
import { useUserContext } from '@/hooks/useUserContext';
import { useQuery } from '@tanstack/react-query'
import { Skeleton } from '@/components/ui/skeleton';

type Message = {
    title: string;
    message: string;
}

const Notification = () => {
    const { user } = useUserContext();
    const { data, error, isError, isLoading } = useQuery({
        queryKey: ['notifications', user?.sub],
        queryFn: async (): Promise<Base<Notifications>> => {
            const response = await fetch(API_ENDPOINTS.Notification.GET(user?.sub), {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('token')}`
                }
            })
            if (!response.ok) {
                throw new Error("error")
            }
            const data = await response.json();
            return data;
        },
        enabled: !!user?.sub // enabled khi chua co user
    })
    if (isError) {
        return <div>{error.name + ' ' + error.message}</div>;
    }

    return (
        <div className='w-full bg-white shadow-sm rounded-lg'>
            <div className='py-4 px-4 border-b'>
                <h1 className='text-xl font-bold'>Thông báo ({data?.data.length})</h1>
            </div>

            <div className=''>
                {isLoading ? (
                    Array.from({ length: 6 }).map((_, i) => (
                        <Skeleton className='w-full h-16' key={i} />
                    ))
                ) : data?.data.length === 0 ? (
                    <div className='w-full flex items-center justify-center'>
                        <p>Bạn chưa có thông báo nào</p>
                    </div>
                ) : (
                    data?.data.map((msg, index) => (
                        <div key={index} className='flex flex-row p-4 border-b hover:bg-gray-50 transition-colors'>
                            <div className='flex-1'>
                                <h4 className='font-semibold text-blue-600'>{msg.title}</h4>
                                <p className='text-sm text-gray-700'>{msg.message}</p>
                            </div>
                            <div className='flex flex-1 items-end justify-end'>
                                <p className='text-md text-gray-600 opacity-60'>Ngày: {new Date(msg.createdAt).toLocaleDateString('vi-VN')}</p>
                            </div>
                        </div>
                    ))
                )}
            </div>
        </div>
    )
}

export default Notification;
