using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Migrations.Migrations.TargetOneOne
{
    [Migration("1.1.0", 1, "Migration.Demo")]
    public class AddColumns : MigrationBase
    {
        public AddColumns(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Logger.Info<AddColumns>("1.1.0: Running Migration");

            var columns = SqlSyntax.GetColumnsInSchema(Context.Database).ToArray();

            // as this table is created based on the object if someone directly installs
            // v1.1 then the first migration will have already created this column
            // so you need to confirm it isn't already there...
            if (columns.Any(x => x.TableName.InvariantEquals("SomeDataTable")
                                && x.ColumnName.InvariantEquals("ExtraData")) == false)
            {
                Create.Column("ExtraData").OnTable("SomeDataTable").AsString(64).Nullable();
            }
        }
    }
}