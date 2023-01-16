namespace WebAPI.DTOs.Prompt
{
    public class PromptGetDto
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] WrongAnswers { get; set; }
    }
}
