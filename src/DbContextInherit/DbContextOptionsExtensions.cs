using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace DbContextInherit
{
    public static class DbContextOptionsExtensions
    {
        public static bool HasDbContext(this DbContextOptions options)
        {
            var sqlExt = options.Extensions.FirstOrDefault(e => e is SqlServerOptionsExtension);
            return (sqlExt != null);
        }

        public static string GetSqlConnectionString(this DbContextOptions options)
        {
            var sqlExt = options.Extensions.FirstOrDefault(e => e is SqlServerOptionsExtension);

            if (sqlExt == null)
                throw (new Exception("Failed to retrieve SQL connection string for base Context"));

            return ((SqlServerOptionsExtension)sqlExt).ConnectionString;
        }

        public static DbContextOptions<T> ChangeOptionsType<T>(this DbContextOptions options) where T : DbContext
        {
            var sqlExt = options.Extensions.FirstOrDefault(e => e is SqlServerOptionsExtension);

            if (sqlExt == null)
                throw (new Exception("Failed to retrieve SQL connection string for base Context"));

            return new DbContextOptionsBuilder<T>()
                        .UseSqlServer(((SqlServerOptionsExtension)sqlExt).ConnectionString)
                        .Options;
        }
    }
}
