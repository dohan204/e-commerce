import React, { useState, useEffect } from 'react'

function useFetchSingle<T>(url: string, deps: any[] = []) {
    const [data, setData] = useState<T | null>(null);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<Error | null>(null);
    // console.log("helo anh em")
    const fetchData = async () => {
        try {
            const response = await fetch(url);
            if (!response.ok)
                throw new Error(response.statusText);

            const data = await response.json();
            console.log('data: ', data)
            setData(data);
            setLoading(false);
        } catch (err) {
            setError(err as Error);
            setLoading(false);
        } finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        fetchData();
    }, [url]);
    return { data, loading, error, refresh: fetchData}
}

export default useFetchSingle