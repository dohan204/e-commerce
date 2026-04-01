import React, { useEffect, useRef, useState } from 'react';
import * as signalR from '@microsoft/signalr';
import { toast } from 'sonner';
import useFetch from '@/hooks/useFetch';
import type { Base } from '@/models/response/base';
import type { Notification } from '@/models/Notification';
import { API_ENDPOINTS } from '@/constants/UrlGlobal';
import { useUserContext } from '@/hooks/useUserContext';

type Message = {
    title: string;
    message: string;
}

const Notification = () => {
    const [messages, setMessages] = useState<Message[]>([]);
    const connectionRef = useRef<signalR.HubConnection | null>(null);
    const { user } = useUserContext();
    useEffect(() => {
        const token = localStorage.getItem("token");

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5255/notificationHub", {
                accessTokenFactory: () => token || ""
            })
            .withAutomaticReconnect()
            .build();

        connection.on("ReceiveNotification", (data: Message) => {
            console.log("🔔 Nhận thông báo mới:", data);
            // Thêm thông báo mới lên đầu danh sách
            setMessages((prev) => [data, ...prev]);

            toast.success(`${data.title} - ${data.message}`, { position: 'top-center' })
        });

        connection.start()
            .then(() => console.log("✅ SignalR Connected"))
            .catch((err) => console.error("❌ SignalR Connection Error: ", err));

        connectionRef.current = connection;

        return () => {
            connection.stop();
        };
    }, []);

    const { data: baseResponse, loading } = useFetch<Base<Notification>>(API_ENDPOINTS.Notification.GET(user?.sub as string))
    const notifications = baseResponse?.data ?? [];
    return (
        <div className='w-full bg-white shadow-sm rounded-lg'>
            <div className='py-4 px-4 border-b'>
                <h1 className='text-xl font-bold'>Thông báo ({messages.length})</h1>
            </div>

            <div className=''>
                {notifications.length === 0 ? (
                    <p className='p-4 text-gray-500 text-center'>Chưa có thông báo nào.</p>
                ) : (
                    notifications.map((msg, index) => (
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
