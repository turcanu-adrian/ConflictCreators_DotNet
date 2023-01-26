import { Paper, Stack, Button } from "@mui/material";
import { useContext } from "react";
import { toast } from "react-toastify";
import { ConnectionContext } from "../../providers/ConnectionProvider";
import { GameContext } from "../../providers/GameProvider";
import { Cheat } from "../../types-and-interfaces/Enums";
import { WWTBAMState } from "../../types-and-interfaces/WWTBAMState";

const CheatsContainer = () => {
    const gameContext = useContext(GameContext);
    const connectionContext = useContext(ConnectionContext);
    const gameState: WWTBAMState = gameContext.gameState! as WWTBAMState;

    return (
    <Paper elevation={3} sx={{backgroundColor: '#212126'}}>
    <Stack direction={'row'} justifyContent={'space-evenly'} spacing={3} m={1}>
        <Button variant="contained" size={'small'}
        onClick={() => 
            { 
                if (gameState.guestPlayers.length == 0)
                    toast("No guest players joined!");
                else
                    connectionContext.connection!.invoke("UseCheat", gameState.id, Cheat.guestAnswer); 
            }} 
        disabled={!gameState.availableCheats.includes(Cheat.guestAnswer)}
        >ASK A GUEST</Button>
        <Button variant="contained" size={'small'} 
        onClick={async () => 
            { 
                if (gameState.audienceCount == 0)
                    toast("No players in the audience!")
                else
                    await connectionContext.connection!.invoke("UseCheat", gameState.id, Cheat.audienceAnswer); 
            }}
        disabled={!gameState.availableCheats.includes(Cheat.audienceAnswer)}
        >ASK THE AUDIENCE</Button>
        <Button variant="contained" size={'small'}
        onClick={async () => { await connectionContext.connection!.invoke("UseCheat", gameState.id, Cheat.splitAnswers); }}
        disabled={!gameState.availableCheats.includes(Cheat.splitAnswers)} 
        >SPLIT THE ANSWERS</Button>    
    </Stack>    
</Paper>)
}

export default CheatsContainer;