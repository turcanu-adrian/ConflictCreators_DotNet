import { AddBox } from "@mui/icons-material";
import { Button, Popover, Stack, TextField } from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useContext, useState } from "react";
import { Id } from "react-toastify";
import { CreatorPromptDto } from "../../../../types-and-interfaces/Dtos";
import { PromptsContext } from "../../../../views/PromptsPage"

interface AddPromptModel extends Omit<CreatorPromptDto, 'id'>{
    promptSetId: string;
}

const AddPromptButton = (props: {refetch: () => void}) => {
    const promptsContext = useContext(PromptsContext);

    const [anchorEl, setAnchorEl] = useState<HTMLButtonElement | null>(null);
    const [prompt, setPrompt] = useState<AddPromptModel>({
        promptSetId: promptsContext.selectedSet!.id,
        question: "",
        wrongAnswers: [],
        correctAnswer: ""
    });

    const {isError, isLoading, isSuccess, mutate} = useMutation(() => addPrompt(prompt), {onSuccess: () => props.refetch()});

    return (<Stack justifyItems={'center'} alignItems={'center'} height={.1}>
        <Button variant="contained" onClick={(event) => setAnchorEl(event.currentTarget)}><AddBox/>ADD NEW PROMPT</Button>
        <Popover open={anchorEl != null} anchorEl={anchorEl} PaperProps={{sx:{backgroundColor:'#2b2b40'}}} anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'left',
        }} onClose={() => setAnchorEl(null)}>
            <Stack direction={'row'} m={1} spacing={2} alignItems={'center'}>
                <TextField variant="filled" label="Question" onChange={(event) => setPrompt({...prompt, question: event.currentTarget.value})}>{prompt.question}</TextField>
                <TextField variant="filled" label="Correct Answer" onChange={(event) => setPrompt({...prompt, correctAnswer: event.currentTarget.value})}>{prompt.correctAnswer}</TextField>
                <TextField variant="filled" label="Wrong Answers" onChange={(event) => setPrompt({...prompt, wrongAnswers: event.currentTarget.value.split(",")})}>{prompt.wrongAnswers}</TextField>
                <div><Button size="small" variant='contained' onClick={() => mutate()}><AddBox/>ADD PROMPT</Button></div>
            </Stack>
        </Popover>
    </Stack>)
}

const addPrompt = async (prompt: AddPromptModel) =>
{
    return axios.post(`https://localhost:7242/api/prompts/add`,
        prompt,
        {
            headers: {
                Authorization: `Bearer ${localStorage.getItem(`authToken`)}`
            }});
}

export default AddPromptButton;