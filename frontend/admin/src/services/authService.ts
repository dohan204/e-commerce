import * as z from 'zod';

const API_URL = 'http://localhost:5255/api/auth'
export const loginSchema = z.object({
    userName: z.string().nonempty(),
    password: z.string()
});

export type LoginRequest = z.infer<typeof loginSchema>
export const authService = {
    async login(data: LoginRequest) {
        const response = await fetch(`${API_URL}/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })

        if (!response.ok)
            throw new Error(response.statusText)

        const result = await response.json();
        localStorage.setItem('token', result.token);
    },
    getToken(): string | any {
        return localStorage.getItem('token')
    },
    logout() {
        localStorage.removeItem('token')
    },
    isAuthenticated() {
        return !!localStorage.getItem("token");
    },
}