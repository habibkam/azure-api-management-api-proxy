namespace ApiManagementProxyService
{
    /// <summary>
    /// Settings class
    /// </summary>
    public class ApimSettings
    {
        public string SubscriptionId { get; set; }
        public string ResourceGroupName { get; set; }
        public string ServiceName { get; set; }
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Resource { get; set; }
        public string ApiVersion {get; set;}
        public string UriFormat { get; set; }
    }
}
