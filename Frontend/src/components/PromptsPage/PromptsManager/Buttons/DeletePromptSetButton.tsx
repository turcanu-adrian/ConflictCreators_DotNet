import { Delete } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useContext } from "react";
import { PromptsContext } from "../../../../views/PromptsPage";

const DeletePromptSetButton = () => {
    const promptsContext = useContext(PromptsContext);

    const {isError, isLoading, isSuccess, mutate} = useMutation(deletePromptSet, {onSuccess : () => {
        promptsContext.changeSelectedSet!(null);
        promptsContext.query?.refetch();
    }})

    return <div><Button size="small" variant="contained" onClick={() => mutate(promptsContext.selectedSet!.id)}><Delete/>DELETE PROMPT SET</Button></div>
}

const deletePromptSet = async (id: string) => {
    return axios.post(`https://localhost:7242/api/promptSets/removeFromCreated/${id}`,
        {},
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }})
}

export default DeletePromptSetButton;