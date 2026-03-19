// model create
import * as z from 'zod';
export const schema = z.object({
    userName: z.string().nonempty("Tài khoản không được để trống"),
    email: z.string().email(),
    password: z.string().min(6, 'Mật khẩu không được nhỏ hơn 6 ký tự')
});

export type User = z.infer<typeof schema>;

export type UserResponse = {
    userId: string,
    name: string, 
    fullName: string,
    email: string,
    role: string,
    status: string,
    createdAt: string,
    updatedAt: string
}