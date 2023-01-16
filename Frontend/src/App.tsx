import React from 'react';
import { GameState } from './types-and-interfaces/GameState';
import { ConnectionProvider } from './providers/ConnectionProvider';
import { GameProvider } from './providers/GameProvider';
import GameContainer from './views/GameContainer';
import { createTheme, CssBaseline, ThemeProvider } from '@mui/material';
import { AuthenticationProvider } from './providers/AuthenticationProvider';

export const GameStateContext = React.createContext<GameState | null>(null);

const darkTheme = createTheme({
  palette: {
    mode: 'dark'
  }
});

const App = () => {
  return (
    <AuthenticationProvider>
      <ConnectionProvider>
        <GameProvider>
          <ThemeProvider theme={darkTheme}>
            <CssBaseline />
            <GameContainer/>
          </ThemeProvider>
        </GameProvider>
      </ConnectionProvider>
    </AuthenticationProvider>)
}

export default App;