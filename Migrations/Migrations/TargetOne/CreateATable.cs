using Migrations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Migrations.Migrations.TargetOne
{
    [Migration("1.0.0", 1, "Migration.Demo")]
    public class CreateATable : MigrationBase
    {
        public CreateATable(ISqlSyntaxProvider sqlSyntax, ILogger logger) 
            : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
            Logger.Info<CreateATable>("1.0.0: Running Migration Down");

            var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();

            if (tables.InvariantContains("SomeDataTable"))
            {
                Delete.Table("SomeDataTable");
            }
        }

        public override void Up()
        {
            Logger.Info<CreateATable>("1.0.0: Running Migration Up");

            var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();

            if (!tables.InvariantContains("SomeDataTable"))
            {
                Create.Table<SomeDataObject>();
            }

            if (!tables.InvariantContains("ManualDataTable"))
            {
                Create.Table("manualDataTable")
                    .WithColumn("id").AsInt32().PrimaryKey("PK_manualData")
                    .WithColumn("name").AsString(64).NotNullable()
                    .WithColumn("Created").AsDateTime().NotNullable();
            }
        }
    }
}