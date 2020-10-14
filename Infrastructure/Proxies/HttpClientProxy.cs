using Infrastructure.ExecuteApi;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Proxies
{
    public class HttpClientProxy : IProxy<HttpClient>
    {
        /// <summary>
        /// Send http get requset with query string paramerters
        /// </summary>
        /// <typeparam name="ReturnType">
        /// The return type which data will be casted to
        /// </typeparam>
        /// <param name="data">
        /// The paramter which will be passed to request
        /// </param>
        /// <param name="requestedUrl">
        /// The api endpoint
        /// </param>
        /// <returns>
        /// Retrun the response body casted to to the type passed
        /// </returns>
        public Task<ReturnType> ExecuteBodyGetRequestAsync<ReturnType>(object data, string requestedUrl)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential("elastic", "Amman#1!"),
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(requestedUrl),
                    Content = new StringContent(data != null ? JsonConvert.SerializeObject(data) : "", Encoding.UTF8, "application/json"),
                };

                response = httpClient.SendAsync(request).Result;
            }

            return response.Content.ReadAsAsync<ReturnType>();
        }

        /// <summary>
        /// Send http get requset with query string paramerters
        /// </summary>
        /// <typeparam name="ReturnType">
        /// The return type which data will be casted to
        /// </typeparam>
        /// <param name="data">
        /// The paramter which will be passed to request
        /// </param>
        /// <param name="setting">
        /// </param>
        /// <param name="requestedUrl">
        /// The api endpoint
        /// </param>
        /// <returns>
        /// Retrun the response body casted to to the type passed
        /// </returns>

        public ReturnType ExecuteBodyGetRequest<ReturnType>(object data, string requestedUrl, ExecuteApiSetting setting)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = setting.NetworkCredential,
                ServerCertificateCustomValidationCallback = setting.ServerCertificateCustomValidationCallback
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(requestedUrl),
                    Content = new StringContent(data != null ? JsonConvert.SerializeObject(data) : "", Encoding.UTF8, "application/json"),
                };
                response = httpClient.SendAsync(request).Result;
            }
            return response.Content.ReadAsAsync<ReturnType>().Result;
        }

        /// <summary>
        /// Send http post requset
        /// </summary>
        /// <typeparam name="ReturnType">
        /// The return type which data will be casted to
        /// </typeparam>
        /// <param name="data">
        /// The paramter which will be passed to request
        /// </param>
        /// <param name="setting">
        /// </param>
        /// <param name="requestedUrl">
        /// The api endpoint
        /// </param>
        /// <returns>
        /// Retrun the response body casted to to the type passed
        /// </returns>
        public ReturnType ExecuteBodyPostRequest<ReturnType>(object data, string requestedUrl, ExecuteApiSetting setting)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = setting.NetworkCredential,
                ServerCertificateCustomValidationCallback = setting.ServerCertificateCustomValidationCallback
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(requestedUrl),
                    Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
                };
                response = httpClient.SendAsync(request).Result;
            }
            return response.Content.ReadAsAsync<ReturnType>().Result;
        }
        public void ExecuteBodyPostRequest(object data, string requestedUrl)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(requestedUrl),
                    Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
                };
                response = httpClient.SendAsync(request).Result;
            }
        }

        public void ExecuteBodyPostRequestWitoutSerilize(string data, string requestedUrl)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(requestedUrl),
                    Content = new StringContent(data, Encoding.UTF8, "application/json"),
                };
                response = httpClient.SendAsync(request).Result;
            }
        }


        public ReturnType ExecuteBodyPostRequest<ReturnType>(object data, string requestedUrl, string token)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(requestedUrl),
                    Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
                };
                response = httpClient.SendAsync(request).Result;
            }
            return response.Content.ReadAsAsync<ReturnType>().Result;
        }



        public ReturnType ExecuteBodyPostRequest<ReturnType>(object data, string requestedUrl)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(requestedUrl),
                    Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"),
                };
                response = httpClient.SendAsync(request).Result;
            }
            return response.Content.ReadAsAsync<ReturnType>().Result;
        }
        /// <summary>
        /// Send http post requset
        /// </summary>
        /// <typeparam name="ReturnType">
        /// The return type which data will be casted to
        /// </typeparam>
        /// <param name="data">
        /// The paramter which will be passed to request
        /// </param>
        /// <param name="requestedUrl">
        /// The api endpoint
        /// </param>
        /// <returns>
        /// Retrun the response body casted to to the type passed
        /// </returns>


        /// <summary>
        /// Send http get requset
        /// </summary>
        /// <typeparam name="ReturnType">
        /// The return type which data will be casted to
        /// </typeparam>
        /// <param name="apiEndPoint">
        /// The api endpoint
        /// </param>
        /// <param name="setting">
        /// </param>
        /// <returns>
        /// Retrun the response body casted to to the type passed
        /// </returns>
        public ReturnType ExecuteGetRequest<ReturnType>(string apiEndPoint, ExecuteApiSetting setting)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = setting.NetworkCredential,
                ServerCertificateCustomValidationCallback = setting.ServerCertificateCustomValidationCallback
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(apiEndPoint),
                };

                response = httpClient.SendAsync(request).Result;
            }

            return response.Content.ReadAsAsync<ReturnType>().Result;
        }

        /// <summary>
        /// Send http get requset
        /// </summary>
        /// <typeparam name="ReturnType">
        /// The return type which data will be casted to
        /// </typeparam>
        /// <param name="apiEndPoint">
        /// The api endpoint
        /// </param>
        /// <returns>
        /// Retrun the response body casted to to the type passed
        /// </returns>
        public ReturnType ExecuteGetRequest<ReturnType>(string apiEndPoint)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(apiEndPoint),
                };

                response = httpClient.SendAsync(request).Result;

            }

            return response.Content.ReadAsAsync<ReturnType>().Result;

        }

        /// <summary>
        /// Send http put requset
        /// </summary>
        /// <typeparam name="ReturnType">
        /// The return type which data will be casted to
        /// </typeparam>
        /// <param name="requestedUrl">
        /// The api endpoint
        /// </param>
        /// <param name="setting">
        /// </param>
        /// <returns>
        /// Retrun the response body casted to to the type passed
        /// </returns>
        public ReturnType ExecutePutRequest<ReturnType>(string requestedUrl, ExecuteApiSetting setting)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
                Credentials = setting.NetworkCredential,
                ServerCertificateCustomValidationCallback = setting.ServerCertificateCustomValidationCallback
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(requestedUrl),
                };

                response = httpClient.SendAsync(request).Result;
            }

            return response.Content.ReadAsAsync<ReturnType>().Result;
        }

        /// <summary>
        /// Send http put requset
        /// </summary>
        /// <typeparam name="ReturnType">
        /// The return type which data will be casted to
        /// </typeparam>
        /// <param name="requestedUrl">
        /// The api endpoint
        /// </param>
        /// <returns>
        /// Retrun the response body casted to to the type passed
        /// </returns>
        public void ExecutePutRequest(string requestedUrl)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(requestedUrl),
                };

                response = httpClient.SendAsync(request).Result;

            }

        }

        public ReturnType ExecutePutRequest<ReturnType>(string requestedUrl)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(requestedUrl),
                };

                response = httpClient.SendAsync(request).Result;
            }

            return response.Content.ReadAsAsync<ReturnType>().Result;
        }

        public ReturnType ExecuteDeleteRequest<ReturnType>(string apiEndPoint)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(apiEndPoint),
                };

                response = httpClient.SendAsync(request).Result;
            }
            return response.Content.ReadAsAsync<ReturnType>().Result;
        }

        public void ExecuteDeleteRequest(string apiEndPoint)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpClientHandler handler = new HttpClientHandler
            {
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(apiEndPoint),
                };

                response = httpClient.SendAsync(request).Result;
            }
        }
    }
}
