using Domain.Games.Elements;

namespace Application.Abstract
{
    public interface IPromptRepository
    {
        Task Add(Prompt prompt);
        void Remove(Prompt prompt);
        Task<List<Prompt>> GetAll();
        Task Update(Prompt prompt);
        Task<Prompt> GetRandomBySet(string promptSetId);
        Task<List<Prompt>> GetBySetId(string setId);
        Task<Prompt> GetById(string Id);
    }
}
