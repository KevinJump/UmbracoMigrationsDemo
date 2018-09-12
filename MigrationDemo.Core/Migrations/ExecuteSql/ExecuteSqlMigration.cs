using MigrationDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace MigrationDemo.Core.Migrations
{
    [Migration("1.2.0", 1, MigrationDemo.ProductName)]
    public class ExecuteSqlMigration : MigrationBase
    {
        public ExecuteSqlMigration(ISqlSyntaxProvider sqlSyntax, ILogger logger)
            : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
            
        }

        /// <summary>
        ///  do some sql work, this maybe anything really.
        /// </summary>
        public override void Up()
        {
            this.Execute.Code(database =>
            {
                var items = database.Fetch<MySimplePoco>($"SELECT Id from {MigrationDemo.Tables.MySimpleTable}");
                if (items.Count > 0)
                {
                    foreach(var item in items)
                    {
                        // do some work on the item? 
                        // database.Execute($"DELETE FROM {MigrationDemo.Tables.MySimpleTable} WHERE id = @id", new { id = item.Id });
                    }
                }

                return "";
            });
        }
    }
}