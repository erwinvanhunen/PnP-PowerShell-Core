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

        public ContentType GetContentType(SPOnlineConnection context, bool inSiteHierarchy)
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
                    ct = new RestRequest(context, $"Web/AvailableContentTypes('{Id}')").Expand("FieldLinks").Get<ContentType>();
                    //ct = Helpers.RestHelper.ExecuteGetRequest<ContentType>(context, $"Web/AvailableContentTypes('{Id}')");
                }
                else
                {
                    ct = new RestRequest(context, $"Web/ContentTypes('{Id}')").Expand("FieldLinks").Get<ContentType>();
                    //ct = Helpers.RestHelper.ExecuteGetRequest<ContentType>(context, $"Web/ContentTypes('{Id}')");
                }
            }
            else
            {
                if (inSiteHierarchy)
                {
                    //var cts = new RestRequest(context, $"Site/RootWeb/ContentTypes").Select("Name", "Id").Get<ResponseCollection<ContentType>>().Items.FirstOrDefault(c => c.Name == Name);
                    ct = new RestRequest(context, $"Web/AvailableContentTypes").Filter($"Name eq '{Name}'").Expand("FieldLinks").Get<ResponseCollection<ContentType>>().Items.FirstOrDefault();
                    //ct = Helpers.RestHelper.ExecuteGetRequest<ContentType>(context, $"Web/AvailableContentTypes('{cts.Id.StringValue}')");
                }
                else
                {
                    ct = new RestRequest(context, $"Web/ContentTypes").Filter($"Name eq '{Name}'").Expand("FieldLinks").Get<ResponseCollection<ContentType>>().Items.FirstOrDefault();
                    //var cts = new RestRequest(context, $"Web/ContentTypes").Select("Name", "Id").Get<ResponseCollection<ContentType>>().Items.FirstOrDefault(c => c.Name == Name);
                    //ct = new RestRequest(context, $"Web/ContentTypes('{cts.Id.StringValue}')").Expand("FieldLinks").Get<ContentType>();
                    //ct = Helpers.RestHelper.ExecuteGetRequest<ContentType>(context, $"Web/ContentTypes('{cts.Id.StringValue}')");
                }
            }

            return ct;
        }
    }
}
