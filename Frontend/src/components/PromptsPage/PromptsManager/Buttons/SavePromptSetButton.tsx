import { Save } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useContext } from "react";
import { PromptSetDto } from "../../../../types-and-interfaces/Dtos";
import { PromptsContext } from "../../../../views/PromptsPage";

const SavePromptSetButton = (props: {promptSet: PromptSetDto}) => {
    const promptsContext = useContext(PromptsContext);

    const {isError, isLoading, isSuccess, mutate} = useMutation(savePromptSet, {onSuccess : () => {
        promptsContext.query?.refetch();
    }})

    return <div><Button size="small" variant="contained" onClick={() => mutate(props.promptSet)}><Save/>SAVE PROMPT SET</Button></div>
}

const savePromptSet = async (promptSet: PromptSetDto) => {
    return axios.post(`https://localhost:7242/api/promptSets/updateCreated`,
        promptSet,
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }});
}

export default SavePromptSetButton;