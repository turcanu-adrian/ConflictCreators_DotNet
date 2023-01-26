import { AccountCircle, LooksOne, SportsScore, Stars, Timer } from "@mui/icons-material";
import { Button, Paper, Stack } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import { LeaderboardEntryDto } from "../types-and-interfaces/Dtos";
import { AvatarEnum } from "../types-and-interfaces/Enums";

const LeaderboardPage = () => {
    const { status, data } = useQuery(['leaderboard'], fetchLeaderboard);
    
    const navigate = useNavigate();

    return <Stack width={1} height={1} spacing={1} alignItems={'center'}>
        <Button variant="contained" onClick={() => navigate('/')}>GO BACK</Button>
        <Paper elevation={3} sx={{width: .6, height: .6, backgroundColor: '#2b2b40'}}>
            <Stack width={1} height={1} spacing={5} alignItems={'center'} justifyContent={'center'}>
                {/* <Stack direction='row' justifyContent={'space-evenly'} width={.8}>
                    <div>POSITION</div>
                    <div>AVATAR</div>
                    <div>NAME</div>
                    <div>ACHIEVEMENT<br/>POINTS</div>
                    <div>FASTEST GAME</div>
                </Stack> */}
                {(status == 'loading' && <div>LOADING DATA</div>)
                ||
                (status == 'error' && <div> ERROR LOADING DATA</div>)
                ||
                (data?.map((it, index) => <LeaderboardEntry entry={it} index={index}/>))}
            </Stack>
        </Paper>
        <ToastContainer position={'bottom-right'}/>
    </Stack>
}

const LeaderboardEntry = (props: {entry: LeaderboardEntryDto, index: number}) => {
    return <Paper sx={{ height: .1, width: .8, backgroundColor: '#9147ff' }}>
        <Stack direction='row' justifyContent={'space-evenly'} height={1} alignItems={'center'}>
            <Stack direction={'row'} width={.1}><Stars/>{props.index+1}</Stack>
            <img src={`./images/Avatars/${AvatarEnum[props.entry.avatar]}.webp`} height={'100%'} />
            <Stack width={.2} direction={'row'}><AccountCircle/>{props.entry.name}</Stack>
            <Stack width={.2} direction={'row'}><SportsScore/>{props.entry.achievementPoints}</Stack>
            <Stack width={.2} direction={'row'}><Timer/>{props.entry.fastestGame == '00:00:00' ? 'N/A' : props.entry.fastestGame.split('.')[0]}</Stack>
        </Stack>
    </Paper>
}

const fetchLeaderboard = async () => {
    const response = await axios.get<LeaderboardEntryDto[]>(`https://localhost:7242/api/users/getLeaderboard`,
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }});
    
    return response.data;
}

export default LeaderboardPage;