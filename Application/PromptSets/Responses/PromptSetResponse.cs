using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PromptSets.Responses
{
    public class PromptSetResponse
    {
        public string CreatorName { get; set; }
        public string PromptSetId { get; set; }
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public bool isFavourite { get; set; }
    }
}
