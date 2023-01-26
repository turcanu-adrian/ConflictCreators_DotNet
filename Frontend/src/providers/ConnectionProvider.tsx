import {createContext, useState, useEffect, useContext } from "react";
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr';
import { AuthContext } from "./AuthenticationProvider";

const ConnectionContext = createContext<{connection: HubConnection | null, isConnected: boolean}>({connection: null, isConnected: false});

type ConnectionProviderProps = {
    children: React.ReactNode;
};

const ConnectionProvider = (props: ConnectionProviderProps) => {
    const [ connection, setConnection ] = useState<HubConnection | null>(null);
    const [ isConnected, setIsConnected ] = useState<boolean>(false);
    const authContext = useContext(AuthContext);

    useEffect(() => {
            if (authContext.isAuth) {
                const newConnection = new HubConnectionBuilder().withUrl('https://localhost:7242/gameHub', { accessTokenFactory: () => {
                    const accessToken = localStorage.getItem('authToken')!;
                    return accessToken;
                }}).withAutomaticReconnect().build();
                setConnection(newConnection);
                newConnection.start().then(async () => {
                    setIsConnected(true);
                    await newConnection.invoke('CheckPlayerStatus');
                });
            }
            else 
            {
                const newConnection = new HubConnectionBuilder().withUrl('https://localhost:7242/gameHub').withAutomaticReconnect().build();
                setConnection(newConnection);
                newConnection.start().then(() => setIsConnected(true));
            }
    }, []);

    return <ConnectionContext.Provider value={{connection: connection, isConnected: isConnected}}>{props.children}</ConnectionContext.Provider>

}

export {ConnectionContext, ConnectionProvider}