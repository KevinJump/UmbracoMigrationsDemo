using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Migrations.Models
{
    [Migration("1.0.0", 2, "Migration.Demo")]
    public class AddIndexes : MigrationBase
    {
        public AddIndexes(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
            var indexes = SqlSyntax.GetDefinedIndexes(Context.Database).ToArray();

            var found = indexes.FirstOrDefault(
                x => x.Item1.InvariantEquals("someDataTable")
                && x.Item2.InvariantContains("IX_someDataTable_Name"));

            if (found != null)
            {
                Delete.Index("IX_someDataTable_Name").OnTable("SomeDataTable");
            }

        }

        public override void Up()
        {
            Logger.Info<AddIndexes>("1.0.0: Running Migration");

            var indexes = SqlSyntax.GetDefinedIndexes(Context.Database).ToArray();

            var found = indexes.FirstOrDefault(
                x => x.Item1.InvariantEquals("someDataTable")
                && x.Item2.InvariantContains("IX_someDataTable_Name"));

            if (found == null)
            {
                Create.Index("IX_someDataTable_Name")
                    .OnTable("SomeDataTable")
                    .OnColumn("name").Ascending()
                    .WithOptions().NonClustered();
            }
        }


    }
}