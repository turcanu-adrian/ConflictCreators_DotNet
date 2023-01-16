import { GameContext } from "../providers/GameProvider";
import { useContext } from "react";
import GameOverPhase from "../phases/WWTBAM/GameOverPhase";
import LobbyPhase from "../phases/WWTBAM/LobbyPhase";
import PromptPhase from "../phases/WWTBAM/PromptPhase";
import { GamePhase, PlayerType } from "../types-and-interfaces/Enums";
import { WWTBAMState } from "../types-and-interfaces/WWTBAMState";
import { Button, Stack, TextField } from "@mui/material";

type PhasesContainers = {
    [value in GamePhase]: JSX.Element;
}

const WWTBAMContainer = () => {
    const gameContext = useContext(GameContext);
    const gameState = useContext(GameContext).gameState!;

    const phases: PhasesContainers = {
        [GamePhase.lobby]: <LobbyPhase/>,
        [GamePhase.prompt]: <PromptPhase/>,
        [GamePhase.gameover]: <GameOverPhase/>
    };

    return (
    <Stack spacing={2} textAlign={'center'}>
        {gameContext.joinedAs == PlayerType.host && <Button variant="contained" onClick={() => navigator.clipboard.writeText("http://localhost:3000/join?gameId=" + gameState.id)}>COPY INVITE LINK</Button>}
        {phases[gameState.currentPhase]}
    </Stack>
        );
}


export default WWTBAMContainer;