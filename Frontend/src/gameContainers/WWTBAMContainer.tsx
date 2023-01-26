import { GameContext } from "../providers/GameProvider";
import { useContext } from "react";
import GameOverPhase from "../phases/WWTBAM/GameOverPhase";
import LobbyPhase from "../phases/WWTBAM/LobbyPhase";
import PromptPhase from "../phases/WWTBAM/PromptPhase";
import { GamePhase } from "../types-and-interfaces/Enums";
import { Stack } from "@mui/material";

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
    <Stack spacing={2} textAlign={'center'} height={1} margin={2} width={.8}>
        {phases[gameState.currentPhase]}
    </Stack>
        );
}


export default WWTBAMContainer;