
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import * as z from 'zod';
import { useState } from 'react';
import { useNavigate } from 'react-router';
import { useUserContext } from '@/hooks/useUserContext';


const schema = z.object({
    userName: z.string().nonempty(),
    password: z.string().min(6)
});

type LoginValues = z.infer<typeof schema>;
const Login = () => {
    const [err, setErr] = useState('');
    const {login} = useUserContext();
    const [loading, setLoading] = useState(false);
    const { register, handleSubmit, formState: { errors } } = useForm<LoginValues>({
        resolver: zodResolver(schema),
        defaultValues: {
            userName: '',
            password: ''
        }
    })

    const navigate = useNavigate();
    const onSubmit = async (data: LoginValues) => {
        setErr('')
        setLoading(true)
        try {
            const res = await fetch('http://localhost:5255/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            if(res.status === 401 || res.status === 404) {
                setErr('Tài khoản hoặc mật khẩu không đúng');
                return;
            }
            if(!res.ok)
                throw new Error("Login failed");

            const token = await res.json();
            login(token.token)
            navigate('/');
        } catch (err) {
            console.log('error')
            setLoading(false)
        } finally {
            setLoading(false);
        }
    }
     
    return (
        <div className='w-full flex h-screen items-center justify-center bg-white'>
            <form onSubmit={handleSubmit(onSubmit)}>
                <div className='flex flex-col items-center border shadow-xl rounded-md px-6 py-8'>
                    <h3>  Đăng nhập</h3>
                    {err && <p className='text-destructive'>{err}</p>}
                    <div className='flex px-4 py-6 items-center justify-center space-x-3'>
                        <label>Tài khoản: </label>
                        <input
                            placeholder='Nhập tài khoản'
                            type='text'
                            {...register('userName')}
                            className='px-2 py-2 border rounded'
                        />
                        {errors.userName && <p className='text-destructive'>{errors.userName.message}</p>}
                    </div>
                    <div className='flex px-4 items-center justify-center space-x-3'>
                        <label>Mật khẩu: </label>
                        <input
                            placeholder='Nhập mẬt khẩu...'
                            type='password'
                            {...register('password')}
                            className='px-2 py-2 border rounded hover:border-amber-300 focus:border-b-blue-500'
                        />
                        {errors.password && <p className='text-destructive'>{errors.password.message}</p>}
                    </div>
                    <div className='py-2'>
                        <button
                            className='px-4 py-2 border rounded hover:bg-amber-400 transition'
                            type='submit'
                            disabled={loading}
                        >
                            {loading ? 'Đang đăng nhập...' : 'Đăng nhập'}
                        </button>
                    </div>
                </div>
            </form>

        </div>
    )
}

export default Login