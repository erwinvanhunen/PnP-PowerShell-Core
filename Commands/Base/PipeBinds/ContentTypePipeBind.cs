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

        public ContentType GetContentType(SPOnlineContext context, bool inSiteHierarchy)
        {
            if (ContentType != null)
            {
                return ContentType;
            }
            if (!string.IsNullOrEmpty(Id))
            {
                if (inSiteHierarchy)
                {
                    return new RestRequest(context, $"Web/AvailableContentTypes('{Id}')").Expand("FieldLinks").Get<ContentType>();
                }
                else
                {
                    return new RestRequest(context, $"Web/ContentTypes('{Id}')").Expand("FieldLinks").Get<ContentType>();
                }
            }
            else
            {
                if (inSiteHierarchy)
                {
                    return new RestRequest(context, $"Web/AvailableContentTypes").Filter($"Name eq '{Name}'").Expand("FieldLinks").Get<ResponseCollection<ContentType>>().Items.FirstOrDefault();
                }
                else
                {
                    return new RestRequest(context, $"Web/ContentTypes").Filter($"Name eq '{Name}'").Expand("FieldLinks").Get<ResponseCollection<ContentType>>().Items.FirstOrDefault();
                }
            }
        }

        public ContentType GetContentTypeFromList(SPOnlineContext context, List list)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                return new RestRequest(context, $"Web/Lists(guid'{list.Id}')/ContentTypes('{Id}')").Expand("FieldLinks").Get<ContentType>();
            }
            else
            {
                return new RestRequest(context, $"Web/Lists(guid'{list.Id}')/ContentTypes").Filter($"Name eq '{Name}'").Expand("FieldLinks").Get<ResponseCollection<ContentType>>().Items.FirstOrDefault();
            }
        }
    }
}
