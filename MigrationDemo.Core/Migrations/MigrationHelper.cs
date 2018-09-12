using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Services;

namespace MigrationDemo.Core.Migrations
{
    public class MigrationHelper
    {
        private readonly IMigrationEntryService migrationService;
        private readonly ProfilingLogger profilingLogger;
        private readonly DatabaseContext databaseContext;

        public MigrationHelper(ApplicationContext applicationContext)
        {
            this.migrationService = applicationContext.Services.MigrationEntryService;
            this.profilingLogger = applicationContext.ProfilingLogger;
            this.databaseContext = applicationContext.DatabaseContext;
        }

        public SemVersion ApplyMigration(string productName, string targetVersion)
        {
            var currentVersion = new SemVersion(0);

            var migrations = this.migrationService.GetAll(productName);
            var latest = migrations.OrderByDescending(x => x.Version)
                .FirstOrDefault();

            if (latest != null)
                currentVersion = latest.Version;

            profilingLogger.Logger.Debug<MigrationHelper>("[{0}] Current: {1}, Target: {2}",
                ()=> productName, ()=> currentVersion.ToString(), ()=> targetVersion.ToString());

            if (targetVersion == currentVersion)
                return currentVersion;

            var migrationRunner = new MigrationRunner(
                migrationService,
                profilingLogger.Logger,
                currentVersion,
                targetVersion,
                productName);

            try
            {
                migrationRunner.Execute(databaseContext.Database);
            }
            catch(Exception ex)
            {
                profilingLogger.Logger.Error<MigrationHelper>("Error running migration:", ex);
            }

            return currentVersion;
        }
    }
}