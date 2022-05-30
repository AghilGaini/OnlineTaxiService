using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDomain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRoleDomain _role { get; set; }
        public IUserDomain _user { get; set; }
        public IPermisionDomain _permision { get; set; }
        void Commit();

    }
}
