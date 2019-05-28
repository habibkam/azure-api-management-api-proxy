namespace ApiManagementProxyService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Show casing APIM product management
    /// Please refer to https://docs.microsoft.com/en-us/rest/api/apimanagement/ for full documentation of API and contracts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly HttpClient client;
        private readonly IOptions<ApimSettings> settings;

        public ProductsController(HttpClient httpClient, IOptions<ApimSettings> settings)
        {
            this.client = httpClient;
            this.settings = settings;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ApimProduct>>> GetProducts()
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, "products", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ApimCollection<Models.ProductContract>>();
            var values = responseData.Value.Select(s => s.ToApiProduct());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.ApimProduct>> GetProduct(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"products/{id}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ProductContract>();
            var value = responseData.ToApiProduct();
            return Ok(value);
        }

        [HttpGet("{id}/groups")]
        public async Task<ActionResult<IEnumerable<Models.ApimUserGroup>>> GetProductGroups(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"products/{id}/groups", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ApimCollection<Models.GroupContract>>();
            var value = responseData.Value.Select(t => t.ToApimUserGroup());
            return Ok(value);
        }

        [HttpPut("{id}/groups/{groupId}")]
        public async Task<ActionResult<Models.ApimUserGroup>> AddGroupToProduct(string id, string groupId)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"products/{id}/groups/{groupId}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.GroupContract>();
            var value = responseData.ToApimUserGroup();
            return Ok(value);
        }

        [HttpDelete("{id}/groups/{groupId}")]
        public async Task<ActionResult<IEnumerable<Models.ApimUserGroup>>> RemoveGroupFromProduct(string id, string groupId)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"products/{id}/groups/{groupId}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return NoContent();
        }

        [HttpGet("{id}/tags")]
        public async Task<ActionResult<IEnumerable<Models.ApimTag>>> GetProductTags(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"products/{id}/tags", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ApimCollection<Models.TagContract>>();
            var value = responseData.Value.Select(t => t.ToApimTag());
            return Ok(value);
        }

        [HttpPut("{id}/tags/{tagId}")]
        public async Task<ActionResult<Models.ApimTag>> AddTagToProduct(string id, string tagId)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"products/{id}/tags/{tagId}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.TagContract>();
            var value = responseData.ToApimTag();
            return Ok(value);
        }

        [HttpDelete("{id}/tags/{tagId}")]
        public async Task<ActionResult<IEnumerable<Models.ApimTag>>> RemoveTagFromProduct(string id, string tagId)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"products/{id}/tags/{tagId}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return NoContent();
        }
    }
}
