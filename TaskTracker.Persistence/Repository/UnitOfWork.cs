using TaskTracker.Application.Contracts.Persistence;

namespace TaskTracker.Persistence.Repository;
  public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskTrackerDbContext _context;
        private ITaskRepository _taskRepository;
        private IChecklistRepository _checklistRepository;        
        private IUserRepository _userRepository;
    
        public UnitOfWork(TaskTrackerDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository { 
            get 
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            } 
         }
          public ITaskRepository TaskRepository { 
            get 
            {
                if (_taskRepository == null)
                    _taskRepository = new TaskRepository(_context);
                return _taskRepository; 
            } 
         }

        public IChecklistRepository ChecklistRepository
        {
            get
            {
                if (_checklistRepository == null)
                    _checklistRepository = new ChecklistRepository(_context);
                return _checklistRepository;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }

