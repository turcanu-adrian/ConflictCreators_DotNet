import { CheckCircle, Login } from "@mui/icons-material";
import { Button, Modal, Box, Stack, TextField} from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useContext, useState } from "react";
import { toast } from "react-toastify";
import { AuthContext, LoginPayload, RegisterPayload } from "../providers/AuthenticationProvider";

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

const RegisterModal = (props: {open: boolean}) => {
    const [open, setOpen] = useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    const [registerPayload, setRegisterPayload ] = useState<RegisterPayload>({
        userName: "",
        password: "",
        email: "",
        displayName: ""
    });

    const { status, data, mutate } = useMutation((registerPayload: RegisterPayload) => handleRegister(registerPayload), {onSuccess: () => {handleClose(); toast('You can now login')}});

    return (
    <div>
        <Button variant={'contained'} onClick={handleOpen}><Login/>{props.open && 'REGISTER'}</Button>
        <Modal
        open={open}
        onClose={handleClose}
        >
        <Box sx={style}>
            <Stack spacing={2}>
                <TextField variant={'filled'} label={'UserName'} onChange={event => setRegisterPayload({...registerPayload, userName: event.currentTarget.value})}>{registerPayload.userName}</TextField>
                <TextField variant={'filled'} type={'password'} label={'Password'} onChange={event => setRegisterPayload({...registerPayload, password: event.currentTarget.value})}>{registerPayload.password}</TextField>
                <TextField variant={'filled'} label={'Email'} onChange={event => setRegisterPayload({...registerPayload, email: event.currentTarget.value})}>{registerPayload.email}</TextField>
                <TextField variant={'filled'} label={'Display Name'} onChange={event => setRegisterPayload({...registerPayload, displayName: event.currentTarget.value})}>{registerPayload.displayName}</TextField>
                <Button onClick={() => mutate(registerPayload)} variant='contained'>
                    {(status == 'loading' && 'Registering...')
                    ||
                    (status == 'success' && <CheckCircle/>)
                    ||
                    (status == 'error' && 'FAILED')
                    ||
                    'REGISTER'}
                </Button>
            </Stack>
        </Box>
        </Modal>
    </div>
    );
}

const handleRegister = async (registerPayload: RegisterPayload) => {
    return axios.post("https://localhost:7242/api/users/register", registerPayload)
        .then((response: any) => {
            console.log(response);
        })
}

export default RegisterModal;