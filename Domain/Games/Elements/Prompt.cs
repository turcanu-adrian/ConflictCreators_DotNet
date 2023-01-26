namespace Domain.Games.Elements
{
    public class Prompt : BaseEntity
    {
        public string PromptSetId { get; set; }
        public string Question { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public string[] WrongAnswers { get; set; } = null!;
    }
}
