using Webapi.Models;
using Webapi.Repositories.Interfaces;

namespace Webapi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Employee> Employees { get; private set; }
        public IGenericRepository<Department> Departments { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Employees = new GenericRepository<Employee>(_context);
            Departments = new GenericRepository<Department>(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
