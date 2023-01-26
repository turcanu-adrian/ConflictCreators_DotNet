import { Button } from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";

const DeletePromptButton = (props: {promptId: string}) => {
    const {isError, isLoading, isSuccess, mutate} = useMutation(deletePrompt);

    return (<Button variant="contained" onClick={() => mutate(props.promptId)}>R</Button>)
}

const deletePrompt = async (promptId: string) => {
    return axios.post(`https://localhost:7242/api/prompts/remove/${promptId}`,
        {},
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }})
        .then((result: any) => console.log(result))
        .catch((err: any) => console.log(err))
}

export default DeletePromptButton;