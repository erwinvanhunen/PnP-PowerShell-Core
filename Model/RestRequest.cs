﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharePointPnP.PowerShell.Core.Model
{
    public class RestRequest
    {
        private string _root;
        private List<string> _expands;
        private List<string> _selects;
        private string _filter;

        public RestRequest()
        {
            _expands = new List<string>();
            _selects = new List<string>();
            _filter = "";
        }

        public RestRequest(string root, string filter = null)
        {
            _root = root;
            _expands = new List<string>();
            _selects = new List<string>();
            _filter = filter;
        }

        public RestRequest Root(string root, string filter = null)
        {
            _root = root;
            _filter = filter;
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

        public T Get<T>()
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecuteGetRequest<T>(_root, select, _filter, expands);
        }

        public string Get()
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecuteGetRequest(_root, select, _filter, expands);
        }
        
        public T Post<T>(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecutePostRequest<T>(_root, content, select, _filter, expands, contentType: contentType);
        }

        public void Post(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            Helpers.RestHelper.ExecutePostRequest(_root, content, select, _filter, expands, contentType: contentType);
        }

         public T Put<T>(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecutePutRequest<T>(_root, content, select, _filter, expands, contentType: contentType);
        }

        public void Put(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            Helpers.RestHelper.ExecutePutRequest(_root, content, select, _filter, expands, contentType: contentType);
        }

           public T Merge<T>(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            return Helpers.RestHelper.ExecuteMergeRequest<T>(_root, content, select, _filter, expands, contentType: contentType);
        }

        public void Merge(string content = null, string contentType = null)
        {
            var select = _selects.Any() ? string.Join(",", _selects) : null;
            var expands = _expands.Any() ? string.Join(",", _expands) : null;
            Helpers.RestHelper.ExecuteMergeRequest(_root, content, select, _filter, expands, contentType: contentType);
        }
    }
}
