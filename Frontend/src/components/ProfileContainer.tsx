import { HowToReg, Login, Logout } from "@mui/icons-material";
import { Avatar, Button, Popover, Stack, TextField, useTheme } from "@mui/material";
import { useMutation, useQueries, useQuery } from "@tanstack/react-query";
import axios from "axios";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../providers/AuthenticationProvider";
import { AvatarEnum as AvatarEnum } from "../types-and-interfaces/Enums";
import LoginModal from "./LoginModal";
import RegisterModal from "./RegisterModal";

const ProfileContainer = (props: {open: boolean}) => {
    const authContext = useContext(AuthContext);

    return (<Stack alignItems={'center'} width={1} height={.3} position={'absolute'} bottom={0} spacing={2}>
                <AvatarPicker/>
                <div>{authContext.isAuth && localStorage.getItem('userName') || 'GUEST'}</div>
                {authContext.isAuth && <Button variant="contained" onClick={() => {localStorage.clear(); window.location.href='/'}}><Logout/>Logout</Button> 
                || 
                <>
                    <LoginModal open={props.open}/>
                    <RegisterModal open={props.open}/>
                </>}
            </Stack>
     )
}

const availableAvatars = Object.keys(AvatarEnum).filter(x => !(parseInt(x) >= 0));

const AvatarPicker = () => {
    const [currentAvatar, setCurrentAvatar] = useState(AvatarEnum.KEKW);
    const {status, data, mutate } = useMutation(setAvatar);
    const {status: queryStatus, data: queryData, refetch} = useQuery(['currentAvatar', status], () => getAvatar(), {onSuccess: (queryData: any) => setCurrentAvatar(AvatarEnum[queryData as keyof typeof AvatarEnum])});
    const [anchorEl, setAnchorEl] = useState<HTMLDivElement|null>(null);

    const authContext = useContext(AuthContext);

    const handleClick = (event: React.MouseEvent<HTMLDivElement>) => {
        setAnchorEl(event.currentTarget);
    }

    const handleClose = () => {
        setAnchorEl(null);
    }

    return (<>
        <Avatar onClick={authContext.isAuth ? handleClick : undefined} sx={{cursor: 'pointer'}} variant="square" src={`./images/Avatars/${currentAvatar}.webp`}/>
        <Popover PaperProps={{sx: PopoverStyle}} open={Boolean(anchorEl)} anchorEl={anchorEl} onClose={handleClose} anchorOrigin={{vertical: 'top', horizontal: 'left'}} transformOrigin={{vertical: 'bottom', horizontal: 'left'}}>
            <Stack direction={'row'} spacing={2} height={1} alignItems={'center'}>
                {availableAvatars.map(it => 
                <img key={it} src={`./images/Avatars/${it}.webp`} onClick={() => mutate(AvatarEnum[it as keyof typeof AvatarEnum])} height={'80%'}/>)}
            </Stack>
        </Popover>
    </>)
}

const setAvatar = async (avatar: AvatarEnum) => {
    return axios.post(`https://localhost:7242/api/users/setAvatar/${avatar}`,
        {},
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }})
} 

const getAvatar = async () => {
    const result = await axios.get(`https://localhost:7242/api/users/getAvatar`,
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }});
    return result.data;
}

const PopoverStyle = {
    color: 'black',
    height: .07,
    padding: 1
};

export default ProfileContainer;