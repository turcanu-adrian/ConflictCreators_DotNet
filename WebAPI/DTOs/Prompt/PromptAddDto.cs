using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Prompt
{
    public class PromptAddDto
    {
        [Required]
        public string Question { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }

        [Required]
        public string[] WrongAnswers { get; set; }
    }
}
