import React, { useEffect, useState } from 'react'
import { Button } from "@/components/ui/button"
import {
    Card,
    CardAction,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Controller, useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import { useLocation, useNavigate } from 'react-router'
import { Spinner } from '@/components/ui/spinner'
import { authService, type LoginRequest, loginSchema } from '@/services/authService'




const Login = () => {
    const location = useLocation();
    const navigation = useNavigate();
    const [loading, setLoading] = useState<boolean>(false);
    const [error, setError] = useState<Error | null>(null)
    const form = useForm<LoginRequest>({
        resolver: zodResolver(loginSchema),
        defaultValues: {
            userName: '',
            password: ''
        }
    })

    useEffect(() => {
        if(location.pathname === '/login')
            authService.logout();
    }, [location])

    const onSubmit = async (data: LoginRequest) => {
        setLoading(true);
        setError(null);
        try {
            authService.login(data)
            navigation("/");
        } catch (error) {
            setError(error as Error);
            setLoading(false);
        } finally {
            setLoading(false);
        }
    }
    return (
        <div className='flex flex-col w-full h-screen justify-center items-center'>
            <Card className="w-full max-w-sm">
                <form onSubmit={form.handleSubmit(onSubmit)}>
                    <CardHeader className='justify-center'>
                        <CardTitle className='text-2xl'>Đăng nhập</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <div className="flex flex-col gap-8">
                            <Controller
                                name='userName'
                                control={form.control}
                                render={({ field, fieldState }) => (
                                    <div className="grid gap-3">
                                        <Label htmlFor='userName' className='mt-4'>Tài khoản</Label>
                                        <Input
                                            {...field}
                                            id='userName'
                                            placeholder='enter username...'
                                            className={fieldState.error ? "text-destructive" : ""}
                                            required
                                        />
                                    </div>
                                )}
                            />
                            <Controller
                                name='password'
                                control={form.control}
                                render={({ field, fieldState }) => (
                                    <div className="grid gap-3">
                                        <div className="flex items-center">
                                            <Label htmlFor="password">Mật khẩu</Label>
                                            <a
                                                href="#"
                                                className="ml-auto inline-block text-sm underline-offset-4 hover:underline"
                                            >
                                                Quên mật khẩu?
                                            </a>
                                        </div>
                                        <Input {...field} id="password" type="password" required />
                                        {fieldState.error ? <p className='text-sm text-destructive font-medium'>{fieldState.error.message}</p> : ''}
                                    </div>
                                )}
                            />
                        </div>
                    </CardContent>
                    <CardFooter className="flex-col gap-2">
                        <div className='flex items-center w-full'>
                            {loading ? <Spinner /> : ''}
                            <Button type="submit" className="w-full" variant={'outline'} disabled={loading}>
                                {loading ? 'Đang đăng nhập' : 'Đăng nhập'}
                            </Button>
                        </div>
                        <Button variant="outline" className="w-full">
                            Đăng nhập bằng Google
                        </Button>
                    </CardFooter>
                </form>
            </Card>
        </div>
    )
}

export default Login