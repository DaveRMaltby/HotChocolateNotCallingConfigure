using HotChocolate.Data;
using System.Collections.Generic;
using System.Linq;

namespace HotChocolateNotCallingConfigure
{
    public class Queries
    {
        private List<string> tables = new List<string> { "Table1", "Table2" };

        [UseSorting]
        public IQueryable<TableInfo> GetTables() =>
            tables.Select(t => new TableInfo { Name = t }).AsQueryable();

    }
}
