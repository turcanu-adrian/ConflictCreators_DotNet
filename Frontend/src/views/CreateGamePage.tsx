import { HubConnection } from "@microsoft/signalr";
import { Looks3Outlined, Looks4Outlined, Looks5Outlined, LooksOne, LooksOneOutlined, LooksTwoOutlined } from "@mui/icons-material";
import { Stack, FormControl, InputLabel, Select, MenuItem, Button, TextField, Modal, Box, Paper, paperClasses, ToggleButtonGroup, ToggleButton, styled } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import PromptsBrowser from "../components/PromptsPage/PromptsBrowser";
import { AuthContext } from "../providers/AuthenticationProvider";
import { ConnectionContext } from "../providers/ConnectionProvider";
import { PromptSetDto } from "../types-and-interfaces/Dtos";
import { GameType } from "../types-and-interfaces/Enums";
import { fetchTab, PromptsContext, route } from "./PromptsPage";

const modalStyle = {
    bgcolor: 'background.paper'
}



const StyledToggleButton = styled(ToggleButton)({
    '&.Mui-selected, &.Mui-selected:hover': {
        backgroundColor: 'purple'
    }
});

const CreateGamePage = () => {
    const [activeTab, setActiveTab] = useState<number>(0);
    const [selectedSet, setSelectedSet] = useState<PromptSetDto|null>(null);
    const [searchString, setSearchString] = useState<string>("");

    const query = useQuery([route[activeTab]], () => fetchTab(activeTab, searchString));
    const navigate = useNavigate();

    const changeSelectedSet = (ps: PromptSetDto|null) => {
        setSelectedSet(ps);
    }
    
    const changeActiveTab = (n: number) => {
        setActiveTab(n);
    }

    const changeSearchString = (newString: string) => {
        setSearchString(newString);
    }
    
    const [gameType, setGameType] = useState<GameType>(GameType.WWTBAM);
    const [guestPlayersNo, setGuestPlayersNo ] = useState<number>(5);

    const connectionContext = useContext(ConnectionContext);

    const handleGameType = (event: React.MouseEvent<HTMLElement>, newGameType: GameType|null) => {
        if (newGameType != null)
            setGameType(newGameType);
    }

    const handleGuestPlayers = (event: React.MouseEvent<HTMLElement>, newGuestPlayersNo: number|null) => {
        if (newGuestPlayersNo != null)
            setGuestPlayersNo(newGuestPlayersNo);
    } 

    return (
        <Stack spacing={1} alignItems={'center'} width={1} height={.85}>
            <Button variant="contained" onClick={() => navigate(-1)}>GO BACK</Button>
            <Paper elevation={3} sx={{height: 1, width: .5, backgroundColor: '#2b2b40'}}>
                <Stack height={1} width={1} spacing={2}  alignItems={'center'} marginTop={1}>
                    <Stack direction={'row'} gap={2} alignItems={'center'}>
                        <div>Game type:</div>
                        <ToggleButtonGroup value={gameType} exclusive onChange={handleGameType}>
                            <StyledToggleButton value={GameType.WWTBAM}>WWTBAM</StyledToggleButton>
                        </ToggleButtonGroup>
                    </Stack>
                    <Stack direction={'row'} gap={2} alignItems={'center'}>
                        <div>Max guest players:</div>
                        <ToggleButtonGroup value={guestPlayersNo} exclusive onChange={handleGuestPlayers}>
                            <StyledToggleButton value={1}><LooksOneOutlined/></StyledToggleButton>
                            <StyledToggleButton value={2}><LooksTwoOutlined/></StyledToggleButton>
                            <StyledToggleButton value={3}><Looks3Outlined/></StyledToggleButton>
                            <StyledToggleButton value={4}><Looks4Outlined/></StyledToggleButton>
                            <StyledToggleButton value={5}><Looks5Outlined/></StyledToggleButton>
                        </ToggleButtonGroup>
                    </Stack>
                    <Stack width={.8} height={.75} alignItems={'center'} spacing={2}>
                        <PromptsContext.Provider value={{selectedSet, changeSelectedSet, activeTab, changeActiveTab, query, searchString, changeSearchString}}>
                            <PromptsBrowser width={1} height={1} canCreate={false} cardSize={3}/>
                            <Button variant="contained" onClick={() => {
                                    if (!selectedSet)
                                        toast.error('You must select a prompts set!')
                                    else
                                        createGame(connectionContext.connection!, "Guest", gameType, guestPlayersNo, selectedSet.id)
                                }
                                }>CREATE GAME</Button>
                        </PromptsContext.Provider>
                    </Stack>
                </Stack>
            </Paper>
        <ToastContainer position={'bottom-right'}/>
        </Stack>
    )
}


const createGame = async (connection: HubConnection, name: string, gameType: GameType, guestPlayersNo: number, promptsSetId: string) => {
    await connection.invoke("CreateNewGame", name, gameType, guestPlayersNo, promptsSetId).catch(function (err: any) {
        console.error(err.toString());
    });
}

export default CreateGamePage;