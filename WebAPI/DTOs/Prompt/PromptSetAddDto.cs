using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Prompt
{
    public class PromptSetAddDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<string> Tags { get; set; }
    }
}
