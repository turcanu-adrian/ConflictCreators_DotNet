import { createContext, useContext, useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import parseGameState from "../functions/GameStateParsing";
import { PlayerType } from "../types-and-interfaces/Enums";
import { GameState } from "../types-and-interfaces/GameState";
import { ConnectionContext } from "./ConnectionProvider";

const GameContext = createContext<{gameState: GameState|null; joinedAs: PlayerType|null; leaveGame: (() => void)|undefined}>({gameState: null, joinedAs: null, leaveGame: undefined });

type GameProviderProps = {
  children: React.ReactNode;
}

const GameProvider = (props: GameProviderProps) => {
    const [ gameState, setGameState ] = useState<GameState|null>(null);
    const [ joinedAs, setJoinedAs ] = useState<PlayerType|null>(null);
    const connectionContext = useContext(ConnectionContext);
    const navigate = useNavigate();
    const location = useLocation();

    const leaveGame = async () => {
      await connectionContext.connection!.invoke("LeaveGame");
      setGameState(null);
      setJoinedAs(null);
    }

    useEffect(() => {
        if (connectionContext.connection) {
              connectionContext.connection.on("ReceiveGameState", (stringifiedGameState) => {
                setGameState(parseGameState(stringifiedGameState));
              });
    
              connectionContext.connection.on("JoinedGameAs", (joinedGameAs) => {
                console.log("JOINED GAME AS INVOKED")
                setJoinedAs(PlayerType[JSON.parse(joinedGameAs) as keyof typeof PlayerType]);
              });
        }
      }, [connectionContext.connection]);

      useEffect(() => {
        if (location.pathname != "/play" && joinedAs != null){
          navigate("/play");
        }
      }, [joinedAs, location.pathname])

    return (<GameContext.Provider value={{ gameState, joinedAs, leaveGame }}>{props.children}</GameContext.Provider>)

}

export {GameProvider, GameContext}