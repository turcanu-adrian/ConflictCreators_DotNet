using Domain.Enums;

namespace WebAPI.DTOs.User
{
    public class LeaderboardEntryDto
    {
        public Avatar Avatar { get; set; }
        public string Name { get; set; }
        public int AchievementPoints { get; set; }
        public TimeSpan FastestGame { get; set; }
    }
}
