using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityContext.Fms.Wrapper
{
    public interface IQueryBase
    {
        public string? GetCreateQuery();
        public string? GetReadQuery();
        public string? GetUpdateQuery();
        public string? GetDeleteQuery();
    }
}