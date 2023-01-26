namespace WebAPI.DTOs.Prompt
{
    public class PromptSetGetDto
    {
        public string CreatorName { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public int PromptsCount { get; set; }
    }
}
