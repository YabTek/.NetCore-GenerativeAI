using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Application.Contracts.Persistence;
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ITaskRepository TaskRepository { get; }
        IChecklistRepository ChecklistRepository {get;}

       
        Task <int> Save();

    }

