using MigrationDemo.Core.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Web.Strategies.Migrations;

namespace MigrationDemo.Core
{
    public class MigrationEventHandler : ApplicationEventHandler
    {

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var migrationHelper = new MigrationHelper(applicationContext);
            migrationHelper.ApplyMigration(
                MigrationDemo.ProductName, 
                MigrationDemo.ProductVersion);
           
        }

    }

    /// <summary>
    ///  if you want to do things at the end of a migration, the MigrationStartupHandler has
    ///  an override for you to hookinto post migration 
    ///  (really just a hook into MigrationRunner.Migrated event)
    /// </summary>
    public class PostMigrationHandler : MigrationStartupHander
    {
        protected override void AfterMigration(MigrationRunner sender, MigrationEventArgs e)
        {
            if (!e.ProductName.InvariantEquals(MigrationDemo.ProductName)) return;

            if (e.ConfiguredSemVersion <= "2.0.0")
            {
                // do some stuff after our product has been migrated and 
                // the version is less then or equal to version 2.0.0

                // this is where you should do anything non-databasey (i.e update content)
                // https://github.com/umbraco/Umbraco-CMS/blob/dev-v7/src/Umbraco.Web/Strategies/Migrations/PublishAfterUpgradeToVersionSixth.cs
                // https://github.com/umbraco/Umbraco-CMS/blob/dev-v7/src/Umbraco.Web/Strategies/Migrations/OverwriteStylesheetFilesFromTempFiles.cs
            }
        }
    }
}