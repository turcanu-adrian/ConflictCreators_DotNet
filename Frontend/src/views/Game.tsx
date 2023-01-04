import { Phases } from "../types-and-interfaces/Phases";
import { GameContext } from "../providers/GameProvider";
import { useContext } from "react";
import GameOverPhase from "../phases/GameOverPhase";
import LobbyPhase from "../phases/LobbyPhase";
import PromptPhase from "../phases/PromptPhase";

const Game = () => {
    const gameState = useContext(GameContext).gameState!;

    const phases: Phases = {
        lobby: <LobbyPhase/>,
        prompt: <PromptPhase key={new Date().getTime()}/>,
        gameover: <GameOverPhase/>
    };

    return (phases[gameState.currentPhase]);
}


export default Game;