import { Prompt } from "../types-and-interfaces/GameState";
import Button from "@mui/material/Button";
import Grid2 from "@mui/material/Unstable_Grid2/Grid2";
import { Paper } from "@mui/material";

type PromptContainerProps = {
    playerAnswer: string;
    rightAnswer: string;
    prompt: Prompt;
    onClickHandler: (answer:string) => void;
}

const PromptContainer = (props: PromptContainerProps) => {
    return (
        <Paper elevation={3}>
            <Grid2 container spacing={2} direction="row" justifyContent="center" alignItems="center">
            <Grid2 xs={12} textAlign="center">
                CURRENT QUESTION = {props.prompt.currentQuestion}
            </Grid2>
            {props.prompt.currentQuestionAnswers.map(it => <Grid2 key={it} xs={5}><Button disabled={props.playerAnswer != "" && it != props.playerAnswer} sx={{height: '3em'}} key={it} fullWidth variant='contained' onClick={() => {props.onClickHandler(it)}} color={checkAnswer(it, props.playerAnswer, props.rightAnswer)}>{it}</Button></Grid2>)}
            </Grid2>
        </Paper>
    )
}

// "primary" | "success" | "error" | "warning"

const checkAnswer = (answer: string, playerAnswer: string, rightAnswer: string): "primary" | "success" | "error" | "warning" => {
    if (answer == "")
        return 'error';
    
    if (answer == playerAnswer)
        {
            if (rightAnswer != "")
                {
                    if (answer == rightAnswer)
                        return 'success';
                    return 'error';
                }
            return 'warning';
        }
    return 'primary'; 
}

export {PromptContainer}