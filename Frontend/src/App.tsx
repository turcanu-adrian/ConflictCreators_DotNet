import React from 'react';
import { GameState } from './types-and-interfaces/GameState';
import { ConnectionProvider } from './providers/ConnectionProvider';
import { GameProvider } from './providers/GameProvider';
import Game from './views/Game';

export const GameStateContext = React.createContext<GameState | null>(null);

const App = () => {
  return (
    <ConnectionProvider>
      <GameProvider>
        <Game/>
      </GameProvider>
    </ConnectionProvider>
  )
}




export default App;
