using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Abstract
{
    public interface IUnitOfWork
    {
        IGenericRepository<Employee> EmployeeRepository { get; }
        void Save();
        void Dispose();
    }
}
