using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SharePointPnP.PowerShell.Core.Base;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class RestRequest
    {
        private string _root;
        private List<string> _expands;
        private List<string> _selects;
        private string _filter;
        private SPOnlineContext _context;

        public RestRequest(SPOnlineContext context)
        {
            _context = context;
            _expands = new List<string>();
            _selects = new List<string>();
            _filter = "";
        }

        public RestRequest(SPOnlineContext context , ClientSideObject clientSideObject)
        {
            _context = context;
            _root = clientSideObject.ObjectPath;
            _expands = new List<string>();
            _selects = new List<string>();
        }

        public RestRequest(SPOnlineContext context, string root)
        {
            _context = context;
            _root = root;
            _expands = new List<string>();
            _selects = new List<string>();
            //_filter = filter;
        }

        public RestRequest Filter(string filter)
        {
            _filter = filter;
            return this;
        }

        public RestRequest Root(string root)
        {
            _root = root;
            return this;
        }

        public RestRequest Expand(params string[] expand)
        {
            _expands.AddRange(expand);
            return this;
        }

        public RestRequest Select(params string[] select)
        {
            _selects.AddRange(select);
            return this;
        }

        public T Get<T>() where T : IClientSideObject
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecuteGetRequest<T>(_context, _root, select, _filter, expands);
        }


        public string Get()
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecuteGetRequest(_context, _root, select, _filter, expands);
        }

        public T Post<T>(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecutePostRequest<T>(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public void Post(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            Helpers.RestHelper.ExecutePostRequest(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public void Post(ClientSideObject clientSideObject)
        {
            var content = JsonConvert.SerializeObject(clientSideObject, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            Helpers.RestHelper.ExecutePostRequest(_context, _root, content, select, _filter, expands, null, contentType:"application/json;odata=verbose");
        }

        public void Post(MetadataType metadataType, Dictionary<string, object> properties, string contentType = "application/json; odata=verbose")
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            properties["__metadata"] = metadataType;
            var content = JsonConvert.SerializeObject(properties);
            Helpers.RestHelper.ExecutePostRequest(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public T Post<T>(MetadataType metadataType, Dictionary<string, object> properties, string contentType = "application/json; odata=verbose")
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            properties["__metadata"] = metadataType;
            var payload = new Dictionary<string, object>() {
                { "parameters", properties}
            };
            var content = JsonConvert.SerializeObject(payload);

            return Helpers.RestHelper.ExecutePostRequest<T>(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public T Put<T>(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecutePutRequest<T>(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public void Put(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            Helpers.RestHelper.ExecutePutRequest(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public T Merge<T>(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecuteMergeRequest<T>(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public T Merge<T>(MetadataType metadataType, Dictionary<string, object> properties, string contentType = "application/json;odata=verbose")
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            properties["__metadata"] = metadataType;
            var content = JsonConvert.SerializeObject(properties);
            return Helpers.RestHelper.ExecutePostRequest<T>(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public void Merge(MetadataType metadataType, Dictionary<string, object> properties, string contentType = "application/json;odata=verbose")
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            properties["__metadata"] = metadataType;
            var content = JsonConvert.SerializeObject(properties);
            Helpers.RestHelper.ExecuteMergeRequest(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public void Merge(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            Helpers.RestHelper.ExecuteMergeRequest(_context, _root, content, select, _filter, expands, contentType: contentType);
        }

        public void Delete(string content = null, string contentType = "application/json;odata=version")
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            Helpers.RestHelper.ExecuteDeleteRequest(_context, _root, content, select, _filter);
        }
    }
}
