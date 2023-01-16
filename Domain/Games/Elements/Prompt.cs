namespace Domain.Games.Elements
{
    public class Prompt : BaseEntity
    {
        public string PromptSetId { get; set; }
        public virtual PromptSet PromptSet { get; set; }
        public String Question { get; set; } = null!;
        public String CorrectAnswer { get; set; } = null!;
        public String[] WrongAnswers { get; set; } = null!;
    }
}
