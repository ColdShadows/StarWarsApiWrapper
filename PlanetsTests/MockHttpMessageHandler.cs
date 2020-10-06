using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PlanetsTests
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;
        private readonly HttpStatusCode _statusCode;

        public MockHttpMessageHandler(string response, HttpStatusCode statusCode)
        {
            _response = response;
            _statusCode = statusCode;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return new HttpResponseMessage
            {
                StatusCode = _statusCode,
                Content = new StringContent(_response)
            };
        }
    }

    public static class MockHttpClientHelper
    {
        public static HttpClient GetHttpClient(string content, HttpStatusCode httpStatusCode)
        {
            var handler = new MockHttpMessageHandler(content, httpStatusCode);
            var httpClient = new HttpClient(handler);
            httpClient.BaseAddress = new Uri("http://fakeAddress.com");

            return httpClient;
        }
    }
}
