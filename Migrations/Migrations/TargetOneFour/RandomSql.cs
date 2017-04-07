using Migrations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;

namespace Migrations.Migrations.TargetOneFour
{
    [Migration("1.4.0", 1, "Migration.Demo")]
    public class RandomSql : MigrationBase
    {
        public RandomSql(ISqlSyntaxProvider sqlSyntax, ILogger logger) 
            : base(sqlSyntax, logger)
        {
        }

        public override void Down()
        {
        }

        public override void Up()
        {
            Logger.Info<RandomSql>("1.4.0: Running Migration");

            // you can always just run sql - to do anything to the db... 
            Execute.Sql("SELECT * FROM UmbracoMigrations");

            // and fix things if you want.

            var sql = new Sql()
                .Select("*")
                .From<SomeDataObject>(SqlSyntax);
            /*
            var rows = Context.Database.Fetch<SomeDataObject>(sql);

            foreach(var row in rows)
            {
                row.ExtraData = $"Updated at {DateTime.Now}";
                Context.Database.Update(row);
            }
            */  

        }
    }
}