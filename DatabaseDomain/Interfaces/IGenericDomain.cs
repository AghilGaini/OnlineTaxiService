using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Interfaces
{
    public interface IGenericDomain<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(long id);
        Task<bool> RemoveById(long id);
    }
}
