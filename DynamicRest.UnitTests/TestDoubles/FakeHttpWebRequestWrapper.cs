﻿using System;
using System.IO;
using System.Net;
using DynamicRest.HTTPInterfaces;

namespace DynamicRest.UnitTests.TestDoubles
{
    public class FakeHttpWebRequestWrapper : IHttpRequest
    {
        private readonly Uri _uri;

        private string _contentType;
        private string _requestBody;
        private WebHeaderCollection _headers;

        public FakeHttpWebRequestWrapper(Uri uri){
            _uri = uri;
        }

        public Uri RequestURI{
            get { return _uri; }
        }

        public string Accept { get; set; }

        public WebHeaderCollection Headers {
            get {
                return _headers;
            }
        }

        public void AddCredentials(ICredentials credentials){
            
        }

        public void AddHeaders(WebHeaderCollection headers) {
            _headers = headers;
        }

        public void AddRequestBody(string contentType, string content) {
            _requestBody = content;
            _contentType = contentType;
        }
        
        public void BeginGetResponse(Action<object> action, object asyncRequest){
           
        }

        public IHttpResponse EndGetResponse(object asyncRequest){
            return new FakeHttpWebResponseWrapper();
        }

        public IHttpResponse GetResponse(){
            return new FakeHttpWebResponseWrapper();
        }

        public Stream GetRequestStream(){
            return new MemoryStream();
        }

        public void SetContentHeaders(string contentType, int contentLength){
            
        }

        public HttpVerb HttpVerb { get; set; }

        public string GetContentType() {
            return _contentType;
        }

        public string GetRequestBody() {
            return _requestBody;
        }
    }
}