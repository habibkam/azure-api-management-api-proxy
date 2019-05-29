namespace ApiManagementProxyService.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// APIM group management
    /// Please refer to https://docs.microsoft.com/en-us/rest/api/apimanagement/ for full documentation of API and contracts
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly HttpClient client;
        private IOptions<ApimSettings> settings;

        public GroupsController(HttpClient httpClient, IOptions<ApimSettings> settings)
        {
            this.client = httpClient;
            this.settings = settings;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ApimUserGroup>>> GetGroups()
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"groups", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ApimCollection<Models.GroupContract>>();
            var values = responseData.Value.Select(s => s.ToApimUserGroup());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Models.ApimUserGroup>>> GetGroup(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"groups/{id}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.GroupContract>();
            var value = responseData.ToApimUserGroup();
            return Ok(value);
        }

        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<Models.ApimUser>>> GetGroupUsers(string id)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"groups/{id}/users", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.ApimCollection<Models.UserContract>>();
            var values = responseData.Value.Select(s => s.ToApimUser());
            return Ok(values);
        }

        [HttpPut("{id}/users/{userId}")]
        public async Task<ActionResult<Models.ApimUser>> AddUserToGroup(string id, string userId)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"groups/{id}/users/{userId}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsAsync<Models.UserContract>();
            var value = responseData.ToApimUser();
            return Ok(value);
        }

        [HttpDelete("{id}/users/{userId}")]
        public async Task<ActionResult<Models.ApimUser>> RemoveUserFromGroup([FromBody]Models.UserCreateContract model, string id, string userId)
        {
            var requestUri = ApiUriFormatter.GetRequestUri(this.settings.Value, $"groups/{id}/users/{userId}", string.Empty);
            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return NoContent();
        }
    }
}
