namespace ApiManagementProxyService.Models
{
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// APIM custom TAG contract
    /// </summary>
    [DataContract]
    public class ApimTag
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
    }

    /// <summary>
    /// APIM Tag contract
    /// </summary>
    [DataContract]
    public class TagContract
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "properties")]
        public TagContractProperties Properties { get; set; }

        public ApimTag ToApimTag()
        {
            return new ApimTag { Id = Id.Split('/').Last(), DisplayName = Properties.DisplayName };
        }
    }

    /// <summary>
    /// APIM Tag contract properties
    /// </summary>
    [DataContract]
    public class TagContractProperties
    {
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
    }
}
