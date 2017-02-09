using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DbContextInherit.DbContexts
{
    public class NonSpecificCommandContext : QueryContext
    {
        public NonSpecificCommandContext() : base()
        {
            Debug.WriteLine("CommandContext parameterless constructor called");
        }

        public NonSpecificCommandContext(DbContextOptions options) : base(options)
        {

            Debug.WriteLine($"=================================\nDerivedContext Type: {this.GetType().GetTypeInfo()}\nDbOptionsContext Type: {options.GetType().GetTypeInfo()}\n****************************");

            if (!options.ContextType.GetTypeInfo().IsAssignableFrom(this.GetType().GetTypeInfo()))
                Debug.WriteLine("DbOptionsContext type is not correct.");


            if (options.HasDbContext())
                Debug.WriteLine(options.GetSqlConnectionString());
            else
                Debug.WriteLine("DbOptionsContext not configured with a SQL Server connection");
        }
    }
}
