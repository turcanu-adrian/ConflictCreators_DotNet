import { HubConnection } from '@microsoft/signalr';
import Button from "../components/Button";
import { useContext } from "react";
import { ConnectionContext } from "../providers/ConnectionProvider";
import { GameContext } from "../providers/GameProvider";

const Lobby = () => {
    const connection = useContext(ConnectionContext)!;
    const gameState = useContext(GameContext).gameState!;
    const joinedAs = useContext(GameContext).joinedAs!;

    return (<div>
                <div>IN LOBBY PHASE</div>
                {joinedAs === "host" ? 
                <div><Button className='menubutton' onClick={() => startGame(connection, gameState.id)} text={"START GAME"}/></div> 
                : 
                <div>WAITING FOR GAME TO START...</div>}
            </div>);
}

const startGame = (connection: HubConnection, gameId: string): void => {
    connection.invoke("StartGame", gameId).catch(function (err) {
        console.error(err.toString());
    });    
}

export default Lobby;