import { AvatarEnum } from "./Enums"

export interface PromptSetDto {
    creatorName: string,
    id: string,
    name: string,
    tags: string[],
    promptsCount: number
}

interface PromptDtoBase {
    id: string,
    question: string
}

export interface PromptDto extends PromptDtoBase {
    answers: string[]
}

export interface CreatorPromptDto extends PromptDtoBase {
    correctAnswer: string,
    wrongAnswers: string[]
}

export interface LeaderboardEntryDto {
    avatar: AvatarEnum;
    name: string;
    achievementPoints: number;
    fastestGame: any;
}