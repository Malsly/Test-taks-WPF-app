using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Position { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsWorking { get; set; }
    }
}
