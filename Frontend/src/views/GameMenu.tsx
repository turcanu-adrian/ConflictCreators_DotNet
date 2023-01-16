import { HubConnection } from '@microsoft/signalr';
import { useContext, useEffect, useState } from 'react';
import { ConnectionContext } from '../providers/ConnectionProvider';
import { Button, Stack, TextField } from '@mui/material';
import { ToastContainer,  toast } from 'react-toastify';
import { join } from 'path';
import { AuthContext } from '../providers/AuthenticationProvider';
import AuthContainer from '../components/AuthContainer';
import axios from 'axios';

const GameMenu = () => {
    const [ nickname, setNickname ] = useState<string>("");
    const connection = useContext(ConnectionContext);
    const authContext = useContext(AuthContext);

    useEffect(() => {
        const gameId = window.location.href.split("/join?gameId=")[1];
        if (gameId != undefined)
        {
            toast('NO GAME FOUND');
            joinGame(connection!, localStorage.getItem("userName")!, gameId);
        }
    }, [])

    return (
        <Stack alignItems='center' sx={{border: '1px solid white'}}>
            {!authContext.isAuth && <TextField margin="normal" label={"Nickname"} id="outlined-basic" value={nickname} onChange={(e) => setNickname(e.target.value)} error={nickname===""} helperText="nickname cannot be empty"/>}
            <Button sx={{m:1}} variant="outlined" onClick={() => {
                if (authContext.isAuth && localStorage.getItem)
                    createGame(connection!, localStorage.getItem("userName")!)
                if (nickname!="")
                    createGame(connection!, nickname)
                }} color={nickname==="" ? 'error' : 'success'}>CREATE NEW GAME</Button>
                {authContext.isAuth ? <div>LOGGED IN AS {localStorage.getItem("userName")}</div> : <Button variant="outlined" onClick={() => authContext.handleLogin({username: "gusky", password: "123456"})}>LOGIN</Button>}
                <Button variant="outlined" onClick={() => authContext.handleRegister({username: "gusky", email:"gusky651@gmail.com", password: "123456"})}>REGISTER</Button>
                <Button variant="outlined" onClick={sendPromptSet}>ADD PROMPT SET</Button>
                <Button variant="outlined" onClick={sendPrompt}>ADD PROMPT</Button>

            <ToastContainer/>
        </Stack>
        )
}


const sendPrompt = () => {
    axios.post("https://localhost:7242/api/prompts/add",
     {promptSetId: '97d2cfc1a93646a990abf53166da7630', question: 'What color is the sun?', correctAnswer: 'Yellow', wrongAnswers: ['Green', 'Blue', 'Orange']}
     , {headers: {
        Authorization: `Bearer ${localStorage['authToken']}`
     }})
        .then((result: any) => console.log(result))
        .catch((err: any) => console.log(err));
}

const sendPromptSet = () => {
    axios.post("https://localhost:7242/api/prompts/addSet",
     {name: "Prompt set name", tags: ["funny", "amazing"]}
     , {headers: {
        Authorization: `Bearer ${localStorage['authToken']}`
     }})
        .then((result: any) => console.log(result))
        .catch((err: any) => console.log(err));
}

const createGame = (connection: HubConnection, name: string): void => {
    connection.invoke("CreateNewGame", name).catch(function (err) {
        console.error(err.toString());
    });
}

const joinGame = (connection: HubConnection, name: string, gameId: string): void => {
    console.log(gameId)
    connection.invoke("JoinGame", name, gameId).catch(function (err) {
        console.error(err.toString());
    })
} 

export default GameMenu;