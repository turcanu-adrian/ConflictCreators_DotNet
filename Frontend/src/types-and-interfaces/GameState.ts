import { GamePhase, GameType } from "./Enums";

export interface GameState {
    gameType: GameType;
    hostPlayer: Player;
    guestPlayers: Player[];
    currentPhase: GamePhase;
    id: string;
    audienceCount: number;
    prompt: Prompt;
}

export interface Prompt {
    currentQuestion: string;
    currentQuestionAnswers: string[];
}

export interface Player {
    id: string;
    nickname: string;
    avatar: string;
    points: number;
    answer: string;
};