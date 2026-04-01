

export const url = import.meta.env.VITE_API_URL
console.log(url)
export const API_ENDPOINTS = {
    CATEGORY: {
        GET: `${url}/api/category/alls`
    },
    CART: {
        CREATE: `${url}/api/cart`,
        GET: (id?: string) => `${url}/api/cart/${id}`,
        DEL: (id: number) => `${url}/api/cart/${id}`
    },
    PRODUCT: {
        GETALL: (page: number, pageSize: number, search?: string, categoryId?: number) => {
            let baseUrl = `${url}/api/product/pagination?page=${page}&pageSize=${pageSize}`;

            if (search)
                baseUrl += `&search=${search}`;

            if (categoryId)
                baseUrl += `&categoryId=${categoryId}`;

            return baseUrl
        },
        GET: (id: number) => `${url}/api/product/${id}`,
        GETSALES: `${url}/api/product/sales`,
        PAGINATION: (page: number, pageSize: number, search?: string, categoryId?: number) => {
            let urls = `${url}/api/product/pagination?page=${page}&pageSize=${pageSize}`;

            if (search)
                urls += `&search=${search}`;

            if (categoryId)
                urls += `&categoryId=${categoryId}`
            return urls;
        },

    },
     ADDRESS: {
        CREATE: `${url}/api/address`,
        GET: (userId?: string) => `${url}/api/address/${userId}`
    },
    ORDER: {
        CREATE: `${url}/api/order`,
        GETBYUSERID: (userId: string) => `${url}/api/order/user/${userId}`,
        CANCELLED: (id: number) => `${url}/api/order/${id}/cancelled`,
        DELETE: (id: number) => `${url}/api/order/${id}`
    },
    Review: {
        GET: (id: number) => `${url}/api/review/${id}`,
        CREATE: `${url}/api/review/create`
    },
    Notification: {
        GET: (userId: string) => `${url}/api/notification/${userId}`
    }
}