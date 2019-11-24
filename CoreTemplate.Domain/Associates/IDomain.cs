using CoreTemplate.Repository.Associates;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTemplate.Domain.Associates
{
    public interface IDomain<T>: IRepository<T> where T: class
    {
    }
}
