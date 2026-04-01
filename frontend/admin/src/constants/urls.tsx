import type { User } from "@/models/users";
import type { StatusOrder } from "@/pages/orders/Update";

const apiUrl = import.meta.env.VITE_API_URL_BASE

export const API_ENDPOINTS = {
    AUTH: {
        LOGIN: `${apiUrl}/auth/login`,
    },
    USER: {
        PROFILE: '/user/profile',
        GETALL: `${apiUrl}/user/users`,
        GETUSER: (userId: string | number) => `${apiUrl}/user/${userId}`,
        CREATEUSER: `${apiUrl}/user/create`,
        DELETE: (id: string | number) => `${apiUrl}/user/${id}`,
        UPDATE: (data: any) => `${apiUrl}/user/${data.id}/update`
    },
    PRODUCT: {
        CREATE: `${apiUrl}/product`,
        DELETE: (id: number) => `${apiUrl}/product/${id}`,
        UPDATE: `${apiUrl}/product`,
        GETALL: `${apiUrl}/product`,
        GETID: (id: number) => `${apiUrl}/product/${id}`,
        GETDATASUMARY: `${apiUrl}/product/dataoverview`,
        GETTOPSALE: `${apiUrl}/product/topSales`,
        PAGINATION: (page: number, pageSize: number, search?: string, cid?: number) => {
            let url = `${apiUrl}/product/pagination?page=${page}&pageSize=${pageSize}`;

            if (search) url += `&search=${search}`;

            // luôn add categoryId (kể cả undefined)
            url += `&categoryId=${cid ?? ''}`;

            return url;
        }
    },
    CATEGORY: {
        CREATE: `${apiUrl}/category`,
        GETALL: `${apiUrl}/category/alls`,
        DELETE: (id: number) => `${apiUrl}/category/${id}`
    },
    ORDER: {
        GETALL: `${apiUrl}/order`,
        UPDATE: (id: number) => `${apiUrl}/order/${id}/updateStatus`,
        GETREVENUE: `${apiUrl}/order/revenues`
    },
    VOUCHER: {
        GETALL: `${apiUrl}/voucher`,
        CREATE: `${apiUrl}/voucher`,
        DELETE: (id: number) => `${apiUrl}/voucher/${id}`
    }
} as const;