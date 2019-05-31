namespace ApiManagementProxyService.Models
{
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom APIM product
    /// </summary>
    [DataContract]
    public class ApimProduct
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "approvalRequired")]
        public bool ApprovalRequired { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "subscriptionRequired")]
        public bool SubscriptionRequired { get; set; }
        [DataMember(Name = "subscriptionsLimit")]
        public int? SubscriptionsLimit { get; set; }
    }

    /// <summary>
    /// APIM Product contract
    /// </summary>
    [DataContract]
    public class ProductContract
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "properties")]
        public ProductContractProperties Properties { get; set; }

        public ApimProduct ToApiProduct()
        {
            return new ApimProduct { Id = Id.Split('/').Last(),
             ApprovalRequired = Properties.ApprovalRequired,
             DisplayName = Properties.DisplayName,
             State = Properties.State,
             SubscriptionRequired = Properties.SubscriptionRequired,
             SubscriptionsLimit = Properties.SubscriptionsLimit};
        }
    }

    /// <summary>
    /// APIM Product contract properties
    /// </summary>
    [DataContract]
    public class ProductContractProperties
    {
        [DataMember(Name = "approvalRequired")]
        public bool ApprovalRequired { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "subscriptionRequired")]
        public bool SubscriptionRequired { get; set; }
        [DataMember(Name = "subscriptionsLimit")]
        public int? SubscriptionsLimit { get; set; }
    }
}
