using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Data.Contexts;

namespace AgendaApp.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext? _dataContext;

        public UnitOfWork(DataContext? dataContext)
        {
            _dataContext = dataContext;
        }

        public void BeginTransaction()
        {
           _dataContext?.Database.BeginTransaction();
        }

        public void Commit()
        {
            _dataContext?.Database.CommitTransaction();
        }

        public void Dispose()
        {
            _dataContext?.Dispose();
        }

        public void Rollback()
        {
            _dataContext?.Database.RollbackTransaction();
        }
       // public void Dispose() => _dataContext?.Database.Dispose();
    }
}
