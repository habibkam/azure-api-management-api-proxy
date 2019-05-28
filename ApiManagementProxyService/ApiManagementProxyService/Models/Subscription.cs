namespace ApiManagementProxyService.Models
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom APIM subscription
    /// </summary>
    [DataContract]
    public class ApimSubscription
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "ownerId")]
        public string OwnerId { get; set; }
        [DataMember(Name = "scopeType")]
        public string ScopeType { get; set; }
        [DataMember(Name = "scopeId")]
        public string ScopeId { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; }
        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }
        [DataMember(Name = "endDate")]
        public DateTime? EndDate { get; set; }
        [DataMember(Name = "expirationDate")]
        public DateTime? ExpirationDate { get; set; }
        [DataMember(Name = "notificationDate")]
        public DateTime? NotificationDate { get; set; }
        [DataMember(Name = "stateComment")]
        public string StateComment { get; set; }
    }

    /// <summary>
    /// APIM subscription contract
    /// </summary>
    [DataContract]
    public class SubscriptionContract
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "properties")]
        public SubscriptionContractProperties Properties { get; set; }

        public ApimSubscription ToApimSubscription()
        {
            return new ApimSubscription { Id = Id.Split('/').Last(),
                DisplayName = Properties.DisplayName,
                OwnerId = Properties.OwnerId?.Split('/').Last(),
                CreatedDate = Properties.CreatedDate,
                EndDate = Properties.EndDate,
                ExpirationDate = Properties.ExpirationDate,
                NotificationDate = Properties.NotificationDate,
                ScopeType = Properties.Scope.Split('/').Reverse().Skip(1).Take(1).First(),
                ScopeId = Properties.Scope.Split('/').Last(),
                StartDate = Properties.StartDate,
                State = Properties.State,
                StateComment = Properties.StateComment };
        }
    }

    /// <summary>
    /// APIM subscription contract properties. More properties might be supported. Refer to the documentation.
    /// </summary>
    [DataContract]
    public class SubscriptionContractProperties : SubscriptionCreateProperties
    {
        [DataMember(Name = "createdDate")]
        public DateTime CreatedDate { get; set; }
        [DataMember(Name = "startDate")]
        public DateTime? StartDate { get; set; }
        [DataMember(Name = "endDate")]
        public DateTime? EndDate { get; set; }
        [DataMember(Name = "expirationDate")]
        public DateTime? ExpirationDate { get; set; }
        [DataMember(Name = "notificationDate")]
        public DateTime? NotificationDate { get; set; }
        [DataMember(Name = "stateComment")]
        public string StateComment { get; set; }
    }

    /// <summary>
    /// APIM subscription create contract
    /// </summary>
    [DataContract]
    public class SubscriptionCreateParameters
    {
        [DataMember(Name = "properties")]
        public SubscriptionCreateProperties Properties { get; set; }
    }

    /// <summary>
    /// APIM subscription create properties
    /// </summary>
    [DataContract]
    public class SubscriptionCreateProperties
    {
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "ownerId")]
        public string OwnerId { get; set; }
        [DataMember(Name = "scope")]
        public string Scope { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
    }
}
