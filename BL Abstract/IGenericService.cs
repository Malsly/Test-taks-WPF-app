using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Abstract
{
    public interface IGenericService<T> where T : class
    {
        void Save();
    }
}
