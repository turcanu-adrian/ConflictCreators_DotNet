using Application.Abstract;
using Domain.Games.Elements;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.PromptSets.Take(100).ToListAsync();
        }
        public async Task Update(PromptSet promptSet)
        {
            _context.PromptSets.Update(promptSet);
        }
    }
}
