import { BookmarkAdd, BookmarkRemove } from "@mui/icons-material";
import { Button } from "@mui/material";
import { useMutation } from "@tanstack/react-query";
import axios from "axios";
import { useContext } from "react";
import { PromptsContext } from "../../../../views/PromptsPage";

const FavouritePromptSetButton = () => {
    const promptsContext = useContext(PromptsContext);

    const {isError, isLoading, isSuccess, mutate} = useMutation(favouritePromptSet, {onSuccess: () => { promptsContext.query?.refetch(); promptsContext.changeSelectedSet!(null); }});

    return <div><Button size="small" variant="contained" onClick={() => mutate(promptsContext.selectedSet!.id)}><BookmarkAdd/>ADD TO FAVOURITES</Button></div>
}

const UnfavouritePromptSetButton = () => {
    const promptsContext = useContext(PromptsContext)

    const {isError, isLoading, isSuccess, mutate} = useMutation(unfavouritePromptSet, {onSuccess: () => { promptsContext.query?.refetch(); promptsContext.changeSelectedSet!(null); }});

    return <div><Button size="small" variant="contained" onClick={() => mutate(promptsContext.selectedSet!.id)}><BookmarkRemove/>REMOVE FROM FAVOURITES</Button></div>
}

const favouritePromptSet = async (id: string) => {
    return axios.post(`https://localhost:7242/api/promptSets/addToFavourites/${id}`,
        {},
        {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('authToken')}`
            }
        })
}

const unfavouritePromptSet = async (id: string) => {
    return axios.post(`https://localhost:7242/api/promptSets/removeFromFavourites/${id}`,
        {},
        {
            headers: {
                Authorization: `Bearer ${localStorage.getItem('authToken')}`
            }
        })
}

export {FavouritePromptSetButton, UnfavouritePromptSetButton}