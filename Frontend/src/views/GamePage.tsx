import { useContext } from "react"
import { GameContext } from "../providers/GameProvider"
import WWTBAMContainer from "../gameContainers/WWTBAMContainer";
import { GameType } from "../types-and-interfaces/Enums";
import { Button, Paper, Stack } from "@mui/material";
import { ConnectionContext } from "../providers/ConnectionProvider";
import { useNavigate } from "react-router-dom";
import GameBrowser from "../components/GamePage/GameBrowser";
import { ToastContainer } from "react-toastify";

type GameTypesContainers = {
    [value in GameType]: JSX.Element;
};

const GamePage = () => {
    const gameContext = useContext(GameContext);
    const connectionContext = useContext(ConnectionContext);
    const navigate = useNavigate();

    const isInGame = gameContext.gameState != null && gameContext.joinedAs != null;

    const gameTypesContainers: GameTypesContainers = {
        [GameType.WWTBAM]: <WWTBAMContainer/>
    };

    return (
    <Stack alignItems={'center'} m={'auto'} spacing={2} height={.7}>
        {isInGame && <Button variant="contained" onClick={gameContext.leaveGame}>LEAVE GAME</Button> || <Button variant="contained" onClick={() => navigate("/")}>GO BACK</Button>}
        <Paper sx={{width: .5, height: 1, background: 'none'}} elevation={3}>
            <Stack width={1} alignItems={'center'} m={'auto'} spacing={2} bgcolor={'#2b2b40'} height={1} borderRadius={1}>
                {!connectionContext.isConnected && <><div>Connecting to hub....</div></>
                ||
                (isInGame && <>{gameTypesContainers[gameContext.gameState!.gameType]}</>)
                ||
                (connectionContext.isConnected && <GameBrowser/>)}
            </Stack>
        </Paper>
        <ToastContainer position={'bottom-right'}/>
    </Stack>
    )
}

export default GamePage;