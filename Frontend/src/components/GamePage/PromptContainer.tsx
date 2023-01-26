import { Prompt } from "../../types-and-interfaces/GameState";
import Button from "@mui/material/Button";
import Grid2 from "@mui/material/Unstable_Grid2/Grid2";
import { Paper, Stack } from "@mui/material";

type PromptContainerProps = {
    playerAnswer: string;
    rightAnswer: string;
    prompt: Prompt;
    onClickHandler: (answer:string) => void;
}

const PromptContainer = (props: PromptContainerProps) => {
    return (
        <Stack spacing={2}>
            <Paper elevation={3} sx={{backgroundColor: '#212126'}}>
                    <Stack spacing={1} margin={1}>
                        <div>CURRENT QUESTION</div>
                        <div style={{fontSize: '1.3em'}}>{props.prompt.currentQuestion}</div>
                    </Stack>
            </Paper>
            <Grid2 container spacing={.5} direction="row" justifyContent="center" alignItems="center" margin={'auto'} width={1}>
                {props.prompt.currentQuestionAnswers.map(it => <Grid2  xs={5}><Button disabled={(props.playerAnswer != "" && it != props.playerAnswer) || it == ""} sx={{height: '2em', fontSize: '1.3em'}} key={it} fullWidth variant='contained' onClick={() => {props.onClickHandler(it)}} color={checkAnswer(it, props.playerAnswer, props.rightAnswer)}>{it}</Button></Grid2>)}
            </Grid2>
        </Stack>
    )
}

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