﻿// AmazonSample.cs
// DynamicRest provides REST service access using C# 4.0 dynamic programming.
// The latest information and code for the project can be found at
// https://github.com/NikhilK/dynamicrest
//
// This project is licensed under the BSD license. See the License.txt file for
// more information.
//

using System;
using DynamicRest;
using DynamicRest.HTTPInterfaces.WebWrappers;

namespace Application {

    internal static class AmazonSample {

        public static void Run() {
            //TODO: Fix this up with a request wrapper
            AmazonUriSigner signer = new AmazonUriSigner(Services.AmazonAccessKey, Services.AmazonSecretKey);
            var templatedUriBuilder = new TemplatedUriBuilder();
            templatedUriBuilder.SetUriTransformer(signer);
            templatedUriBuilder.UriTemplate = Services.AmazonUri;
            dynamic amazon = new RestClient(
                new RequestBuilder(null, new RequestFactory(), templatedUriBuilder),
                new ResponseProcessor(RestService.Xml));

            dynamic searchOptions = new JsonObject();
            searchOptions.SearchIndex = "Books";
            searchOptions.Keywords = "Dynamic Programming";

            dynamic search = amazon.ItemSearch(searchOptions);
            dynamic bookList = search.Result;

            foreach (dynamic book in bookList.SelectAll("Item")) {
                Console.WriteLine(book.ASIN + " : " + book.ItemAttributes.Title);
            }
        }
    }
}
