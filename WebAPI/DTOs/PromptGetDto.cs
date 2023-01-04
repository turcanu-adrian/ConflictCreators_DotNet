namespace WebAPI.DTOs
{
    public class PromptGetDto
    {
        public int Id { get; set; }
        public String User { get; set; }
        public String Question { get; set; }
        public String CorrectAnswer { get; set; }
        public String[] WrongAnswers { get; set; }
    }
}
