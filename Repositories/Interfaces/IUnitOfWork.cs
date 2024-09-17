using Webapi.Models;

namespace Webapi.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Employee> Employees { get; }
        IGenericRepository<Department> Departments { get; }
        Task<int> SaveAsync();
    }

}
