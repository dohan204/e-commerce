import type { User } from "@/models/Users";
import { createContext } from "react";

export interface UserContextType {
    user: User | null,
    login: (token: string) => void
    logout?: () => void,
    loading?: boolean
}

export const UserContext = createContext<UserContextType | undefined>(undefined);

