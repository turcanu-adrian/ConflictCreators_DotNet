using System.ComponentModel.DataAnnotations;

namespace SignalRTest.DTOs
{
    public class PromptPutPostDto
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Question { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string CorrectAnswer { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public String[] WrongAnswers { get; set; }
    }
}
