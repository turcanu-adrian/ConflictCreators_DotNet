import { SportsEsports, Leaderboard, PlaylistPlay, Create, Info, MenuOpen, Help } from "@mui/icons-material";
import { Box, Button, Stack } from "@mui/material";
import { ReactNode, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../providers/AuthenticationProvider";
import ProfileContainer from "./ProfileContainer";

const drawerStyle = {
    position: 'fixed',
    left: 0,
    top: 0,
    backgroundColor: '#2b2b40', 
    transition: 'width .5s',
    whiteSpace: 'nowrap',
    overflow: 'hidden',
    borderRight: '5px solid #9147ff'
};

const openDrawerStyle = {
    ...drawerStyle,
    width: '12%'
}

const closedDrawerStyle = {
    ...drawerStyle,
    width: '4.5%'
}

const drawerButtonStyle = {
    justifyContent: 'flex-start',
    gap: '2em',
    transition: 'gap 1s',
    margin: 1,
    overflow: 'hidden'
}

const openDrawerButtonStyle = {
    ...drawerButtonStyle,
    gap: '.5em',
    transition: 'gap .5s'
};


const openDrawerSwitchContainerStyle = {
    ...openDrawerStyle,
    overflow: 'visible',
    backgroundColor: 'none',
    pointerEvents: 'none'
}

const closedDrawerSwitchContainerStyle = {
    ...closedDrawerStyle,
    overflow: 'visible',
    backgroundColor: 'none',
    pointerEvents: 'none'
}

const MiniDrawer = () => {
    const [open, setOpen] = useState(true);
    
    const navigate = useNavigate();
    const authContext = useContext(AuthContext);

    const handleDrawerOpen = () => {
        setOpen(true);
    };

    const handleDrawerClose = () => {
        setOpen(false);
    };

    return (
    <div>
        <Stack sx={open ? openDrawerStyle : closedDrawerStyle} height={1}>
            <Button variant="contained" onClick={() => navigate("/play")} sx={open ? openDrawerButtonStyle : drawerButtonStyle}>
                <SportsEsports fontSize="large"/><span>{'BROWSE GAMES'}</span>
                </Button>
            <Button variant="contained" onClick={() => navigate("/play/create")} sx={open ? openDrawerButtonStyle : drawerButtonStyle}>
                <Create fontSize="large"/><span>{'CREATE NEW GAME'}</span>
                </Button>
            <Button variant="contained" onClick={() => navigate("/prompts")} sx={open ? openDrawerButtonStyle : drawerButtonStyle}>
                <PlaylistPlay fontSize="large"/><span>{"PROMPTS"}</span>
                </Button>
            <Button variant="contained" onClick={() => navigate("/leaderboard")} sx={open ? openDrawerButtonStyle : drawerButtonStyle}>
                <Leaderboard fontSize="large"/><span>{'LEADERBOARD'}</span>
                </Button>
            <Button variant="contained" onClick={() => navigate("/howtoplay")} sx={open ? openDrawerButtonStyle : drawerButtonStyle}>
                <Help fontSize="large"/><span>{'HOW TO PLAY'}</span>
                </Button>
            <ProfileContainer open={open}/>
        </Stack>
        <Stack sx={open ? openDrawerSwitchContainerStyle : closedDrawerSwitchContainerStyle} height={1} spacing={2}>
            <Button variant="contained" size="small"
                sx={{position: 'absolute', top: '50%', right: '-2em', pointerEvents: 'auto'}} 
                onClick={open ? handleDrawerClose : handleDrawerOpen}>
                    <MenuOpen fontSize="large" sx={open && {} || {transform: 'scaleX(-1)'}}/>
            </Button>
        </Stack>
    </div>)
}


  
export default MiniDrawer;