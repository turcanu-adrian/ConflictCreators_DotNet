import { SportsScore } from "@mui/icons-material";
import { Button, Card, Stack } from "@mui/material";
import { useContext } from "react";
import AudienceContainer from "../../components/GamePage/AudienceContainer";
import TiersBar from "../../components/GamePage/TiersBar";
import { GameContext } from "../../providers/GameProvider";
import { PlayerType } from "../../types-and-interfaces/Enums";
import { WWTBAMState } from "../../types-and-interfaces/WWTBAMState";

const GameOverPhase = () => {
    const gameContext = useContext(GameContext);
    const gameState = gameContext.gameState as WWTBAMState;

    // COLOR TEXT
    // DIFFERENT END SCREEN FOR GUESTS
    // GAME OVER STICKER
    // DIFFERENT BACKGROUND ON GAME OVER
    //SEPARATE SCREENS FOR WIN LOSE OR LEAVE WITH MONEY

    return (<Stack spacing={2} sx={{height: 1}} alignItems={'center'}>
        <AudienceContainer/>
        <TiersBar gameState={gameState}/>
        <Card sx={{width: .7, height: .2, backgroundColor: '#212126'}}>
            <Stack alignItems={'center'} width={1} height={1} marginTop={1} spacing={1}>
                <div>GAME OVER</div>
                {gameState.currentTier != 14 && <div>You have <b>LOST</b> at tier {gameState.currentTier}</div> || <div>You have <b>WON</b>!</div>}
                <Stack direction='row' alignItems={'center'}>You have earned <SportsScore/>{gameContext.joinedAs == PlayerType.host ? gameState.hostPlayer.points : gameState.guestPlayers[0].points} points!</Stack>

            </Stack>
        </Card>
    </Stack>)
}

export default GameOverPhase;