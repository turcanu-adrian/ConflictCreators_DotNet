import { Player } from "./Player";
import { Prompt } from "./Prompt";

export interface GameState {
    hostPlayer: Player;
    gamemode: string;
    guestPlayers: Player[];
    currentPhase: string;
    id: string;
    audienceCount: number;
    prompt: Prompt;
}


