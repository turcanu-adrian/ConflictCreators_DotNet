import { HubConnection } from '@microsoft/signalr';
import { useContext } from 'react';
import AudienceContainer from '../../components/AudienceContainer';
import { ConnectionContext } from '../../providers/ConnectionProvider';
import { GameContext } from '../../providers/GameProvider';
import { PlayerType } from '../../types-and-interfaces/Enums';
import Button from '@mui/material/Button';
import { Stack } from '@mui/material';


const LobbyPhase = () => {
    const connection = useContext(ConnectionContext)!;
    const gameContext = useContext(GameContext)!;
    const gameState = gameContext.gameState!;
    const joinedAs = gameContext.joinedAs!;
    
    console.log(gameState.id);

    return (<Stack spacing={2} sx={{height: 1}} alignItems={'center'}>
                <AudienceContainer/>
                {joinedAs == PlayerType.host ? 
                <Button variant={'outlined'} onClick={() => startGame(connection, gameState.id)}>START GAME</Button>
                : 
                <div>WAITING FOR GAME TO START...</div>}
            </Stack>);
}

const startGame = (connection: HubConnection, gameId: string): void => {
    connection.invoke("ContinueGame", gameId).catch(function (err) {
        console.error(err.toString());
    });    
}

export default LobbyPhase;