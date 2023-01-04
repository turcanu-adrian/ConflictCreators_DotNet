import { createContext, useContext, useState, useEffect } from "react";
import parseGameState from "../functions/GameStateParsing";
import { GameState } from "../types-and-interfaces/GameState";
import Game from "../views/Game";
import Menu from "../views/Menu";
import { ConnectionContext } from "./ConnectionProvider";

const GameContext = createContext<{gameState: GameState|null; joinedAs: string|null}>({gameState: null, joinedAs: null});

type GameProviderProps = {
  children: React.ReactNode;
}

const GameProvider = (props: GameProviderProps) => {
    const [ gameState, setGameState ] = useState<GameState | null>(null);
    const [ joinedAs, setJoinedAs ] = useState<string | null>(null);
    const connection = useContext(ConnectionContext);

    useEffect(() => {
        if (connection) {
          connection.start()
            .then(result => {
              console.log('Connected');
    
              connection.on("ReceiveGameState", (stringifiedGameState) => {
                setGameState(parseGameState(stringifiedGameState));
              });
    
              connection.on("JoinedGameAs", (joinedGameAs) => {
                setJoinedAs(joinedGameAs);
              });
            })
            .catch(e => console.log('Connection failed: ', e));
        }
      }, [connection]);

    if (gameState)
      return (<GameContext.Provider value={{ gameState, joinedAs }}>{props.children}</GameContext.Provider>)

    return <Menu/>
    
}

export {GameProvider, GameContext}