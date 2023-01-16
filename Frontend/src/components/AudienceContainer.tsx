import { Box, Button, CircularProgress, Paper, Stack } from "@mui/material";
import { useContext, useState } from "react";
import { GameContext } from "../providers/GameProvider";
import { Cheat, PlayerType } from "../types-and-interfaces/Enums";
import { Player } from "../types-and-interfaces/GameState";
import { WWTBAMState } from "../types-and-interfaces/WWTBAMState";
import PlayerCharacter from "./PlayerCharacter";

const colors: {[key:number]: string; } = {
    0: 'red',
    1: 'green',
    2: 'blue',
    3: 'white'
}

const AudienceContainer = () =>
{
    const joinedAs = useContext(GameContext).joinedAs;
    const gameState:WWTBAMState = useContext(GameContext).gameState! as WWTBAMState;
    const [selectedPlayerId, setSelectedPlayerId ] = useState<string>("");

    const isClickable = (id: string) : (()=>void)|undefined => {
        if (gameState.activeCheats.includes(Cheat.guestAnswer) && !selectedPlayerId && joinedAs == PlayerType.host)
            return (() => setSelectedPlayerId(id));
        return undefined;
    }

    return (
    <Stack alignItems={'center'} sx={{width: 1, height: 300}} spacing={1}>
        <Paper elevation={3} sx={{height: .5, width: 1}}>
            <Stack sx={{height: 1}} direction="row" justifyContent={'space-evenly'} alignItems={'center'} padding={1}>
                {(gameState.activeCheats.includes(Cheat.audienceAnswer) 
                && 
                (Object.keys(gameState.audienceAnswers).length == 0 ? <Stack alignItems={'center'}>Waiting for audience answers... <CircularProgress/></Stack> :
                Object.keys(gameState.audienceAnswers).map((it, index) => <Stack justifyContent={'center'} sx={{textAlign: 'center', backgroundColor: colors[index], width: gameState.audienceAnswers[it], height: .6, transition: 'width 1s'}}>{it}:{gameState.audienceAnswers[it]}</Stack>))) 
                || 
                <div>Audience count is {gameState.audienceCount}</div>}
            </Stack>
        </Paper>
        <Paper elevation={3} sx={{height: .5, width: 1}}>
            <Stack height={1} direction="row" justifyContent={'space-evenly'} alignItems={'center'} textAlign={'center'} spacing={2}>
                {(gameState.guestPlayers.length > 0 &&
                gameState.guestPlayers?.map((it: Player) => 
                <PlayerCharacter 
                    key={it.id} 
                    onClick={isClickable(it.id)} 
                    showAnswer={selectedPlayerId===it.id} 
                    playerInfo={it}/>
                    ))
                ||
                (<Stack alignItems={'center'}>Waiting for guest players to join...<CircularProgress/></Stack>)
                }
            </Stack>
        </Paper>
    </Stack>
    )
}

export default AudienceContainer;