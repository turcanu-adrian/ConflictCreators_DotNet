import { HubConnection } from "@microsoft/signalr";
import { Box, Button, Stack } from "@mui/material";
import axios from "axios";
import { useContext, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import { AuthContext } from "../providers/AuthenticationProvider";
import { ConnectionContext } from "../providers/ConnectionProvider";
import { GameContext } from "../providers/GameProvider";

const HomePage = () => {
    const navigate = useNavigate();
    const connectionContext = useContext(ConnectionContext);
    const gameContext = useContext(GameContext);
    const { gameId } = useParams();

    useEffect(() => {
        if (gameId && connectionContext.isConnected){
            connectionContext.connection!.invoke("JoinGame", "Guest", gameId);
            toast("game not found");
        }
    }, [gameId])

    return (
    <Stack alignItems='center' justifyContent='center' textAlign={'center'} m="auto" width={.5} spacing={5} position="relative">
        <Button variant="contained" sx={{fontSize: '1.2em'}} onClick={() => navigate("/play")}>Play!</Button>
        <Button variant="contained" sx={{fontSize: '1.2em'}} onClick={() => navigate("/prompts")}>Prompt Manager</Button>
        <Button variant="contained" sx={{fontSize: '1.2em'}} onClick={() => navigate("/leaderboard")}>LEADERBOARD</Button>
        <ToastContainer position={'bottom-right'}/>
    </Stack>)
}

export default HomePage;