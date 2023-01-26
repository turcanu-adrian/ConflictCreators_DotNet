import { useContext, useEffect, useState } from "react";
import { HubConnection } from "@microsoft/signalr";
import AudienceContainer from "../../components/GamePage/AudienceContainer";
import { PromptContainer } from "../../components/GamePage/PromptContainer";
import { ConnectionContext } from "../../providers/ConnectionProvider";
import { GameContext } from "../../providers/GameProvider";
import { Cheat, PlayerType } from "../../types-and-interfaces/Enums";
import { WWTBAMState } from "../../types-and-interfaces/WWTBAMState";
import Button from "@mui/material/Button";
import { LinearProgress, Paper, Stack } from "@mui/material";
import { toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import CheatsContainer from "../../components/GamePage/CheatsContainer";
import TiersBar from "../../components/GamePage/TiersBar";
import SafetyTierContainer from "../../components/GamePage/SafetyTierContainer";

const delay = 1;

const PromptPhase = () => {
    const [ rightAnswer, setRightAnswer ] = useState<string>("");
    const [ playerAnswer, setPlayerAnswer ] = useState<string>("");
    const [ inSafetyTier, setInSafetyTier ] = useState<boolean>(false);
    const connectionContext = useContext(ConnectionContext)!;
    const gameContext = useContext(GameContext)!;
    const gameState: WWTBAMState = gameContext.gameState! as WWTBAMState;

    useEffect(() => {
        connectionContext.connection!.on("ReceiveRightAnswer", (receivedAnswer: string) => {
            setTimeout(async () => {
                if (receivedAnswer != playerAnswer)
                    await connectionContext.connection!.invoke("EndGame", gameState.id);
                else
                {
                    setPlayerAnswer("");
                    setRightAnswer("");
                    if (((gameContext.gameState as WWTBAMState).currentTier+1)%5==0)
                        setInSafetyTier(true);
                    else
                        await connectionContext.connection!.invoke("ContinueGame", gameState.id);
                }
            }, 1000);
                
            setRightAnswer(receivedAnswer);
        });

        return () => {
            connectionContext.connection!.off("ReceiveRightAnswer");
        }
    }, [playerAnswer]);

    return (
    <Stack spacing={2} sx={{height: 1}} alignItems={'center'}>
        <AudienceContainer/>

        <TiersBar gameState={gameState}/>

        {(!inSafetyTier && 
        <>
            <PromptContainer prompt={gameState.prompt} playerAnswer={playerAnswer} rightAnswer={rightAnswer}
                onClickHandler={(answer: string): void => {
                    setPlayerAnswer(answer); 
                    setTimeout(async () => { 
                        await sendAnswer(answer, connectionContext.connection!, gameState.id)
                    }, delay * 1000)
                }}/>
            {gameContext.joinedAs == PlayerType.host && <CheatsContainer/>}
        </>)}

        {(inSafetyTier && <SafetyTierContainer setFalseSafetyTier={() => setInSafetyTier(false)}/>)}
    </Stack>)
}


const sendAnswer = async (answer: string, connection: HubConnection, gameId: string) => {
    await connection.invoke("SendPlayerAnswer", answer, gameId)
        .catch(function (err) {
            console.error(err.toString());
        });
}


export default PromptPhase;