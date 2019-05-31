namespace ApiManagementProxyService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Show casing APIM subscription management
    /// Please refer to https://docs.microsoft.com/en-us/rest/api/apimanagement/ for full documentation of API and contracts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly HttpClient client;
        private readonly IOptions<ApimSettings> settings;

        public SubscriptionsController(HttpClient httpClient, IOptions<ApimSettings> settings)
        {
            this.client = httpClient;
            this.settings = settings;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ApimSubscription>>> GetSubscriptions()
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"subscriptions", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ApimCollection<Models.SubscriptionContract>>();
            var values = responseData.Value.Select(s => s.ToApimSubscription());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.ApimSubscription>> GetSubscription(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"subscriptions/{id}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.SubscriptionContract>();
            var value = responseData.ToApimSubscription();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Models.ApimSubscription>> CreateOrUpdateSubscription([FromBody]Models.ApimSubscriptionCreateOrUpdateContract model, string id, bool? notify)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"subscriptions/{id}", (notify != null) ? $"&notify={notify}" : string.Empty);
            var createOrUpdateSubscriptionContract = model.ToSubscriptionCreateOrUpdateContract(this.settings.Value);
            var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(createOrUpdateSubscriptionContract, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.SubscriptionContract>();
            var value = responseData.ToApimSubscription();
            return Ok(value);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Models.ApimSubscription>> UpdateSubsription([FromBody]Models.ApimSubscriptionCreateOrUpdateContract model, string id, bool? notify)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"subscriptions/{id}", (notify != null) ? $"&notify={notify}" : string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Head, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            response.Headers.TryGetValues("etag", out IEnumerable<string> etags);

            // optionally handle if match to make sure not to override a subscription after it has been changed
            request = new HttpRequestMessage(HttpMethod.Patch, requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(model.ToSubscriptionCreateOrUpdateContract(this.settings.Value), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode, "application/json")
            };

            request.Headers.TryAddWithoutValidation("If-Match", etags.First().Replace("\"", string.Empty));
            response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubsription(string id, bool? notify)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"subscriptions/{id}", (notify != null) ? $"&notify={notify}" : string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return NoContent();
        }
    }
}
