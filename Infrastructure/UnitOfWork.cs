using Application.Abstract;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext dataContext, IPromptRepository promptRepository)
        {
            _dataContext = dataContext;
            PromptRepository = promptRepository;
        }

        public IPromptRepository PromptRepository { get; private set; }

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
