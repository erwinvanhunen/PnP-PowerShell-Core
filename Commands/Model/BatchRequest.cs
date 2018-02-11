using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class BatchRequest
    {
        private List<BatchItem> items = new List<BatchItem>();
        private SPOnlineContext _context;

        public BatchRequest(SPOnlineContext context)
        {
            _context = context;
        }

        public void Post(string url, string content = null, string select = null, string filter = null, string expand = null, string contentType = "application/json;odata=verbose")
        {
            var restUrl = BuildUrl(url, select, filter, expand);
            var item = new BatchItem();
            item.Url = restUrl;
            item.ContentType = contentType;
            item.Content = content;
            item.Method = "POST";
            items.Add(item);
        }

        public void Merge(string url, MetadataType metadataType, Dictionary<string, object> properties, string contentType = "application/json;odata=verbose")
        {
            properties["__metadata"] = metadataType;
            var content = JsonConvert.SerializeObject(properties);
            var item = new BatchItem();
            item.Url = url;
            item.ContentType = contentType;
            item.Content = content;
            item.Method = "MERGE";
            items.Add(item);
        }

        public void Execute()
        {
            var builder = new StringBuilder();
            var batchIdentifier = $"batch_{(Guid.NewGuid().ToString("N"))}";
            var changeSetIdentifier = $"changeset_{(Guid.NewGuid().ToString("N"))}";
            var contentType = $"multipart/mixed; boundary={batchIdentifier}";
            var host = new Uri(_context.Url).Host;

            builder.AppendLine($"--{batchIdentifier}");
            builder.AppendLine($"Content-Type: multipart/mixed; boundary={changeSetIdentifier}");
            //builder.AppendLine($"Host: {host}");
            //builder.AppendLine("Content-Transfer-Encoding: binary");

            var changeSetBuilder = new StringBuilder();

            foreach (var item in items)
            {
                changeSetBuilder.AppendLine($"--{changeSetIdentifier}");
                changeSetBuilder.AppendLine("Content-Type: application/http");
                changeSetBuilder.AppendLine("Content-Transfer-Encoding: binary");
                changeSetBuilder.AppendLine();
                changeSetBuilder.AppendLine($"{item.Method.ToUpper()} {item.Url} HTTP/1.1");
                changeSetBuilder.AppendLine($"Host: {host}");
                if (!string.IsNullOrEmpty(item.ContentType) && !string.IsNullOrEmpty(item.Content))
                {
                    changeSetBuilder.AppendLine($"Content-Type: {item.ContentType}");
                }
                if (item.Method.ToUpper() == "MERGE")
                {
                    changeSetBuilder.AppendLine("IF-MATCH: *");
                    changeSetBuilder.AppendLine("X-HTTP-Method: MERGE");
                }
                if (!string.IsNullOrEmpty(item.Content))
                {
                    changeSetBuilder.AppendLine($"Content-Length: {item.Content.Length}");
                    changeSetBuilder.AppendLine();
                    changeSetBuilder.AppendLine(item.Content);
                }
                changeSetBuilder.AppendLine();
            }
            changeSetBuilder.AppendLine($"--{changeSetIdentifier}--");
            builder.AppendLine($"Content-Length: {changeSetBuilder.Length}");
            builder.AppendLine();
            builder.Append(changeSetBuilder);
            builder.AppendLine();
            builder.AppendLine($"--{batchIdentifier}--");
            new RestRequest(_context, "$batch").Post(builder.ToString(), contentType);

        }
        private string BuildUrl(string endPointUrl, string select, string filter, string expand)
        {
            var url = endPointUrl;
            if (!url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = _context.Url + "/_api/" + endPointUrl;
            }
            var restparams = new System.Collections.Generic.List<string>();
            if (!string.IsNullOrEmpty(select))
            {
                restparams.Add($"$select={select}");
            }
            if (!string.IsNullOrEmpty(filter))
            {
                restparams.Add($"$filter=({filter})");
            }
            if (!string.IsNullOrEmpty(expand))
            {
                restparams.Add($"$expand={expand}");
            }
            if (restparams.Any())
            {
                url += $"?{string.Join("&", restparams)}";
            }
            return url;
        }
    }

    public class BatchItem
    {
        public string ContentType { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public string Content { get; set; }

    }
}
