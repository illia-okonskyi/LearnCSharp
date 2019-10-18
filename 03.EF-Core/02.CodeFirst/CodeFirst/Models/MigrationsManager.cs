using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CodeFirst.Models
{
    public class MigrationsManager
    {
        private readonly IEnumerable<Type> _contextTypes;
        private readonly IServiceProvider _provider;

        public MigrationsManager(IServiceProvider provider)
        {
            _provider = provider;
            _contextTypes = provider.GetServices<DbContextOptions>().Select(o => o.ContextType);
            ContextNames = _contextTypes.Select(t => t.FullName);
            ContextName = ContextNames.First();
        }

        public IEnumerable<string> ContextNames;
        public string ContextName { get; set; }
        public DbContext Context => _provider.GetRequiredService(Type.GetType(ContextName)) as DbContext;
        public IEnumerable<string> AppliedMigrations => Context.Database.GetAppliedMigrations();
        public IEnumerable<string> PendingMigrations => Context.Database.GetPendingMigrations();
        public IEnumerable<string> AllMigrations => Context.Database.GetMigrations();

        public void Migrate(string contextName, string target = null)
        {
            Context.GetService<IMigrator>().Migrate(target);
        }
    }
}
