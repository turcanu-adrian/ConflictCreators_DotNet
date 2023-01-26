import { HubConnection } from "@microsoft/signalr";
import { FitScreen, Search } from "@mui/icons-material";
import { Button, Card, FormControlLabel, InputAdornment, Paper, Stack, styled, Switch, TextField } from "@mui/material";
import Grid from "@mui/material/Unstable_Grid2/Grid2";
import { useQuery } from "@tanstack/react-query";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import parseGameState from "../../functions/GameStateParsing";
import { ConnectionContext } from "../../providers/ConnectionProvider";
import { GamePhase } from "../../types-and-interfaces/Enums";
import { WWTBAMState } from "../../types-and-interfaces/WWTBAMState";

const cardStyle = {
    color: 'black',
    overflow: 'hidden',
    cursor: 'pointer',
    transition: 'box-shadow .2s',
    height: 1
}

const SearchTextField = styled(TextField)({
    '& .MuiFilledInput-root:before': {
          borderColor: 'purple'
    }
});

const FilterSwitch = styled(Switch)({
    '& .MuiSwitch-switchBase': {
        '&.Mui-checked': {
            color: 'purple'
        },
        '& + .MuiSwitch-track':{
            backgroundColor: 'purple'
        }
    }
})

const GameBrowser = () => {
    const connectionContext = useContext(ConnectionContext);
    const [searchValue, setSearchValue] = useState("");

    const [filters, setFilters] = useState({
        showFull: true,
        showEmpty: true
    });
    
    const {status, data, error, refetch} = useQuery(['games', searchValue, filters.showFull, filters.showEmpty], () =>  fetchGames(connectionContext.connection, searchValue, filters.showFull, filters.showEmpty));

    return (
    <Stack sx={{width: 1, height: 1}}>
        <Stack width={1} height={1} m={2}>
            <div>BROWSE GAMES</div>
            <Stack direction={'row'} spacing={2} m={1}>
                <SearchTextField variant='filled'
                InputProps={{
                            startAdornment: (<InputAdornment position="start"><Search sx={{color: 'white'}}/></InputAdornment>)
                        }} label={'Search'} value={searchValue} onChange={async (event) => {setSearchValue(event.currentTarget.value);}}>{searchValue}</SearchTextField>
                <FormControlLabel control={<FilterSwitch onChange={async (event) => {setFilters({...filters, showFull: event.target.checked});}}/>} label={"SHOW FULL GAMES"} checked={filters.showFull}/>
                <FormControlLabel control={<FilterSwitch onChange={async (event) => {setFilters({...filters, showEmpty: event.target.checked});}}/>} label={"SHOW EMPTY GAMES"} checked={filters.showEmpty}/>
            </Stack>
            <Grid container spacing={3} m={2} sx={{overflowY: 'scroll'}} height={1}>
                {(status == 'error' && <div>FAILED LOADING GAMES</div>)
                ||
                (status == 'loading' && <div>LOADING GAMES</div>)
                ||
                (status == 'success' && (data.length > 0 && data?.map((it:WWTBAMState) => <Grid key={it.id} xs={4} height={.4}>
                    <GameCard game={it}/> 
                </Grid>)) || <div>NO GAMES FOUND</div>)}
            </Grid>
        </Stack>
    </Stack>)
}

const GameCard = (props: {game: WWTBAMState}) => {
    const navigate = useNavigate();

    return <Card sx={cardStyle}>
        <Stack height={1}>
            <div style={{color: 'white', backgroundColor:'purple'}}>{props.game.hostPlayer.nickname}'s game</div>
            <div>Guest players: {props.game.guestPlayers.length}/{props.game.maxGuestPlayers}</div>
            <div>Current phase: {GamePhase[props.game.currentPhase]}</div>
            <div>Current tier: {props.game.currentTier}/{props.game.tiers.length}</div>
            <div style={{marginTop: 'auto'}}><Button variant={'contained'} sx={{backgroundColor: 'purple'}} onClick={() => navigate(`/${props.game.id}`)}>JOIN</Button></div>
        </Stack>
    </Card>
}

const fetchGames = async (connection: HubConnection|null, searchValue: string, showFull: boolean, showEmpty: boolean) => {
    const result = await connection?.invoke("GetAllGames", searchValue, showFull, showEmpty);

    return JSON.parse(result).map((it: any) => parseGameState(JSON.stringify(it)));
}

export default GameBrowser;