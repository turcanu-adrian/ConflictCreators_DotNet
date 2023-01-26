import { CheckCircle, Login } from "@mui/icons-material";
import { Button, Modal, Box, Stack, TextField} from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useContext, useState } from "react";
import { AuthContext, LoginPayload } from "../providers/AuthenticationProvider";

const style = {
    position: 'absolute' as 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: '#2b2b40',
    boxShadow: 24,
    p: 4,
  };

const LoginModal = (props: {open: boolean}) => {
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const [loginPayload, setLoginPayload ] = useState<LoginPayload>({
        userName: "",
        password: "",
    });

    const { status, data, mutate } = useMutation((loginPayload: LoginPayload) => handleLogin(loginPayload));

    return (
    <div>
        <Button variant={'contained'} onClick={handleOpen}><Login/>{props.open && 'LOGIN'}</Button>
        <Modal
        open={open}
        onClose={handleClose}
        >
        <Box sx={style}>
            <Stack spacing={2}>
                <TextField variant={'filled'} label={'UserName'} onChange={event => setLoginPayload({...loginPayload, userName: event.currentTarget.value})}>{loginPayload.userName}</TextField>
                <TextField variant={'filled'} type={'password'} label={'Password'} onChange={event => setLoginPayload({...loginPayload, password: event.currentTarget.value})}>{loginPayload.password}</TextField>
                <Button onClick={() => mutate(loginPayload)} variant='contained'>
                    {(status == 'loading' && 'Signing in...')
                    ||
                    (status == 'success' && <CheckCircle/>)
                    ||
                    (status == 'error' && 'FAILED')
                    ||
                    'LOGIN'}
                </Button>
            </Stack>
        </Box>
        </Modal>
    </div>
    );
}

const handleLogin = async (loginPayload: LoginPayload) => {
    return axios.post("https://localhost:7242/api/users/login", loginPayload)
        .then((response: any) => {
            const token = response.data.token;
            const userName = response.data.username;
            const expires = response.data.expiration;
            
            localStorage.setItem("authToken", token);
            localStorage.setItem("userName", userName);
            localStorage.setItem("tokenExpires", expires);
            window.location.href = '/';
        })
}

export default LoginModal;