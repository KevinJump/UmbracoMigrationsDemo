using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;
using Umbraco.Core.Persistence.DatabaseModelDefinitions;

namespace MigrationDemo.Core.Models
{
    [TableName(MigrationDemo.Tables.MySimpleTable)]
    [PrimaryKey("id")]
    [ExplicitColumns]
    public class MySimplePoco
    {
        [Column("id")]
        [PrimaryKeyColumn]
        public int Id { get; set; }

        [Column("key")]
        [Constraint(Default = SystemMethods.NewGuid)]
        public Guid Key { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        /*
        [Column("myNewColumn")]
        [NullSetting(NullSetting = NullSettings.Null)]
        public string NewColumn;
        */
    }
}