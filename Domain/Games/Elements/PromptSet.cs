using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Games.Elements
{
    public class PromptSet : BaseEntity
    {
        public virtual ICollection<string> Tags { get; set; }
        public virtual ICollection<Prompt> Prompts { get; set; }
        public string Name { get ; set; }
        public string CreatedByUserId { get; set; }
    }
}
