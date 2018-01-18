using SharePointPnP.PowerShell.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Base.PipeBinds
{
    public sealed class ContentTypePipeBind
    {
        private readonly string _id;
        private readonly string _name;
        private readonly ContentType _contentType;

        public ContentTypePipeBind()
        {
            _id = string.Empty;
            _name = string.Empty;
            _contentType = null;
        }

        public ContentTypePipeBind(string id)
        {
            if (id.ToLower().StartsWith("0x0"))
            {
                _id = id;
            }
            else
            {
                _name = id;
            }

        }

        public ContentTypePipeBind(ContentType contentType)
        {
            _contentType = contentType;
        }

        public string Id
        {
            get
            {
                if (_contentType != null)
                {
                    return _contentType.StringId;
                }
                else
                {
                    return _id;
                }
            }
        }

        public string Name => _name;

        public ContentType ContentType => _contentType;

        public ContentType GetContentType(bool inSiteHierarchy)
        {
            if (ContentType != null)
            {
                return ContentType;
            }
            ContentType ct;
            if (!string.IsNullOrEmpty(Id))
            {
                if (inSiteHierarchy)
                {
                    ct = Helpers.RestHelper.ExecuteGetRequest<ContentType>($"Site/RootWeb/ContentTypes('{Id}')");
                }
                else
                {
                    ct = Helpers.RestHelper.ExecuteGetRequest<ContentType>($"Web/ContentTypes('{Id}')");
                }
            }
            else
            {
                if (inSiteHierarchy)
                {
                    var cts = Helpers.RestHelper.ExecuteGetRequest<ResponseCollection<ContentType>>($"Site/RootWeb/ContentTypes", "Name,Id").Items.FirstOrDefault(c => c.Name == Name);
                    ct = Helpers.RestHelper.ExecuteGetRequest<ContentType>($"Site/RootWeb/ContentTypes('{cts.Id.StringValue}')");
                }
                else
                {
                    var cts = Helpers.RestHelper.ExecuteGetRequest<ResponseCollection<ContentType>>($"Web/ContentTypes", "Name,Id").Items.FirstOrDefault(c => c.Name == Name);
                    ct = Helpers.RestHelper.ExecuteGetRequest<ContentType>($"Web/ContentTypes('{cts.Id.StringValue}')");
                }
            }

            return ct;
        }
    }
}
