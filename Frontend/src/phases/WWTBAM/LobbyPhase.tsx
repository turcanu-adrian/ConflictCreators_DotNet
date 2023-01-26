import { HubConnection } from '@microsoft/signalr';
import { useContext } from 'react';
import AudienceContainer from '../../components/GamePage/AudienceContainer';
import { ConnectionContext } from '../../providers/ConnectionProvider';
import { GameContext } from '../../providers/GameProvider';
import { PlayerType } from '../../types-and-interfaces/Enums';
import Button from '@mui/material/Button';
import { Stack } from '@mui/material';


const LobbyPhase = () => {
    const connectionContext = useContext(ConnectionContext)!;
    const gameContext = useContext(GameContext)!;
    const gameState = gameContext.gameState!;
    const joinedAs = gameContext.joinedAs!;

    return (<Stack spacing={2} sx={{height: 1}} alignItems={'center'}>
                <AudienceContainer/>
                {joinedAs == PlayerType.host ? 
                <Button variant={'contained'} onClick={async () => await startGame(connectionContext.connection!, gameState.id)}>START GAME</Button>
                : 
                <div>WAITING FOR GAME TO START...</div>}
            </Stack>);
}

const startGame = async (connection: HubConnection, gameId: string) => {
    await connection.invoke("ContinueGame", gameId).catch(function (err) {
        console.error(err.toString());
    });    
}

export default LobbyPhase;