import { ButtonProps } from "../types-and-interfaces/ButtonProps"
import './button.css';

const Button = (props: ButtonProps) => {
    return (<button className={props.className} onClick={props.onClick}>{props.text}</button>)
}

export default Button;