import { Cheat } from "./Enums";
import { GameState } from "./GameState";

export interface WWTBAMState extends GameState {
    tiers: number[];
    availableCheats: Cheat[];
    activeCheats: Cheat[];
    currentTier: number;
    audienceAnswers: {
        [key: string]: number;
    };
}
