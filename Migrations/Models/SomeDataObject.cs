using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.DatabaseAnnotations;

namespace Migrations.Models
{
    [TableName("SomeDataTable")]
    [PrimaryKey("id", autoIncrement = true)]
    public class SomeDataObject
    {
        [Column("id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        [NullSetting(NullSetting = NullSettings.Null)]
        public string ExtraData { get; set; }
    }
}