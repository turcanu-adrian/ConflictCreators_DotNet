using Application.Abstract;
using Domain.Games.Elements;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PromptRepository : IPromptRepository
    {
        private readonly DataContext _context;

        public PromptRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(Prompt prompt)
        {
            await _context.Prompts.AddAsync(prompt);
        }

        public async Task<List<Prompt>> GetAll()
        {
            return await _context.Prompts.ToListAsync();
        }

        public async Task<Prompt> GetRandomBySet(string promptSetId)
        {
            Random rnd = new Random();

            List<Prompt> prompts = await _context.Prompts.Where(p=> p.PromptSetId == promptSetId).ToListAsync();
            Prompt prompt = prompts[rnd.Next(prompts.Count)];
            return prompt;
        }

        public void Remove(Prompt prompt) 
        {
            _context.Prompts.Remove(prompt); ;
        }

        public async Task Update(Prompt prompt)
        {
            _context.Prompts.Update(prompt);
        }

        public async Task<List<Prompt>> GetBySetId(string setId)
        {
            return await _context.Prompts.Where(p => p.PromptSetId == setId).ToListAsync();
        }

        public async Task<Prompt> GetById(string id)
        {
            return await _context.Prompts.FirstAsync(p => p.Id == id);
        }
    }
}
