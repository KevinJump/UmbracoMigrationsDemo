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
    [Migration("1.1.0", 1, MigrationDemo.ProductName)]
    public class AddColumnMigration : MigrationBase
    {
        public AddColumnMigration(ISqlSyntaxProvider sqlSyntax, ILogger logger)
            : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
            
        }

        /// <summary>
        ///  create a new column, 
        ///  
        ///  when you add a new property to your poco class, anyone who
        ///  has already installed your code will have a table with the 
        ///  extra columns missing, so on this migration we add it. 
        ///  
        ///  however if someone installs your product directly at this 
        ///  version then the initial creation of the table will probibly
        ///  include your new coloumn, so we skip this one with the table check
        /// </summary>
        public override void Up()
        {
            var tables = SqlSyntax.GetTablesInSchema(Context.Database).ToArray();

            // does the table exist ?
            // if it doesn't then that probibly means the create migration
            // hasn't ran yet - and in all probibliy the poco class will
            // already contain our new column, so we let the 
            // create migration create the whole table and don't worry 
            // about adding our column here...
            if (tables.InvariantContains(MigrationDemo.Tables.MySimpleTable))
            {
                var columns = SqlSyntax.GetColumnsInSchema(Context.Database).ToArray();

                if (columns.Any(x => x.TableName.InvariantEquals(MigrationDemo.Tables.MySimpleTable)
                && x.ColumnName.InvariantEquals("myNewColumn")) == false)
                {
                    Create.Column("myNewColumn")
                        .OnTable(MigrationDemo.Tables.MySimpleTable)
                        .AsString()
                        .Nullable();
                }
            }
        }
    }
}