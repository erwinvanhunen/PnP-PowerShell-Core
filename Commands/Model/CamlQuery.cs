using System;
using System.Collections.Generic;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class CamlQuery : ClientSideObject
    {
        public string ViewXml { get; set; }

        public CamlQuery() : base("SP.CamlQuery")
        { }

        public static CamlQuery CreateAllItemsQuery()
        {
            return new CamlQuery
            {
                ViewXml = "<View Scope=\"RecursiveAll\">\r\n    <Query>\r\n    </Query>\r\n</View>"
            };
        }

        public static CamlQuery CreateAllFoldersQuery()
        {
            return new CamlQuery
            {
                ViewXml = "<View Scope=\"RecursiveAll\">\r\n    <Query>\r\n        <Where>\r\n            <Eq>\r\n                <FieldRef Name=\"FSObjType\" />\r\n                <Value Type=\"Integer\">1</Value>\r\n            </Eq>\r\n        </Where>\r\n    </Query>\r\n</View>"
            };
        }

    }
}
