import { styled } from "@mui/system"

const StyledCornerImage = styled('div')({
    position: 'fixed',
    right: '-10%',
    backgroundImage: 'url(./images/conflictcreatorscorner.png)',
    backgroundSize: '150%',
    width: '20%',
    height: '80%',
    borderBottomLeftRadius: '90%',
    borderTopLeftRadius: '0%'
})

const CornerImage = () => {
    return <StyledCornerImage>
    </StyledCornerImage>
}

export default CornerImage