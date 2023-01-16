using Application.Abstract;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext dataContext, IPromptRepository promptRepository, IPromptSetRepository promptSetRepository)
        {
            _dataContext = dataContext;
            PromptRepository = promptRepository;
            PromptSetRepository = promptSetRepository;
        }

        public IPromptRepository PromptRepository { get; private set; }
        public IPromptSetRepository PromptSetRepository { get; private set; }

        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
