using Log.Contracts;
using Repo.Contracts;
using System;
using System.Data.Entity;

namespace Repo.EF
{
    public class UnitOfWork : Disposable, IUnitOfWork
    {
        private ICustomLog _logger;
        private DbContext _dbContext;
        public DbContext DataContext => _dbContext ?? (_dbContext = new DataContext());

        private UnitOfWork(ICustomLog logger)
        {
            _logger = logger;
        }

        public static UnitOfWork CreateUnitOfWorkWithCustomLog(ICustomLog logger)
        {
            return new UnitOfWork(logger);
        }

        public void Commit()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The was a problem updating the db.");
            }
        }
    }
}
