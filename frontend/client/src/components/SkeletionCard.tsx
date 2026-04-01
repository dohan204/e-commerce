import React from 'react'
import { Card, CardContent, CardHeader } from "@/components/ui/card"
import { Skeleton } from "@/components/ui/skeleton"
const SkeletionCard = () => {
    return (
        <Card className="w-full max-w-xs bg-gray-300 shadow-md relative animate-pulse">
            <CardHeader>
                <Skeleton className="aspect-video w-full" />
            </CardHeader>
            <CardContent className='flex flex-col gap-2'>
                <Skeleton className='h-4 w-1/3 rounded-sm' /> 
                <Skeleton className="h-4 w-full rounded-sm" />
                <Skeleton className="h-4 w-1/2" />
            </CardContent>
            <Skeleton className="h-8 w-1/3 rounded-sm absolute top-2 left-2" />
        </Card>
    )
}

export default SkeletionCard