
using TSmartClinic.Core.Domain.Interfaces.Repositories;
using TSmartClinic.Data.Contexts;

namespace TSmartClinic.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TSmartClinicContext? _TSmartClinicContext;

        public UnitOfWork(TSmartClinicContext? AgendaAppContext)
        {
            _TSmartClinicContext = AgendaAppContext;
        }

        public void BeginTransaction() => _TSmartClinicContext?.Database.BeginTransaction();
    
        public void Commit() => _TSmartClinicContext?.Database.CommitTransaction();
        
        public void Rollback() => _TSmartClinicContext?.Database.RollbackTransaction();

        public void Dispose() => _TSmartClinicContext?.Dispose();  
    }
}
