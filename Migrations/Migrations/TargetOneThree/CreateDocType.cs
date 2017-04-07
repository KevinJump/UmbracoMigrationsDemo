using Jumoo.uSync.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Umbraco.Core;
using Umbraco.Core.IO;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence.Migrations;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Core.Services;

namespace Migrations.Migrations.TargetOneThree
{
    [Migration("1.3.0", 1, "Migration.Demo")]
    public class CreateDocType : MigrationBase
    {
        IContentTypeService _contentTypeService; 

        public CreateDocType(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
            // when dealing with services - you have to go and get hte current context.
            // because it's not passed to us :( 
            _contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
        }

        public override void Down()
        {
            var doctype = _contentTypeService.GetContentType("NewTextPage");

            //
            // i wouldn't do this - because you will loose any content that might 
            // be using this doctype. 
            //
            if (doctype != null)
            {
                _contentTypeService.Delete(doctype);
            }
        }

        public override void Up()
        {
            Logger.Info<CreateDocType>("1.3.0: Running Migration");

            var doctype = _contentTypeService.GetContentType("NewTextPage");

            if (doctype == null)
            {
                var file = IOHelper.MapPath("~/Migrations/TargetOneThree/textpage.udt");
                if (File.Exists(file))
                {
                    XElement node = XElement.Load(file);

                    // just call the packaging service to install the doctype.
                    // it's a bit dirty but it works. 
                    var types = ApplicationContext.Current.Services.PackagingService.ImportContentTypes(node);
                    if (types != null && types.Any())
                    {
                        // worked 
                    }

                    // OR

                    /*
                    // if you had usync...
                    //   (& you just need usync.core - so it doesn't need to be actually syncing your project.) 
                    // 
                    // but you would want a uSync export file because they have all the id mappings in.
                    var attempt = uSyncCoreContext.Instance.ContentTypeSerializer.Deserialize(node, true, true);
                    if (attempt.Success)
                    {
                        // worked...
                    }
                    */
                }


            }
            
        }
    }
}