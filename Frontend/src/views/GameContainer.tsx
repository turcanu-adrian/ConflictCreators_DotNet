import { useContext } from "react"
import { ConnectionContext } from "../providers/ConnectionProvider";
import { GameContext } from "../providers/GameProvider"
import GameMenu from "./GameMenu";
import WWTBAMContainer from "../gameContainers/WWTBAMContainer";
import { GameType } from "../types-and-interfaces/Enums";
import { Box } from "@mui/material";
import React from "react";

type GameTypesContainers = {
    [value in GameType]: JSX.Element;
};

const GameContainer = () => {
    const gameContext = useContext(GameContext);
    const connection = useContext(ConnectionContext);

    const gameTypesContainers: GameTypesContainers = {
        [GameType.WWTBAM]: <WWTBAMContainer/>
    };

    return (
    <Box display="flex" height={1}>
        <Box m="auto" width="50%">
            {(!connection && <div>Connecting to Hub...</div>)
            ||
            (gameContext.gameState && gameContext.joinedAs != null && <React.StrictMode>{gameTypesContainers[gameContext.gameState.gameType]}</React.StrictMode>)
            ||
            <GameMenu/>}
        </Box>
    </Box>
    )
}

export default GameContainer;