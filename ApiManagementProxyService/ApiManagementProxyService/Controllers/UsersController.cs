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
    /// APIM user management
    /// Please refer to https://docs.microsoft.com/en-us/rest/api/apimanagement/ for full documentation of API and contracts/// Show casing APIM user management. Please refer to https://docs.microsoft.com/en-us/rest/api/apimanagement/2019-01-01/user for full documentation of API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HttpClient client;
        private IOptions<ApimSettings> settings;

        public UsersController(HttpClient httpClient, IOptions<ApimSettings> settings)
        {
            this.client = httpClient;
            this.settings = settings;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ApimUser>>> GetUsers()
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, "users", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ApimCollection<Models.UserContract>>();
            var values = responseData.Value.Select(s => s.ToApimUser());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.ApimUser>> GetUser(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"users/{id}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.UserContract>();
            var value = responseData.ToApimUser();
            return Ok(value);
        }

        
        [HttpGet("{id}/groups")]
        public async Task<ActionResult<IEnumerable<Models.ApimUserGroup>>> GetUserGroups(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"users/{id}/groups", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ApimCollection<Models.GroupContract>>();
            var values = responseData.Value.Select(s => s.ToApimUserGroup());
            return Ok(values);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Models.ApimUser>> CreateOrUpdateUser([FromBody]Models.UserCreateContract model, string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"users/{id}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(model, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.Unicode, "application/json")
            };

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.UserContract>();
            var value = responseData.ToApimUser();
            return Ok(value);
        }

        [HttpPost("{id}/generatessourl")]
        public async Task<ActionResult<Models.ApimSsoUrlResult>> GenerateSsoUrl(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"users/{id}/generateSsoUrl", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.GenerateSsoUrlResult>();
            var value = responseData.ToApimSsoUrlResult();
            return Ok(value);
        }
    }
}
