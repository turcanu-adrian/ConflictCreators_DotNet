using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.Prompt
{
    public class PromptSetModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string[] Tags { get; set; }
    }
}
