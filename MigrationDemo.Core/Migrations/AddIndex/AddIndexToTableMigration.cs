using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace MigrationDemo.Core.Migrations
{
    [Migration("1.0.1", 1, MigrationDemo.ProductName)]
    public class AddIndexToTableMigration : MigrationBase
    {
        public AddIndexToTableMigration(ISqlSyntaxProvider sqlSyntax, ILogger logger) 
            : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
            
        }

        public override void Up()
        {
            var dbIndexes = SqlSyntax.GetDefinedIndexes(Context.Database)
                .Select(x => x.Item2).ToArray();

            if (dbIndexes.InvariantContains("IX_MigrationDemo_NameKey") == false)
            {
                Create.Index("IX_MigrationDemo_NameKey")
                    .OnTable(MigrationDemo.Tables.MySimpleTable)
                    .OnColumn("name")
                    .Ascending()
                    .WithOptions()
                    .NonClustered();
            }
        }
    }
}