using DAL.Abstract;
using DAL_Abstract;
using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAl.Impl.Mappers
{
    public class EmployeeMapper : IMapper<Employee, EmployeeDTO, IGenericRepository<Employee>>
    {
        public IGenericRepository<Employee> repo;

        public EmployeeMapper(IGenericRepository<Employee> repo)
        {
            this.repo = repo;
        }

        public Employee DeMap(EmployeeDTO dto)
        {
            Employee entity = (Employee)repo.GetByID(dto.Id);
            if (entity == null)
                return new Employee()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Position = dto.Position,
                    BirthDay = new DateTime(DateTime.Now.Year - dto.Age, 1,1) 
                };
            entity.Id = dto.Id;
            entity.Name = dto.Name;
            entity.Position = dto.Position;
            entity.BirthDay = new DateTime(DateTime.Now.Year - dto.Age, entity.BirthDay.Month, entity.BirthDay.Day);
            return entity;
        }

        public EmployeeDTO Map(Employee entity)
        {
            return new EmployeeDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Position = entity.Position,
                Age = (int)(DateTime.Now - entity.BirthDay).TotalDays/365
            };
        }
    }
}
