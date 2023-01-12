using Domain.Games.Elements;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class User : IdentityUser
    {
        public List<Prompt> Prompts { get; set; }
    }
}
