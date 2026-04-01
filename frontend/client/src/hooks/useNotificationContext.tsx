import React, { useContext, useEffect, useState, type ReactNode } from 'react'
import * as signalR from '@microsoft/signalr'
import { NotificationContext } from '@/context/NotificationContext';
const NotificationProvider = ({children}: {children: ReactNode}) => {
    const [unreadCount, setUnreadCount] = useState(0);

    useEffect(() => {
        const connection = new signalR.HubConnectionBuilder()
            .withUrl('http://localhost:5255/notificationHub', {
                accessTokenFactory: () => localStorage.getItem('token') || ''
            })
            .withAutomaticReconnect()
            .build();

        connection.on("ReceiveMessage", (data: any) => {
            console.log(data);
            setUnreadCount(prev => prev + 1);
        });

        connection.start().then(() => console.log("connected success.")).catch((err) => console.error('Connected error: ', err));

        return () => {
            connection.stop();
        }
    }, [])

    const resetCount = () => setUnreadCount(0);
  return (
    <NotificationContext.Provider value={{unreadCount, resetCount}}>
        {children}
    </NotificationContext.Provider>
  )
}


export const useNotification = () => {
    const context = useContext(NotificationContext);
    if(!context) {
        throw new Error("useNatification must be used within provider")
    };
    return context;
}
export default NotificationProvider