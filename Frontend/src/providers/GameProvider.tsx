import { createContext, useContext, useState, useEffect } from "react";
import parseGameState from "../functions/GameStateParsing";
import { PlayerType } from "../types-and-interfaces/Enums";
import { GameState } from "../types-and-interfaces/GameState";
import { ConnectionContext } from "./ConnectionProvider";

const GameContext = createContext<{gameState: GameState|null; joinedAs: PlayerType|null; endGame: (() => void)|undefined}>({gameState: null, joinedAs: null, endGame: undefined });

type GameProviderProps = {
  children: React.ReactNode;
}

const GameProvider = (props: GameProviderProps) => {
    const [ gameState, setGameState ] = useState<GameState|null>(null);
    const [ joinedAs, setJoinedAs ] = useState<PlayerType|null>(null);
    const connection = useContext(ConnectionContext);

    const endGame = () => {
      setGameState(null);
    }

    useEffect(() => {
        if (connection) {
              connection.on("ReceiveGameState", (stringifiedGameState) => {
                setGameState(parseGameState(stringifiedGameState));
              });
    
              connection.on("JoinedGameAs", (joinedGameAs) => {
                setJoinedAs(PlayerType[JSON.parse(joinedGameAs) as keyof typeof PlayerType]);
              });
        }
      }, [connection]);

    return (<GameContext.Provider value={{ gameState, joinedAs, endGame }}>{props.children}</GameContext.Provider>)

}

export {GameProvider, GameContext}