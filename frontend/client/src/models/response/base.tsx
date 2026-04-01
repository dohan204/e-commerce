export interface Base<T> {
    message: string,
    data: T[]
}

export interface PagedResult<T> {
    items: T[],
    page: number,
    pageSize: number,
    total?: number
}

