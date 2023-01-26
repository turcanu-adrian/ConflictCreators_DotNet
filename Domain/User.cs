using Domain.Enums;
using Domain.Games.Elements;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser
    {
        public virtual ICollection<PromptSet> FavouritePromptSets { get; set; } = new List<PromptSet>();
        public string DisplayName { get; set; }
        public int AchievementPoints { get; set; } = 0;
        public TimeSpan FastestRun { get; set; }
        public Avatar currentAvatar { get; set; } = Avatar.LULE;
        public int GamesWon { get; set; } = 0;
        public ICollection<Badge> Badges { get; set; } = new List<Badge>();

        //*ENDLESS MODE
        //*TIERS AMOUNT = PROMPTS AMOUNT / 5 ?

        // *collection of unlocked avatars
        // collection of badges

    }
}
