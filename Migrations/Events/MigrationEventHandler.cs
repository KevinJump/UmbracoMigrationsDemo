using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;

namespace Migrations.Events
{
    public class MigrationEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ApplyMigrations(applicationContext, "Migration.Demo", new SemVersion(1, 4, 0));
        }

        private void ApplyMigrations(ApplicationContext applicationContext, string productName, SemVersion targetVersion)
        {
            var currentVersion = new SemVersion(0);

            var migrations = applicationContext.Services.MigrationEntryService.GetAll(productName);
            var latest = migrations.OrderByDescending(x => x.Version).FirstOrDefault();
            if (latest != null)
                currentVersion = latest.Version;

            if (targetVersion == currentVersion)
                return;

            var migrationRunner = new MigrationRunner(
                applicationContext.Services.MigrationEntryService,
                applicationContext.ProfilingLogger.Logger,
                currentVersion,
                targetVersion,
                productName);

            try
            {
                migrationRunner.Execute(applicationContext.DatabaseContext.Database);
            }
            catch (Exception ex)
            {
                applicationContext.ProfilingLogger
                    .Logger.Error<MigrationEventHandler>("Error running " + productName + " Migration", ex);
            }
        }

    }
}