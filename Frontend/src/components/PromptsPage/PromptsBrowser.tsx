import { Add, LibraryAdd, Search } from "@mui/icons-material";
import { Box, Tabs, Tab, Button, Popover, Stack, TextField, InputAdornment, Paper } from "@mui/material";
import { style } from "@mui/system";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useContext, useState } from "react";
import { PromptsContext } from "../../views/PromptsPage";
import PromptsBrowserTab from "./PromptsBrowserTab";

const TabPanel = (props: {children: React.ReactNode, index: number, value: number}) => {
    const {children, value, index} = props;

    return (<div style={{height: '78%'}} hidden={value !== index}>{value == index &&(<Box height={.97}>{children}</Box>)}</div>)
}



const PromptsBrowser = (props: {width: number, height: number, canCreate: boolean, cardSize: number}) => {
    const promptsContext = useContext(PromptsContext);

    const [anchorEl, setAnchorEl] = useState<HTMLButtonElement | null>(null);

    const handleChange = (event: React.SyntheticEvent, newValue: number) => {
        promptsContext.changeActiveTab!(newValue);
        promptsContext.changeSelectedSet!(null);
    }

    return (
        <Paper elevation={10} sx={{width: props.width, height: props.height, backgroundColor: '#2b2b40'}}>
            <Stack width={1} height={1}>
                <Stack direction="row" m={1} sx={{borderBottom: 1, borderColor: '#9147ff'}} spacing={3}>
                    <Tabs value={promptsContext.activeTab} onChange={handleChange}>
                        <Tab label="Browse"/>
                        <Tab label="Favourites"/>
                        <Tab label="Created"/>
                    </Tabs>
                </Stack>
                <TabPanel value={promptsContext.activeTab} index={0}>
                    <PromptsBrowserTab cardSize={props.cardSize}/>
                </TabPanel>
                <TabPanel value={promptsContext.activeTab} index={1}>
                    <PromptsBrowserTab cardSize={props.cardSize}/>
                </TabPanel>
                <TabPanel value={promptsContext.activeTab} index={2}>
                    <PromptsBrowserTab cardSize={props.cardSize}/>
                </TabPanel>
                <Stack direction="row" justifyContent={'space-evenly'} alignItems={'center'} m={2}>
                    <TextField
                        InputProps={{
                            startAdornment: (<InputAdornment position="start"><Search sx={{color: 'white'}}/></InputAdornment>)
                        }}
                        sx={{'& fieldset': {
                            borderColor: '#9147ff'
                      }}}
                        fullWidth={!props.canCreate}
                        size='small'
                        label={'Search'} 
                        variant={'outlined'}
                        onKeyDown={(e) => {if (e.key == 'Enter') promptsContext.query?.refetch()}}
                        onChange={e => promptsContext.changeSearchString!(e.currentTarget.value)}>
                            {promptsContext.searchString}
                    </TextField>
                    {props.canCreate && 
                    <>
                    <Button variant="contained" onClick={(event) => setAnchorEl(event.currentTarget)}><LibraryAdd/>CREATE NEW PROMPT SET</Button>
                    <Popover open={anchorEl != null} anchorEl={anchorEl} PaperProps={{sx:{backgroundColor:'#2b2b40'}}}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'left',
                                }} 
                            onClose={() => setAnchorEl(null)}>
                        <CreatePromptSetForm/>
                    </Popover>
                    </>}
                </Stack>
            </Stack>
        </Paper>
    )
}

const CreatePromptSetForm = () =>{
    const promptsContext = useContext(PromptsContext);
    
    const [name, setName] = useState<string>("");
    const [tags, setTags] = useState<string[]>([]);
    const {isError, isLoading, isSuccess, mutate} = useMutation((promptSet: {name: string, tags: string[]}) => createPromptSet(promptSet.name, promptSet.tags), {onSuccess: () => promptsContext.query!.refetch()});

    return <Stack direction={'row'} m={1} spacing={2}>
        <TextField label="Name" variant="filled" onChange={(event) => setName(event.currentTarget.value)}>{name}</TextField>
        <TextField label="Tags" variant="filled" onChange={(event) => setTags(event.currentTarget.value.split(" "))}>{tags}</TextField>
        <Button variant="contained" onClick={() => {mutate({name: name, tags: tags})}}>{(isError && "ERROR") || (isLoading && "LOADING") || (isSuccess && "SUCCESSFUL!") || "SUBMIT"}
        </Button>
    </Stack>
}

const createPromptSet = async (name: string, tags: string[]) => {
    return axios.post(`https://localhost:7242/api/promptSets/addToCreated`,
    {name, tags},
    {
        headers: {
            Authorization: `Bearer ${localStorage.getItem(`authToken`)}`
        }});
}

export default PromptsBrowser;