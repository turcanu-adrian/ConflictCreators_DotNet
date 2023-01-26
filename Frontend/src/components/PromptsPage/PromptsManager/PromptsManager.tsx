import { Stack, Tabs, Tab, Button, Paper } from "@mui/material";
import axios from "axios";
import { useContext } from "react";
import { CreatorPromptDto, PromptDto } from "../../../types-and-interfaces/Dtos";
import { PromptsContext } from "../../../views/PromptsPage";
import DeletePromptSetButton from "./Buttons/DeletePromptSetButton";
import { FavouritePromptSetButton, UnfavouritePromptSetButton } from "./Buttons/FavouritePromptSetButton";
import SavePromptSetButton from "./Buttons/SavePromptSetButton";
import PromptsSetEditor from "./PromptsSetEditor";
import PromptSetViewer from "./PromptsSetViewer";

const PromptManager = () => {
    const promptsContext = useContext(PromptsContext);

    return (<Paper elevation={3} sx={{width: .5, height: 1, backgroundColor: '#2b2b40'}}>
        <Stack width={1} height={1}>
            <Stack direction="row" m={1} sx={{borderBottom: 1, borderColor: '#9147ff', alignItems: 'center'}} spacing={3}>
                <Tabs value={promptsContext.selectedSet != null ? 0 : false}>
                    <Tab label="Selected Prompt Set"/>
                </Tabs>
                {promptsContext.selectedSet && (
                    (promptsContext.activeTab == 0 && <FavouritePromptSetButton/>)
                    ||
                    (promptsContext.activeTab == 1 && <UnfavouritePromptSetButton/>)
                    ||
                    (<><SavePromptSetButton promptSet={promptsContext.selectedSet}/><DeletePromptSetButton/></>)
                )}
            </Stack>
            {(promptsContext.selectedSet!= null && (promptsContext.activeTab == 2 ? <PromptsSetEditor key={promptsContext.selectedSet.id}/> : <PromptSetViewer key={promptsContext.selectedSet.id}/>))}
        </Stack>
    </Paper>
    )
}

const fetchPromptsBySetId = async (id: string) => {
    const response = await axios.get<PromptDto[]|CreatorPromptDto[]>(`https://localhost:7242/api/prompts/getBySet/${id}`,
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }});
    
    return response.data;
}

export {PromptManager, fetchPromptsBySetId}