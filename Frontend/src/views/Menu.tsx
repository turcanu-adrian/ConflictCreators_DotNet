import { HubConnection } from '@microsoft/signalr';
import Button from "../components/Button";
import { useContext, useState } from 'react';
import { ConnectionContext } from '../providers/ConnectionProvider';

const Menu = () => {
    const [gameId, setGameId] = useState("");
    const connection = useContext(ConnectionContext);

    return (<div>
        <div><Button className='menubutton' onClick={() => createGame(connection!, "gusky")} text={"CREATE NEW GAME"}/></div>
        <div><input type="text" value={gameId} onChange={(e) => setGameId(e.target.value)}/></div>
        <div><Button className='menubutton' onClick={() => joinGame(connection!, "gusky", gameId)} text={"JOIN GAME"}/></div>
    </div>)
}

const createGame = (connection: HubConnection, name: string): void => {
    connection.invoke("CreateNewGame", name).catch(function (err) {
        console.error(err.toString());
    });
}

const joinGame = (connection: HubConnection, name: string, gameId: string): void => {
    connection.invoke("JoinGame", name, gameId).catch(function (err) {
        console.error(err.toString());
    })
} 

export default Menu;