using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        public IPromptRepository PromptRepository { get; }
        public IPromptSetRepository PromptSetRepository { get; }
        Task Save();
    }
}
