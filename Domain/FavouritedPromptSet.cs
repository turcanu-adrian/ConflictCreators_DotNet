using Domain.Games.Elements;

namespace Domain
{
    public class FavouritedPromptSet : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public string PromptSetId { get; set; }
        public PromptSet PromptSet { get; set; }
    }
}
