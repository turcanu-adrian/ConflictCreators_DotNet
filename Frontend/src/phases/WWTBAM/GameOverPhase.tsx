import { Button } from "@mui/material";
import { useContext } from "react";
import AudienceContainer from "../../components/AudienceContainer";
import { GameContext } from "../../providers/GameProvider";
import { WWTBAMState } from "../../types-and-interfaces/WWTBAMState";

const GameOverPhase = () => {
    const gameContext = useContext(GameContext);
    const gameState = gameContext.gameState as WWTBAMState;

    return (<>
        <AudienceContainer/>
            <div>GAME OVER</div>
            <div>YOU MADE IT TO TIER {gameState.tiers[gameState.currentTier]}</div>
            <div>YOU'VE EARNED {gameState.hostPlayer.points} POINTS</div>
        <Button variant="contained" onClick={gameContext.endGame}>BACK TO MAIN MENU</Button>
    </>)
}

export default GameOverPhase;