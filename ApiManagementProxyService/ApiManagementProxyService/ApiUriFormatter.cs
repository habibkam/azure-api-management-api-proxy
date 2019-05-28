namespace ApiManagementProxyService
{
    /// <summary>
    /// Returns request uri endpoint in the expected format
    /// </summary>
    public class ApiUriFormatter
    {
        public static string GetRequestUri(ApimSettings settings, string endPoint, string queryParameters)
        {
            var requestUri = string.Format(settings.UriFormat, settings.SubscriptionId, settings.ResourceGroupName, settings.ServiceName, endPoint, settings.ApiVersion, queryParameters);
            return requestUri;
        }
    }
}
