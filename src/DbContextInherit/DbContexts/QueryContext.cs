using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DbContextInherit.DbContexts
{
    public class QueryContext : DbContext
    {
        public QueryContext() : base()
        {
        }

        public QueryContext(DbContextOptions options) : base(options)
        {
            Debug.WriteLine($"=================================\nDerivedContext Type: {this.GetType().GetTypeInfo()}\nDbOptionsContext Type: {options.GetType().GetTypeInfo()}\n****************************");

            if (options.HasDbContext())
                Debug.WriteLine(options.GetSqlConnectionString());
            else
                Debug.WriteLine("DbOptionsContext not configured with a SQL Server connection");
        }
    }
}
