using Domain;
using Domain.Games.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IPromptSetRepository
    {
        Task Add(PromptSet promptSet);
        void Remove(PromptSet promptSet);
        Task<List<PromptSet>> GetAll();
        Task Update(PromptSet promptSet);
        Task<PromptSet> GetById(string promptSetId);
        Task<List<PromptSet>> GetAllCreatedBy(string userId);
    }
}
