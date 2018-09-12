using MigrationDemo.Core.Models;
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
    [Migration("1.0.0", 1, MigrationDemo.ProductName)]
    public class CreateTableMigration : MigrationBase
    {
        public CreateTableMigration(ISqlSyntaxProvider sqlSyntax, ILogger logger) 
            : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
            Logger.Debug<CreateTableMigration>("Deleting tables");
            var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();

            if (tables.InvariantContains(MigrationDemo.Tables.MySimpleTable))
            {
                Delete.Table(MigrationDemo.Tables.MySimpleTable);
            }
        }

        public override void Up()
        {
            Logger.Debug<CreateTableMigration>("Creating tables");

            var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();

            if (!tables.InvariantContains(MigrationDemo.Tables.MySimpleTable))
            {
                Create.Table<MySimplePoco>();
            }
               
        }
    }
}