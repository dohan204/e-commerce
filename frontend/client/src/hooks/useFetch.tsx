import React, { useEffect, useState } from 'react'

const cache = new Map<string, any>();
function useFetch<T>(url: string, deps: any[] = []) {
    const [data, setData] = useState<T | null>(cache.get(url) || null);
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);
    const fetchData = React.useCallback(async (signal?: AbortSignal, forceRefresh = false): Promise<void> => {
        if (cache.has(url) && !forceRefresh) {
            setLoading(true); // 👈 thêm dòng này
            const cached = cache.get(url);
            // delay nhẹ để skeleton render
            setTimeout(() => {
                setData(cached);
                setLoading(false);
            }, 300);    

            return;
        }
        cache.delete(url); // xóa cache cũ
        setError(null);
        try {
            const res = await fetch(url, {
                method: 'GET', signal,
                headers: { 'Authorization': `Bearer ${localStorage.getItem('token')}` }
            });
            if (!res.ok) throw new Error(`Error: ${res.status} - ${res.statusText}`);
            const response = await res.json();
            setData(response);
            cache.set(url, response);
        } catch (err: any) {
            if (err.name !== 'AbortError') setError(err.message || "Something went wrong");
        } finally {
            setLoading(false);
        }
    }, [url]) // ← chỉ giữ url

    useEffect(() => {
        const controller = new AbortController();
        fetchData(controller.signal, deps.length > 0); // deps > 0 = force refresh
        return () => controller.abort();
    }, [url, ...deps]) // ← move deps xuống đây, useEffect chấp nhận spread

    const refresh = () => fetchData(undefined, true);
    return { data, loading, error, refresh }
}

export default useFetch