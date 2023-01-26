import { Stack } from "@mui/material";

const footerStyle = {
    position: 'absolute',
    bottom: 0,
    height: .07,
    width: 1,
    backgroundColor: '#2b2b40',
    justifyContent: 'flex-end',
    alignItems: 'center',
    alignContent: 'center',
    gap: 5,
    paddingRight: 5
}

const Footer = () => {
    return (
    <Stack sx={footerStyle} direction={'row'}>
        <div><i>Conflict Creators<sup>©</sup></i></div>
        <img src={'./images/muilogo.png'} height={'30%'}/>
        <img src={'./images/tslogo.png'} height={'30%'}/>
        <img src={'./images/reactlogo.png'} height={'30%'}/>
        <img src={'./images/favicon.ico'} height={'80%'}/>
    </Stack>
    )
}

export default Footer;