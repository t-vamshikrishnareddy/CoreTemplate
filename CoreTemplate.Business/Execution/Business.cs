using CoreTemplate.Business.Associates;
using CoreTemplate.Domain.Associates;
using CoreTemplate.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTemplate.Business.Execution
{
    public class BusinessImpl: IBusiness
    {
        public IDomain<TableModel> Domain { get; set; }
        public BusinessImpl(IDomain<TableModel> domain)
        {
            Domain = domain;
        }
    }
}
