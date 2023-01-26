import { Star, StarBorder } from "@mui/icons-material";
import { Box, LinearProgress, linearProgressClasses, Stack } from "@mui/material"
import { WWTBAMState } from "../../types-and-interfaces/WWTBAMState";

const BarStyle = {
    height: '20%',
    width: '100%',
    [`&.${linearProgressClasses.colorPrimary}`]: {
        backgroundColor: 'gray',
        borderRadius: 5
      },
    [`& .${linearProgressClasses.bar}`]: {
        borderRadius: 5,
    }
}

const TiersBar = (props: {gameState: WWTBAMState}) => {

    return <Box sx={{height: .1, width: .7}}>
        <Stack direction={'row'} width={1} height={1} alignItems={'center'}>
            <LinearProgress sx={BarStyle} color={'primary'} variant="determinate" value={normalise(props.gameState.currentTier)}/>
        </Stack>
        <Stack position={'relative'} top={'-100%'} left={'24%'} direction={'row'} width={.78} height={1} alignItems={'center'} justifyContent={'space-between'}>
            <Star fontSize="large" sx={{color: props.gameState.currentTier >= 4 ? 'purple' : '#212126'}}/>
            <Star fontSize="large" sx={{color: props.gameState.currentTier >= 9 ? 'purple' : '#212126'}}/>
            <Star fontSize="large" sx={{color: props.gameState.currentTier >= 14 ? 'purple' : '#212126'}}/>
        </Stack>
    </Box>
}

const normalise = (value: number): number => (value* 100) / 14;

export default TiersBar;