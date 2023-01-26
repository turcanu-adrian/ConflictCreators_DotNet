import { Card, Chip, Stack, styled } from "@mui/material";
import Grid from "@mui/material/Unstable_Grid2/Grid2";
import { useContext } from "react";
import { AuthContext } from "../../providers/AuthenticationProvider";
import { PromptSetDto } from "../../types-and-interfaces/Dtos";
import { PromptsContext } from "../../views/PromptsPage";

const cardStyle = {
    color: 'black',
    overflow: 'hidden',
    cursor: 'pointer',
    transition: 'box-shadow .2s',
    height: 1
}

const PromptsBrowserTab = (props: {cardSize: number}) => {
    const promptsContext = useContext(PromptsContext);
    const authContext = useContext(AuthContext);

    return (
    <Grid container spacing={3} m={2} sx={{overflowY: 'scroll'}} height={1}>
        {(!authContext.isAuth && promptsContext.activeTab != 0 && <div>YOU MUST BE LOGGED IN</div>)
        ||
        (promptsContext.query!.status == 'loading' && <div>FETCHING DATA...</div>)
        ||
        (promptsContext.query!.status == 'error' && <div>FAILED FETCHING DATA!</div>)
        ||
        ((promptsContext.query!.data as PromptSetDto[]).length == 0 && <div>NO PROMPTS FOUND</div> || (promptsContext.query!.data as PromptSetDto[]).map((it: PromptSetDto) => 
        <Grid key={it.id} sx={{background: 'none', display: 'block'}} onClick={() => promptsContext.changeSelectedSet!(it)} xs={props.cardSize} height={.4}>
                <PromptSetCard promptSet={it}/>
        </Grid>))}
    </Grid>)
}

const PromptSetCard = (props: {promptSet: PromptSetDto}) => {
    const promptsContext = useContext(PromptsContext);

    return (
        <Card sx={{...cardStyle, boxShadow: promptsContext.selectedSet?.id == props.promptSet.id ? '0 0 0 .5em #9147ff' : '0 0 0 .2em #2b2b40'}}>
            <Stack height={1}>
                <div style={{color: 'white', backgroundColor: 'purple', fontWeight: '900'}}>{props.promptSet.name}</div>
                <Grid container overflow={'hidden'} height={.6} spacing={.5}>{props.promptSet.tags.map((tag, index) => <Grid key={index} sx={{background: 'none'}}>
                    <Chip size={'small'} color="primary" variant="filled" label={'#'+tag}/>
                </Grid>)}</Grid>
                <div style={{padding: '2%', textAlign: 'right', marginTop: 'auto', color: 'gray', fontSize: '.8rem'}}>Created by: {props.promptSet.creatorName}</div>
            </Stack>
        </Card>
    )
}


export default PromptsBrowserTab;