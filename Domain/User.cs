using Domain.Games.Elements;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser
    {
        public virtual ICollection<PromptSet> PromptSets { get; set; }
    }
}
