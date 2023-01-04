import {createContext, useState, useEffect } from "react";
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr';

const ConnectionContext = createContext<HubConnection | null>(null);

type ConnectionProviderProps = {
    children: React.ReactNode;
};

const ConnectionProvider = (props: ConnectionProviderProps) => {
   const [ connection, setConnection ] = useState<HubConnection | null>(null);

    useEffect(() => {
        const newConnection = new HubConnectionBuilder().withUrl('https://localhost:7025/gamehub').withAutomaticReconnect().build();
        setConnection(newConnection);
    }, []);

    if (connection)
        return <ConnectionContext.Provider value={connection}>{props.children}</ConnectionContext.Provider>
    
    return <div>CONNECTING TO HUB...</div>
}

export {ConnectionContext, ConnectionProvider}