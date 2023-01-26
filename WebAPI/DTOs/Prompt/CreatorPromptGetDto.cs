namespace WebAPI.DTOs.Prompt
{
    public class CreatorPromptGetDto : PromptGetDtoBase
    {
        public string CorrectAnswer { get; set; }
        public string[] WrongAnswers { get; set; }
    }
}
