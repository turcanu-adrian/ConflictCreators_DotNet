import { Badge, Cheat, GamePhase, GameType } from "../types-and-interfaces/Enums";
import { GameState, Player } from "../types-and-interfaces/GameState";
import { WWTBAMState } from "../types-and-interfaces/WWTBAMState";

const parseGameState = (stringifiedGameState: string): GameState|WWTBAMState => {
    const jsonGameState = JSON.parse(stringifiedGameState);

    const parsedGameState: GameState = {
      hostPlayer: parsePlayer(jsonGameState.HostPlayer),
      gameType: GameType[jsonGameState.Type as keyof typeof GameType],
      guestPlayers: jsonGameState.GuestPlayers?.map((it: any) => parsePlayer(it)),
      currentPhase: GamePhase[jsonGameState.CurrentPhase as keyof typeof GamePhase],
      id: jsonGameState.Id,
      audienceCount: jsonGameState.AudienceCount,
      prompt: {
        currentQuestion: jsonGameState.CurrentQuestion,
        currentQuestionAnswers: jsonGameState.CurrentQuestionAnswers
      },
      maxGuestPlayers: jsonGameState.MaxGuestPlayers
    }

    if (parsedGameState.gameType == GameType.WWTBAM){
      const wwtbamGame: WWTBAMState = {
        ...parsedGameState,
        tiers: jsonGameState.Tiers,
        availableCheats: jsonGameState.AvailableCheats?.map((it: any) => Cheat[it as keyof typeof Cheat]),
        activeCheats: jsonGameState.ActiveCheats?.map((it: any) => Cheat[it as keyof typeof Cheat]),
        currentTier: jsonGameState.CurrentTier,
        audienceAnswers: jsonGameState.AudienceAnswers
      }

      return wwtbamGame;
    }
  
    return parsedGameState;
}

const parsePlayer = (playerJson: any): Player => {
    return {
        id: playerJson.Id,
        nickname: playerJson.Nickname,
        avatar: playerJson.Avatar,
        points: playerJson.Points,
        answer: playerJson.Answer,
        badges: playerJson.Badges?.map((it: any) => Badge[it as keyof typeof Badge])
      }
}

export default parseGameState;