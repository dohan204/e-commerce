import { UserContext } from "@/context/UserContext"
import type { DecodedToken, User } from "@/models/Users";
import { jwtDecode } from "jwt-decode";
import { useContext, useEffect, useState, type ReactNode } from "react"

export const useUserContext = () => {
    const context = useContext(UserContext);
    if (!context)
        throw new Error('useUserContext must be used within a UserProvider')
    return context;
}


export const UserProvider = ({ children }: { children: ReactNode }) => {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState<boolean>(true);

    const checkTokenValid = (token: string): boolean => {
        try {
            const decode = jwtDecode<DecodedToken>(token);
            const currentTime = Date.now() / 1000;

            if(decode.exp < currentTime) {
                console.warn("token ddax het han");
                return false;
            }
            return true;
        } catch {
            return false;
        }
    }
    useEffect(() => {
        const token = localStorage.getItem('token');

        if (token && checkTokenValid(token)) {
            try {
                const decodeToken = jwtDecode<User>(token);
                // console.log(decodeToken)
                setUser(decodeToken);
            } catch (err) {
                console.error("Token không hợp lệ:", err);
                localStorage.removeItem('token');
            }
        }
        setLoading(false);
    }, [])

    const login = (token: string) => {
        localStorage.setItem('token', token);
        const decoded = jwtDecode<User>(token);
        setUser(decoded);
    };
    const logout = () => {
        localStorage.removeItem('token');
        setUser(null);
    };

    return (
        <UserContext.Provider value={{user, login, logout, loading}}>
            {children}
        </UserContext.Provider>
    )
}