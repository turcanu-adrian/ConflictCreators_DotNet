//game description
//feedback form || mail

import { Button, Card, Paper, Stack, Typography } from "@mui/material";


const HowToPlayPage = () => {
    return <Stack alignItems={'center'} width={1} height={1}>
        <Paper elevation={3} sx={{width: .6, height: .7, backgroundColor: '#2b2b40'}}>
            <Stack width={1} height={1} justifyContent={'space-around'} alignItems={'center'}>
                <Card sx={{width: .7}}>
                    <Stack>
                        <Typography variant={'h5'} sx={{backgroundColor: 'purple', color: 'white'}}>As a host player</Typography>
                        <Typography sx={{textAlign: 'left', color: 'black'}}>
                            <ul >
                                <li>Click one of the <b>4</b> answers to answer the question!</li>
                                <ul>
                                    <li>Answer correctly to advance to the next tier!</li>
                                    <li>A wrong answer will result in the game ending and you keeping the points you've earned at the last safety tier achieved.</li>
                                </ul>
                                <li>Click one of the <b>3</b> cheat buttons to use a cheat!</li>
                                <ul style={{ lineHeight: '2em'}}>
                                    <li>Click the <Button variant={'contained'} size={'small'}>ASK A GUEST</Button> button to see one answer from the guest players!</li> 
                                    <li>Click the <Button variant={'contained'} size={'small'}>ASK THE AUDIENCE</Button> button to see the distribution of the audience players' answers!</li>
                                    <li>Click the <Button variant={'contained'} size={'small'}>SPLIT THE ANSWERS</Button> button to remove two of the wrong answers!</li>
                                </ul>
                                <li>Decide whether you continue the game or end with your current points once you reach a safety tier!</li>
                                <ul>
                                    <li>If you decide to continue playing, all your cheats become available again!</li>
                                </ul>
                            </ul>
                        </Typography>
                    </Stack>
                </Card>
                <Card sx={{width: .7}}>
                    <Stack>
                        <Typography variant={'h5'} sx={{backgroundColor: 'purple', color: 'white'}}>As a guest or audience player</Typography>
                        <Typography sx={{textAlign: 'left', color: 'black'}}>
                            <ul>
                                <li>Click one of the <b>4</b> answers to answer the question and help the host player!</li>
                            </ul>
                        </Typography>
                    </Stack>
                </Card>
            </Stack>
        </Paper>
    </Stack>
}

export default HowToPlayPage;