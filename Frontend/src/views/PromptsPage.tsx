import { Stack, Button } from "@mui/material";
import { useQuery, UseQueryResult } from "@tanstack/react-query";
import axios from "axios";
import { createContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import {PromptManager as PromptsManager} from "../components/PromptsPage/PromptsManager/PromptsManager";
import PromptsBrowser from "../components/PromptsPage/PromptsBrowser";
import { PromptSetDto } from "../types-and-interfaces/Dtos";
import { ToastContainer } from "react-toastify";

const PromptsContext = createContext<{
    selectedSet: PromptSetDto|null, 
    changeSelectedSet: ((ps: PromptSetDto|null) => void)|undefined, 
    activeTab: number, 
    changeActiveTab: ((n: number) => void) | undefined,
    query: UseQueryResult | null,
    searchString: string,
    changeSearchString: ((newString: string) => void) | undefined
}>({
    selectedSet: null, 
    changeSelectedSet: undefined, 
    activeTab: 0, 
    changeActiveTab: undefined,
    query: null,
    searchString: "",
    changeSearchString: undefined
});

const route: { [key: number]: string} = {
    0: "getBrowse",
    1: "getFavourites",
    2: "getCreated"
};

const PromptsPage = () => {
    const [activeTab, setActiveTab] = useState<number>(0);
    const [selectedSet, setSelectedSet] = useState<PromptSetDto|null>(null);
    const [searchString, setSearchString] = useState<string>("");

    const query = useQuery([route[activeTab]], () => fetchTab(activeTab, searchString));
    const navigate = useNavigate();

    const changeSelectedSet = (ps: PromptSetDto|null) => {
        setSelectedSet(ps);
    }
    
    const changeActiveTab = (n: number) => {
        setActiveTab(n);
    }

    const changeSearchString = (newString: string) => {
        setSearchString(newString);
    }

    return (
    <Stack alignItems={'center'} m={'auto'} spacing={2} width={.7} height={.9}>
        <Button variant="contained" onClick={() => navigate('/')}>GO BACK</Button>
        <Stack direction={'row'} m={5} spacing={5} width={1} height={.7}>
            <PromptsContext.Provider value={{selectedSet, changeSelectedSet, activeTab, changeActiveTab, query, searchString, changeSearchString}}>
                <PromptsBrowser width={.5} height={1} canCreate={true} cardSize={4}/>
                <PromptsManager/>
            </PromptsContext.Provider>
        </Stack>
        <ToastContainer position={'bottom-right'}/>
    </Stack>
    )
}

const fetchTab = async (tab: number, searchString: string) => {
    const response = await axios.get<PromptSetDto[]>(`https://localhost:7242/api/promptSets/${route[tab]}/${searchString.toLowerCase()}`,
        {headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`
        }});
    
    return response.data;
}

export {PromptsContext, PromptsPage, fetchTab, route}