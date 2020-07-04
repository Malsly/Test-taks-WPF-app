using BL.Abstract.ResultWrappers;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Abstract
{
    public interface IEmployeeService : IGenericService<EmployeeDTO>
    {
        IDataResult<List<EmployeeDTO>> GetAll();
        IDataResult<EmployeeDTO> Get(int id);
        IResult Add(EmployeeDTO dto);
        IResult Update(EmployeeDTO dto);
        IResult Delete(int id);
    }
}
