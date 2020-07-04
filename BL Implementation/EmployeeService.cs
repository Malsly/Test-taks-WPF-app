
using BL.Abstract;
using BL.Abstract.ResultWrappers;
using BL.Impl.ResultWrappers;
using DAl.Impl.Mappers;
using DAL.Abstract;
using DAL_Abstract;
using DAL_Implementation;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Impl
{
    public class EmployeeService : IEmployeeService
    {

        readonly IMapper<Employee, EmployeeDTO, IGenericRepository<Employee>> Mapper;
        readonly IGenericRepository<Employee> Repo;
        readonly IUnitOfWork UoW;
        public EmployeeService()
        {
            UoW = new UnitOfWork();
            Repo = UoW.EmployeeRepository;
            Mapper = new EmployeeMapper(Repo);
        }

        public IDataResult<List<EmployeeDTO>> GetAll()
        {
            return new DataResult<List<EmployeeDTO>>()
            {
                Data = Repo.Get().Select(e => Mapper.Map(e)).ToList(),
                Message = ResponseMessageType.None,
                ResponseStatusType = ResponseStatusType.Successed
            };
        }

        public IDataResult<EmployeeDTO> Get(int id)
        {
            return new DataResult<EmployeeDTO>()
            {
                Data = Mapper.Map(Repo.GetByID(id)),
                Message = ResponseMessageType.None,
                ResponseStatusType = ResponseStatusType.Successed
            };
        }

        public IResult Add(EmployeeDTO dto)
        {
            Repo.Insert(Mapper.DeMap(dto));
            return new Result()
            {
                Message = ResponseMessageType.None,
                ResponseStatusType = ResponseStatusType.Successed
            };
        }

        public IResult Update(EmployeeDTO dto)
        {
            Repo.Update(Mapper.DeMap(dto));
            return new Result()
            {
                Message = ResponseMessageType.None,
                ResponseStatusType = ResponseStatusType.Successed
            };
        }

        public IResult Delete(int id)
        {
            Repo.Delete(id);
            return new Result()
            {
                Message = ResponseMessageType.None,
                ResponseStatusType = ResponseStatusType.Successed
            };
        }
        public void Save()
        {
            UoW.Save();
        }
    }
}
