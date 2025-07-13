using AgendaApp.Core.Domain.Interfaces.Repositories;
using AgendaApp.Data.Contexts;

namespace AgendaApp.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AgendaAppContext? _agendaAppContext;

        public UnitOfWork(AgendaAppContext? AgendaAppContext)
        {
            _agendaAppContext = AgendaAppContext;
        }

        public void BeginTransaction() => _agendaAppContext?.Database.BeginTransaction();
    
        public void Commit() => _agendaAppContext?.Database.CommitTransaction();
        
        public void Rollback() => _agendaAppContext?.Database.RollbackTransaction();

        public void Dispose() => _agendaAppContext?.Dispose();  
    }
}
