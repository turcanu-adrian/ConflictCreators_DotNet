import { Avatar, Button, Card, Stack } from "@mui/material";
import { Player } from "../types-and-interfaces/GameState";

const PlayerCharacter = (props: {onClick: (() => void)|undefined; playerInfo: Player, showAnswer: Boolean}) => {
    const color = props.showAnswer ? "success" : (props.playerInfo.answer ? "warning":"primary");

    return <Card elevation={10} sx={{width: .2, height: 1}}>
            <Stack spacing={2}>
                <Avatar sx={{margin: 'auto'}} alt={props.playerInfo.nickname} src={props.playerInfo.avatar}/>
                <div>{props.playerInfo.nickname}</div>
                {(props.showAnswer && <div>{props.playerInfo.answer}</div>)
                ||
                ((props.onClick && props.playerInfo.answer)
                && 
                (props.showAnswer && <div>{props.playerInfo.answer}</div>
                    ||
                    <Button variant="contained" onClick={props.onClick}>SHOW ANSWER</Button>))
                }
            </Stack>
        </Card>
}

export default PlayerCharacter;