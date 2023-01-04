import Button from "../components/Button";
import { useContext, useEffect, useState } from "react";
import { HubConnection } from "@microsoft/signalr";
import { Prompt } from "../types-and-interfaces/Prompt";
import { ConnectionContext } from "../providers/ConnectionProvider";
import { GameContext } from "../providers/GameProvider";

const PromptPhase = () => {
    const [ rightAnswer, setRightAnswer ] = useState<number>(5);
    const [ playerAnswer, setPlayerAnswer ] = useState<number>(5);
    const connection = useContext(ConnectionContext)!;
    const gameState = useContext(GameContext).gameState!;

    useEffect(() => {
        connection!.on("ReceiveRightAnswer", (receivedRightAnswer: string) => {
            setRightAnswer(gameState.prompt.currentQuestionAnswers.findIndex(it => it === receivedRightAnswer));
        });

        return () => {
            connection!.off("ReceiveRightAnswer");;
        }
    }, [gameState.prompt]);

    return (<div>
        <div>IN PROMPT PHASE</div>
        <div>CURRENT QUESTION = {gameState.prompt.currentQuestion}</div>
        <div>ANSWERS ARE</div>
        <div>
            <Button
                className={checkAnswer(0, playerAnswer, rightAnswer)}
                onClick={() => {setPlayerAnswer(0); sendAnswer(0, gameState.prompt, connection, gameState.id)}} 
                text={gameState.prompt.currentQuestionAnswers[0]}/>
        </div>
        <div>
            <Button
                className={checkAnswer(1, playerAnswer, rightAnswer)}
                onClick={() => {setPlayerAnswer(1); sendAnswer(1, gameState.prompt, connection, gameState.id)}} 
                text={gameState.prompt.currentQuestionAnswers[1]}/>
        </div>
        <div>
            <Button 
                className={checkAnswer(2, playerAnswer, rightAnswer)}
                onClick={() => {setPlayerAnswer(2); sendAnswer(2, gameState.prompt, connection, gameState.id)}} 
                text={gameState.prompt.currentQuestionAnswers[2]}/>
        </div>
        <div>
            <Button
                className={checkAnswer(3, playerAnswer, rightAnswer)}
                onClick={() => {setPlayerAnswer(3); sendAnswer(3, gameState.prompt, connection, gameState.id)}} 
                text={gameState.prompt.currentQuestionAnswers[3]}/>
        </div>
    </div>)
}

const checkAnswer = (n: number, playerAnswer: number, rightAnswer: number): string => {
    if (n == playerAnswer)
        {
            if (rightAnswer != 5)
                {
                    if (n == rightAnswer)
                        return 'rightAnswer';
                    return 'wrongAnswer';
                }
            return 'checkingAnswer';
        }
    return 'neutralAnswer';
}

const sendAnswer = (n: number, prompt: Prompt, connection: HubConnection, gameId: string): void => {
    connection.invoke("SendPlayerAnswer", prompt.currentQuestionAnswers[n], gameId).catch(function (err) {
        console.error(err.toString());
    });
}

export default PromptPhase;