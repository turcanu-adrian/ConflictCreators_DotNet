namespace Domain.Games.Elements
{
    public class Prompt
    {
        public int Id { get; init; }
        public String User { get; set; } = "default";
        public String Question { get; set; } = null!;
        public String CorrectAnswer { get; set; } = null!;
        public String[] WrongAnswers { get; set; } = null!;
    }
}
