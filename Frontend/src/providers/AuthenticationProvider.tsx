import axios from "axios";
import { createContext, ReactNode, useEffect, useState } from "react";

export interface LoginPayload {
    userName: string,
    password: string
};

export interface RegisterPayload {
    displayName: string,
    userName: string,
    email: string,
    password: string
}

export const AuthContext = createContext<{
    isAuth: boolean
}>({
    isAuth: false
});

export const AuthenticationProvider = ({ children }:{ children: ReactNode }) => {
    const [isAuth, setIsAuth ] = useState(!!localStorage.getItem("authToken"));

    useEffect(() => {
        const tokenExpires = localStorage.getItem("tokenExpires");
        if (tokenExpires)
        {
            const currentDate = new Date().getTime();
            const tokenExpirationDate = Date.parse(tokenExpires);
            
            if ((tokenExpirationDate - currentDate)/60000 < 10)
            {
                localStorage.clear();
                window.location.href = '/';
            }
        }
    }, []);

    return <AuthContext.Provider value={{ isAuth }}>{children}</AuthContext.Provider>
}