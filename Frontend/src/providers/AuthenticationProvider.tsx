import axios from "axios";
import { createContext, ReactNode, useState } from "react";

export const AuthContext = createContext({isAuth: false, handleLogin: (loginPayload: {username: string, password: string}) => {}, handleRegister: (registerPayload : {username: string, email: string, password: string}) => {}});

export const AuthenticationProvider = ({ children }:{ children: ReactNode }) => {
    const [isAuth, setIsAuth ] = useState(!!localStorage.getItem("authToken"));

    const handleLogin = (loginPayload: {username: string, password: string}) => {
        axios.post("https://localhost:7242/api/users/login", loginPayload)
            .then((response: any) => {
                const token = response.data.token;
                const userName = response.data.username;

                localStorage.setItem("authToken", token);
                localStorage.setItem("userName", userName);
                window.location.href = '/'
            })
            .catch((err: any) => console.log(err));
    }

    const handleRegister = (registerPayload: {username: string, email: string, password: string}) => {
        axios.post("https://localhost:7242/api/users/register", registerPayload)
            .then((response: any) => {
                console.log("register response");
                console.log(response);
            })
    }

    return <AuthContext.Provider value={{ isAuth, handleLogin, handleRegister}}>{children}</AuthContext.Provider>
}