import { ArrowForwardIosSharp, Delete, ExpandMore, QuestionAnswer, Tag, Title } from "@mui/icons-material";
import { Accordion, AccordionDetails, AccordionSummary, Box, Chip, IconButton, InputAdornment, Stack, Table, TableBody, TableCell, TableHead, TableRow, TextField } from "@mui/material";
import { useMutation, useQuery } from "@tanstack/react-query";
import axios from "axios";
import { SyntheticEvent, useContext, useState } from "react";
import { CreatorPromptDto, PromptSetDto } from "../../../types-and-interfaces/Dtos";
import { PromptsContext } from "../../../views/PromptsPage";
import AddPromptButton from "./Buttons/AddPromptButton";
import { fetchPromptsBySetId } from "./PromptsManager";

const PromptsSetEditor = () => {
    const promptsContext = useContext(PromptsContext);

    if (promptsContext.selectedSet != null)
        return (
        <Stack spacing={2} height={1}>
            <TextField InputProps={{startAdornment: (<InputAdornment position="start"><Title sx={{color: 'white'}}/></InputAdornment>)}} label="Name" variant="filled" defaultValue={promptsContext.selectedSet.name} onChange={(event) => promptsContext.changeSelectedSet!({...promptsContext.selectedSet!, name: event.currentTarget.value})}>{promptsContext.selectedSet.name}</TextField>
            <TextField InputProps={{startAdornment: (<InputAdornment position="start"><Tag sx={{color: 'white'}}/></InputAdornment>)}} label="Tags" variant="filled" defaultValue={promptsContext.selectedSet.tags} onChange={(event) => promptsContext.changeSelectedSet!({...promptsContext.selectedSet!, tags: event.currentTarget.value.split(",")})}>
                {promptsContext.selectedSet.tags?.map(it =><div>it</div>)}
            </TextField>
            <PromptsListEditor/>
        </Stack>)
    
    return <div></div>
}

const accordionStyle = {
    color: 'black'
};

const PromptsListEditor = () => {
    const promptsContext = useContext(PromptsContext);
    const [expanded, setExpanded] = useState<string|false>(false);
    const {status, data, error, refetch } = useQuery(['promptManager'], () => fetchPromptsBySetId(promptsContext.selectedSet!.id));
    const {isError, isLoading, isSuccess, mutate} = useMutation(deletePrompt, {onSuccess: () => refetch()});
    
    const handleChange =
    (panel: string) => (event: React.SyntheticEvent, newExpanded: boolean) => {
      setExpanded(newExpanded ? panel : false);
    };

    return (<>
        <Stack sx={{overflowY: 'auto'}} height={.65}>
            {(status=="loading" && <div>LOADING PROMPTS...</div>)
            ||
            (status=="error" && <div>FAILED LOADING PROMPTS</div>)
            ||
            (data?.length != 0 && (data as CreatorPromptDto[]).map(it => 
            <Accordion key={it.id} elevation={5} expanded={expanded==it.id} onChange={handleChange(it.id)} disableGutters>
                <AccordionSummary expandIcon={<ArrowForwardIosSharp sx={{fontSize: '.9rem', transform: 'rotate(90deg)'}}/>} sx={{backgroundColor: '#9147ff'}}>
                    <Stack direction='row' alignItems={'center'}>
                        <IconButton size={'small'} onClick={(e) => {
                            e.stopPropagation();
                            mutate(it.id);
                        }}><Delete/></IconButton>
                        {it.question}
                    </Stack>
                </AccordionSummary>
                <AccordionDetails sx={{backgroundColor: '#212126', borderTop: '1px solid rgba(0, 0, 0, .125)'}}>
                    <Stack direction="row" justifyContent={'space-around'}>
                        <Chip label={it.correctAnswer} icon={<QuestionAnswer/>} color={'success'}/>
                        {it.wrongAnswers?.map(it => <Chip label={it} color={'error'} icon={<QuestionAnswer/>}/>)}
                    </Stack>
                </AccordionDetails>
            </Accordion>))}
        </Stack>
        <AddPromptButton refetch={refetch}/>
    </>)
}

// ALIGN PROMPT BUTTON

const deletePrompt = async (promptId: string) => {
    return axios.post(`https://localhost:7242/api/prompts/remove/${promptId}`,
        {},
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }})
}

export default PromptsSetEditor;