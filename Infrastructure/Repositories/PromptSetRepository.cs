using Application.Abstract;
using Domain;
using Domain.Games.Elements;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PromptSetRepository : IPromptSetRepository
    {
        private readonly DataContext _context;
        public PromptSetRepository(DataContext context)
        {
            _context = context;
        }
       
        public async Task Add(PromptSet promptSet)
        {
            await _context.PromptSets.AddAsync(promptSet);
        }

        public async void Remove(PromptSet promptSet)
        {
            _context.PromptSets.Remove(promptSet);
        }

        public async Task<List<PromptSet>> GetAll()
        {
            return await _context.PromptSets.Include(ps => ps.Prompts).ToListAsync();
        }

        public async Task<List<PromptSet>> GetAllCreatedBy(string userId)
        {
            return await _context.PromptSets.Include(ps => ps.Prompts).Where(ps => ps.CreatedByUserId == userId).ToListAsync();
        }

        public async Task<PromptSet> GetById(string promptSetId)
        {
            PromptSet promptSet = await _context.PromptSets.FirstOrDefaultAsync(ps => ps.Id == promptSetId);
            return promptSet;
        }

        public async Task Update(PromptSet promptSet)
        {
            _context.PromptSets.Update(promptSet);
        }
    }
}
