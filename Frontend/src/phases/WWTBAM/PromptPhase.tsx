import { useContext, useEffect, useState } from "react";
import { HubConnection } from "@microsoft/signalr";
import AudienceContainer from "../../components/AudienceContainer";
import { PromptContainer } from "../../components/PromptContainer";
import { ConnectionContext } from "../../providers/ConnectionProvider";
import { GameContext } from "../../providers/GameProvider";
import { Cheat, PlayerType } from "../../types-and-interfaces/Enums";
import { WWTBAMState } from "../../types-and-interfaces/WWTBAMState";
import Button from "@mui/material/Button";
import { LinearProgress, Paper, Stack } from "@mui/material";
import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

const delay = 3;

const PromptPhase = () => {
    const [ rightAnswer, setRightAnswer ] = useState<string>("");
    const [ playerAnswer, setPlayerAnswer ] = useState<string>("");
    const [ inSafetyTier, setInSafetyTier ] = useState<Boolean>(false);
    const connection = useContext(ConnectionContext)!;
    const gameContext = useContext(GameContext)!;
    const gameState: WWTBAMState = gameContext.gameState! as WWTBAMState;

    useEffect(() => {
        connection!.on("ReceiveRightAnswer", (receivedAnswer: string) => {
            setTimeout(() => {
                if (receivedAnswer != playerAnswer)
                    connection.invoke("EndGame", gameState.id);
                else
                    if (((gameContext.gameState as WWTBAMState).currentTier+1)%5==0)
                        setInSafetyTier(true);
                    else
                    {
                        connection.invoke("ContinueGame", gameState.id);
                        setPlayerAnswer("");
                        setRightAnswer("");
                    }
            }, 2000);
                
            setRightAnswer(receivedAnswer);
        });

        return () => {
            connection!.off("ReceiveRightAnswer");
        }
    }, [playerAnswer]);

    return (<>
        <AudienceContainer/>
        { 
        <PromptContainer
            prompt={gameState.prompt}
            playerAnswer={playerAnswer}
            rightAnswer={rightAnswer}
            onClickHandler={(answer: string): void => {
                setPlayerAnswer(answer); 
                setTimeout(() => { 
                    sendAnswer(answer, connection, gameState.id)
                }, delay * 1000)
            }}/>
        }
        
        {(gameContext.joinedAs == PlayerType.host) 
        && 
        <Paper elevation={3}>
            <Stack direction={'row'} justifyContent={'space-evenly'}>
                <Button variant="contained" 
                onClick={() => 
                    { 
                        if (gameState.guestPlayers.length == 0)
                            toast("No guest players joined!");
                        else
                            connection.invoke("UseCheat", gameState.id, Cheat.guestAnswer); 
                    }} 
                disabled={!gameState.availableCheats.includes(Cheat.guestAnswer)}
                >USE GUEST ANSWER CHEAT</Button>
                <Button variant="contained" 
                onClick={() => 
                    { 
                        if (gameState.audienceCount == 0)
                            toast("No players in the audience!")
                        else
                            connection.invoke("UseCheat", gameState.id, Cheat.audienceAnswer); 
                    }}
                disabled={!gameState.availableCheats.includes(Cheat.audienceAnswer)}
                >USE AUDIENCE ANSWER CHEAT</Button>
                <Button variant="contained"
                onClick={() => { connection.invoke("UseCheat", gameState.id, Cheat.splitAnswers); }}
                disabled={!gameState.availableCheats.includes(Cheat.splitAnswers)}
                >USE SPLIT ANSWERS CHEAT</Button>    
            </Stack>    
        </Paper>
        }
        <Paper elevation={3}>
            <LinearProgress color={inSafetyTier? 'success' : 'warning'} variant="buffer" value={normalise(gameState.currentTier)} valueBuffer={normalise(gameState.currentTier+1)}/>
        </Paper>
        {(inSafetyTier
                && 
                <>
                <Stack direction={'row'} width={1} justifyContent={'space-evenly'}>
                    <Button variant="contained" onClick={() => { setInSafetyTier(false); connection.invoke("ContinueGame", gameState.id)}}>CONTINUE GAME</Button>
                    <Button variant="contained" onClick={() => { connection.invoke("EndGame", gameState.id)}}>END GAME</Button>
                </Stack>
                </>)}
        <ToastContainer/>
    </>)
}

const normalise = (value: number): number => (value* 100) / 14;

const sendAnswer = (answer: string, connection: HubConnection, gameId: string): void => {
    connection.invoke("SendPlayerAnswer", answer, gameId)
        .catch(function (err) {
            console.error(err.toString());
        });
}

export default PromptPhase;