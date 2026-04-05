import { useQuery, type UseQueryOptions } from "@tanstack/react-query"


function useFetchData<TResponse = any, TData = TResponse>(
    url: string,
    key: string,
    params: any[] = [],
    options?: Omit<UseQueryOptions<TResponse, Error, TData>, 'queryKey'| 'queryFn'>
) {

    const token = localStorage.getItem('token');
    // tạo query tự động
    return useQuery({
        queryKey: [key, ...params],
        queryFn: async (): Promise<TResponse> => {
            const response = await fetch(url, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            if(!response.ok) {
                throw new Error("Networ response wass not found");
            }
            return await response.json();
        },
        ...options
    })
}

export default useFetchData;