import { Button, Paper, Stack } from "@mui/material";
import { ConnectionContext } from "../../providers/ConnectionProvider";
import { useContext, useEffect } from 'react';
import { GameContext } from "../../providers/GameProvider";
import { WWTBAMState } from "../../types-and-interfaces/WWTBAMState";

const SafetyTierContainer = (props: {setFalseSafetyTier: () => void}) => {
    const connectionContext = useContext(ConnectionContext);
    const gameContext = useContext(GameContext);
    const gameState: WWTBAMState = gameContext.gameState! as WWTBAMState;

    useEffect(() => {
        if (gameState.currentTier == 14)
            connectionContext.connection!.invoke("EndGame", gameState.id);
    }, []);

    return (
        <Paper sx={{width: .7, backgroundColor: '#212126', height: .3}}>
            <Stack width={1} height={1} justifyContent={'center'} spacing={1}>
                    <div>You've reached a safety tier!</div>
                    <div>You may continue playing or end the game and keep the points you've earned so far.</div>
                    <Stack direction={'row'} width={1} justifyContent={'space-evenly'}>
                        <Button variant="contained" onClick={async () => { props.setFalseSafetyTier(); await connectionContext.connection!.invoke("ContinueGame", gameState.id)}}>CONTINUE GAME</Button>
                        <Button variant="contained" onClick={async () => { await connectionContext.connection!.invoke("EndGame", gameState.id)}}>END GAME</Button>
                    </Stack>
                </Stack>
        </Paper>
    )
}

export default SafetyTierContainer;