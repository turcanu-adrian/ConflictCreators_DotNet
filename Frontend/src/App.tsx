import { ConnectionProvider } from './providers/ConnectionProvider';
import { GameProvider } from './providers/GameProvider';
import GamePage from './views/GamePage';
import {  createTheme, CssBaseline, ThemeProvider } from '@mui/material';
import { AuthenticationProvider } from './providers/AuthenticationProvider';
import { Route, Routes, useLocation } from 'react-router-dom';
import HomePage from './views/HomePage';
import {PromptsPage} from './views/PromptsPage';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import React from 'react';
import MiniDrawer from './components/MiniDrawer';
import Footer from './components/Footer';
import './App.css';
import CreateGamePage from './views/CreateGamePage';
import HowToPlayPage from './views/HowToPlayPage';
import LeaderboardPage from './views/LeaderboardPage';
import CornerImage from './components/CornerImage';
import { ToastContainer } from 'react-toastify';

const darkTheme = createTheme({
  palette: {
    background: {
      default: '#212126'
    },
    primary: {
      main: '#9147ff', //TRY #2b2b40
      light: '#c878ff',
      dark: '#590ecb'
    },
    secondary: {
      main: '#212126',
      light: '#48484e',
      dark: '#2b2b40'
    },
    text: {
      primary: '#FFFFFF',
      secondary: '#FFFFFF'
    },
    error: {
      main: '#ff5a79'
    }
  },
  typography: {
    fontFamily: 'acumin-pro',
    fontWeightRegular: '300'
  },
  components: {
    MuiCssBaseline: {
      styleOverrides: {
        body: {
          scrollbarColor: "#6b6b6b #2b2b2b",
          "&::-webkit-scrollbar, & *::-webkit-scrollbar": {
            backgroundColor: "transparent",
            width: '.5em'
          },
          "&::-webkit-scrollbar-thumb, & *::-webkit-scrollbar-thumb": {
            borderRadius: 8,
            backgroundColor: "purple",
          }
        }
      }
    }
  }
});

const logoStyle = {
  transition: 'width .5s'
}

const getLogoWidth = (path: string): string => {
  if (path == '/' || path == '/home')
    return '50%';
   
  if (path == '/play/create')
    return '0%';

  return '20%';
}

const queryClient = new QueryClient();

const App = () => {
  const location = useLocation();

  return (
    <AuthenticationProvider>
      <ConnectionProvider>
        <GameProvider>
            <ThemeProvider theme={darkTheme}>
            <CssBaseline/>
            <CornerImage/>
            <QueryClientProvider client={queryClient}>
              <React.StrictMode>
              <img src='/images/logo.png' 
                style={logoStyle} 
                width={getLogoWidth(location.pathname)}/>
              <Footer/>
              <MiniDrawer/>
                <Routes>
                  <Route path="/" element={<HomePage/>}/>
                  <Route path="/play" element={<GamePage/>}/>
                  <Route path="/play/create" element={<CreateGamePage/>}/>
                  <Route path="/prompts" element={<PromptsPage/>}/>
                  <Route path="/howtoplay" element={<HowToPlayPage/>}/>
                  <Route path="/leaderboard" element={<LeaderboardPage/>}/>
                  <Route path="/:gameId" element={<HomePage/>}/>
                </Routes>
              </React.StrictMode>
            </QueryClientProvider>
          </ThemeProvider>
        </GameProvider>
      </ConnectionProvider>
    </AuthenticationProvider>)
}


export default App;