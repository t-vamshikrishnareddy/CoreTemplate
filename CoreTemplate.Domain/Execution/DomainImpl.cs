using CoreTemplate.DataAccess;
using CoreTemplate.Domain.Associates;
using CoreTemplate.Entity;
using CoreTemplate.Repository.Execution;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTemplate.Domain.Execution
{
    public class DomainImpl: DbRepository<TableModel>, IDomain<TableModel>
    {
        public CoreTemplateDataAccess CoreTemplateDataAccess { get; set; }
        public DomainImpl(CoreTemplateDataAccess dataAccess): base(dataAccess)
        {
            CoreTemplateDataAccess = dataAccess;
        }
    }
}
