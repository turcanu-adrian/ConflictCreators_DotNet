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
            return await _context.Prompts
                .Take(100)
                .ToListAsync();
        }

        public async Task<Prompt> GetRandomByUsers(List<string> users)
        {
            Random rnd = new Random();
            List<Prompt> promptList =  await _context.Prompts.Where(it => users.Contains(it.User)).ToListAsync();
            Prompt prompt = promptList[rnd.Next(promptList.Count)];
            return prompt;
        }

        public void Remove(Prompt prompt) 
        {
            _context.Prompts.Remove(prompt); ;
        }

        public async Task Update(Prompt prompt)
        {
            _context.Update(prompt);
        }
    }
}
