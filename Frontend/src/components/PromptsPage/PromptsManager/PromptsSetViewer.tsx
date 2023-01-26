import { AccountCircle, ArrowForwardIosSharp, Delete, QuestionAnswer, Tag, Title } from "@mui/icons-material";
import { Button, Table, TableHead, TableRow, TableCell, TableBody, Stack, Accordion, AccordionDetails, AccordionSummary, Chip, IconButton, TextField, InputAdornment } from "@mui/material";
import { useQuery } from "@tanstack/react-query";
import { useContext, useState } from "react";
import { CreatorPromptDto, PromptDto } from "../../../types-and-interfaces/Dtos";
import { PromptsContext } from "../../../views/PromptsPage";
import AddPromptButton from "./Buttons/AddPromptButton";
import { fetchPromptsBySetId } from "./PromptsManager";

const PromptSetViewer = () =>{
    const promptsContext = useContext(PromptsContext);

    if (promptsContext.selectedSet != null)
        return (
            <Stack m={1} spacing={2} height={.9}> 
            {/* LABEL NAME SIZE */}
                <TextField variant='filled' defaultValue={promptsContext.selectedSet.creatorName} label={'Creator'} InputProps={{readOnly: true, startAdornment: (<InputAdornment position="start"><AccountCircle sx={{color: 'white'}}/></InputAdornment>)}}/>
                <TextField variant='filled' defaultValue={promptsContext.selectedSet.name} label={'Name'} InputProps={{readOnly: true, startAdornment: (<InputAdornment position="start"><Title sx={{color: 'white'}}/></InputAdornment>)}}/>
                <TextField variant='filled' defaultValue={promptsContext.selectedSet.tags} label={'Tags'} InputProps={{readOnly: true, startAdornment: (<InputAdornment position="start"><Tag sx={{color: 'white'}}/></InputAdornment>)}}/>
                <PromptsListViewer/>
            </Stack>
        )

    return <div></div>
}

const PromptsListViewer = () => {
    const promptsContext = useContext(PromptsContext);
    const [expanded, setExpanded] = useState<string|false>(false);
    const {status, data, error, refetch } = useQuery(['promptManager'], () => fetchPromptsBySetId(promptsContext.selectedSet!.id));
  
    const handleChange =
    (panel: string) => (event: React.SyntheticEvent, newExpanded: boolean) => {
      setExpanded(newExpanded ? panel : false);
    };

    console.log(data as PromptDto[]);

    return (<>
        <Stack sx={{overflowY: 'scroll'}} height={.6}>
            {(status=="loading" && <div>LOADING PROMPTS...</div>)
            ||
            (status=="error" && <div>FAILED LOADING PROMPTS</div>)
            ||
            (data?.length != 0 && (data as PromptDto[])?.map(it => 
            <Accordion key={it.id} elevation={5} expanded={expanded==it.id} onChange={handleChange(it.id)} disableGutters>
                <AccordionSummary expandIcon={<ArrowForwardIosSharp sx={{fontSize: '.9rem', transform: 'rotate(90deg)'}}/>} sx={{backgroundColor: '#9147ff'}}>
                    <Stack direction='row' alignItems={'center'}>
                        {it.question}
                    </Stack>
                </AccordionSummary>
                <AccordionDetails sx={{backgroundColor: '#212126', borderTop: '1px solid rgba(0, 0, 0, .125)'}}>
                    <Stack direction="row" justifyContent={'space-around'}>
                        {it.answers?.map(it => <Chip key={it} label={it} color={'primary'} icon={<QuestionAnswer/>}/>)}
                    </Stack>
                </AccordionDetails>
            </Accordion>))}
        </Stack>
    </>)
}

export default PromptSetViewer;