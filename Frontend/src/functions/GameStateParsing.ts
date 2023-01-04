import { GameState } from "../types-and-interfaces/GameState";
import { Player } from "../types-and-interfaces/Player";

const parseGameState = (stringifiedGameState: string) : GameState => {
    const jsonGameState = JSON.parse(stringifiedGameState);

    const parsedGameState: GameState = {
      hostPlayer: parsePlayer(jsonGameState.hostPlayer),
      gamemode: jsonGameState.gamemode,
      guestPlayers: jsonGameState.guestPlayers.map((it: any) => parsePlayer(it)),
      currentPhase: jsonGameState.currentPhase,
      id: jsonGameState.Id,
      audienceCount: jsonGameState.AudienceCount,
      prompt: {
        currentQuestion: jsonGameState.CurrentQuestion,
        currentQuestionAnswers: jsonGameState.CurrentQuestionAnswers
      }
    }
  
    return parsedGameState;
}

const parsePlayer = (playerJson: any) : Player => {
    return {
        Nickname: playerJson.Nickname,
        Avatar: playerJson.Avatar,
        Points: playerJson.Points,
        Answer: playerJson.Answer
      }
}

export default parseGameState;