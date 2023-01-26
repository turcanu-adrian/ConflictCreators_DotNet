import { Avatar, Button, Card, Stack } from "@mui/material";
import { AvatarEnum } from "../../types-and-interfaces/Enums";
import { Player } from "../../types-and-interfaces/GameState";

const PlayerCharacter = (props: {onClick: (() => void)|undefined; playerInfo: Player, showAnswer: Boolean}) => {
    const color = props.showAnswer ? "success" : (props.playerInfo.answer ? "warning":"primary");

    return <Card elevation={10} sx={{width: .5, height: .8, color: '#2b2b40'}}>
            <Stack width={1} alignItems="center">
                <Avatar variant={'square'} alt={props.playerInfo.nickname} src={`./images/Avatars/${props.playerInfo.avatar}.webp`}/>
                <div style={{color: 'purple'}}>{props.playerInfo.nickname}</div>
                <div>{props.playerInfo.badges}</div>
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