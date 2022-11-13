using Microsoft.Extensions.Logging;
using PTI.Microservices.Library.Configuration;
using PTI.Microservices.Library.Interceptors;
using PTI.Microservices.Library.Models.PaypalService.GetAccessToken;
using PTI.Microservices.Library.Models.PaypalService.ListPlans;
using PTI.Microservices.Library.Models.PaypalService.ListProducts;
using PTI.Microservices.Library.PayPal.Models;
using PTI.Microservices.Library.PayPal.Models.CreateBatchPayout;
using PTI.Microservices.Library.PayPal.Models.GetOrderDetails;
using PTI.Microservices.Library.PayPal.Models.GetPayoutBatchDetails;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.Services
{
    /// <summary>
    /// Service in cahrge of exposing access to Paypal functionality
    /// </summary>
    public sealed class PaypalService
    {
        private ILogger<PaypalService> Logger { get; }
        private PaypalConfiguration PaypalConfiguration { get; }
        private CustomHttpClient CustomHttpClient { get; }

        /// <summary>
        /// Creates a new instance of <see cref="PaypalService"/>
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="paypalConfiguration"></param>
        /// <param name="customHttpClient"></param>
        public PaypalService(ILogger<PaypalService> logger, PaypalConfiguration paypalConfiguration,
            CustomHttpClient customHttpClient)
        {
            this.Logger = logger;
            this.PaypalConfiguration = paypalConfiguration;
            this.CustomHttpClient = customHttpClient;
        }

        /// <summary>
        /// Gets a list of plans
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="productId"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ListPlansResponse> ListPlansAsync(string accessToken, string productId,
            int pageSize = 10, int page = 1,
            CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"{this.PaypalConfiguration.Endpoint}/v1/billing/plans" +
                    $"?product_id={productId}&page_size={pageSize}&page={page}&total_required=true";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await this.CustomHttpClient.GetFromJsonAsync<ListPlansResponse>(requestUrl);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets the subscription details
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="subscriptionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetSubscriptionDetailsResponse> GetSubscriptionDetailsAsync(string accessToken, string subscriptionId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"{this.PaypalConfiguration.Endpoint}/v1/billing/subscriptions/{subscriptionId}";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await this.CustomHttpClient.GetFromJsonAsync<GetSubscriptionDetailsResponse>(requestUrl,
                    cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets a list of products
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<ListProductsResponse> ListProductsAsync(string accessToken, CancellationToken cancellationToken = default,
            int pageSize = 10, int page = 1)
        {
            try
            {
                string requestUrl = $"{this.PaypalConfiguration.Endpoint}/v1/catalogs/products?page_size={pageSize}&page={page}" +
                    $"&total_required=true";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await this.CustomHttpClient.GetFromJsonAsync<ListProductsResponse>(requestUrl, cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets the access token
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetAccessTokenResponse> GetAccessTokenAsync(ILogger<CustomHttpClientHandler> logger, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"{this.PaypalConfiguration.Endpoint}/v1/oauth2/token";
                using (CustomHttpClientHandler customHttpClientHandler = new CustomHttpClientHandler(logger)
                {
                })
                {
                    using (CustomHttpClient customHttpClient = new CustomHttpClient(customHttpClientHandler))
                    {
                        var credentials = Encoding.ASCII.GetBytes($"{this.PaypalConfiguration.ClientId}:{this.PaypalConfiguration.Secret}");
                        customHttpClient.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("basic", Convert.ToBase64String(credentials));
                        List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("grant_type","client_credentials")
                    };
                        System.Net.Http.FormUrlEncodedContent formUrlEncodedContent = new System.Net.Http.FormUrlEncodedContent(data);
                        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUrl)
                        {
                            Content = formUrlEncodedContent
                        };
                        var response = await customHttpClient.SendAsync(httpRequestMessage, completionOption: HttpCompletionOption.ResponseContentRead, cancellationToken: cancellationToken);
                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadFromJsonAsync<GetAccessTokenResponse>();
                            return result;
                            //var result = await response.Content.ReadFromJsonAsync<GetAppTokenResponse>();
                            //return result;
                        }
                        else
                        {
                            string reason = response.ReasonPhrase;
                            string detailedError = await response.Content.ReadAsStringAsync();
                            throw new Exception($"Reason: {reason}. Details: {detailedError}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<GetOrderDetailsResponse> GetOrderDetailsAsync(string orderId, string accessToken, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"{this.PaypalConfiguration.Endpoint}/v1/checkout/orders/{orderId}";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await this.CustomHttpClient.GetFromJsonAsync<GetOrderDetailsResponse>(requestUrl, cancellationToken: cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Creates a batch of payouts
        /// </summary>
        /// <param name="createBatchPayoutRequest"></param>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CreateBatchPayoutResponse> CreateBatchPayoutAsync(CreateBatchPayoutRequest createBatchPayoutRequest,
            string accessToken, CancellationToken cancellationToken)
        {
            try
            {
                string requestUrl = $"{this.PaypalConfiguration.Endpoint}/v1/payments/payouts";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var response = await this.CustomHttpClient
                    .PostAsJsonAsync<CreateBatchPayoutRequest>(requestUrl, createBatchPayoutRequest, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<CreateBatchPayoutResponse>();
                    return result;
                }
                else
                {
                    string reason = response.ReasonPhrase;
                    string detailedError = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Reason: {reason}. Details: {detailedError}");
                }
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(exception: ex, message: ex.Message);
                throw;
            }
        }

        public async Task<GetPayoutBatchDetailsResponse> GetPayoutBatchDetailsAsync(string payoutBatchId,
            string accessToken, CancellationToken cancellationToken)
        {
            try
            {
                string requestUrl = $"{PaypalConfiguration.Endpoint}/v1/payments/payouts/{payoutBatchId}";
                this.CustomHttpClient.DefaultRequestHeaders.Authorization =
                                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                var result = await this.CustomHttpClient.GetFromJsonAsync<GetPayoutBatchDetailsResponse>(requestUrl);
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(exception: ex, message: ex.Message);
                throw;
            }
        }
    }
}
