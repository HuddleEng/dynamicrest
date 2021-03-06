﻿using System.IO;
using System.Xml.Linq;
using DynamicRest.Json;
using DynamicRest.Xml;

namespace DynamicRest {
    public class BuilderResponse
    {
        public object Result { get; set; }
        public string ResponseText { get; set; }

        public BuilderResponse(object result)
        {
            Result = result;
        }
        public BuilderResponse(object result, string responseText) : this(result)
        {
            ResponseText = responseText;
        }
    }

    public class StandardResultBuilder : IBuildDynamicResults
    {
        public StandardResultBuilder(RestService serviceType) {
            ServiceType = serviceType;
        }

        public RestService ServiceType { get; private set; }

        public object CreateResult(string responseText)
        {
            object result;
            switch (ServiceType)
            {
                case RestService.Json:
                    result = GetResultFromJson(responseText);
                    break;
                case RestService.Text:
                    result = GetResultFromText(responseText);
                    break;
                default:
                    result = GetResultFromXml(responseText);
                    break;
            }
            return result;
        }

        public static object GetResultFromText(string responseText)
        {
            return responseText ?? "";
        }

        public static object GetResultFromXml(string responseText) {
            var xmlDocument = XDocument.Parse(responseText);
            dynamic result = new XmlNode(xmlDocument.Root);
            return result;
        }

        public static object GetResultFromJson(string responseText) {
            var jsonReader = new JsonReader(responseText);

            if (responseText == string.Empty) return new JsonObject();

            dynamic result = jsonReader.ReadValue();
            return result;
        }

        public BuilderResponse ProcessResponse(Stream responseStream)
        {
            if (ServiceType == RestService.Binary) {
                return new BuilderResponse(responseStream);
            }

            try {
                var responseText = (new StreamReader(responseStream)).ReadToEnd();
                dynamic result = CreateResult(responseText);

                return new BuilderResponse(result, responseText);
            }
            catch
            {
                // ignored
            }

            return null;
        }
    }
}