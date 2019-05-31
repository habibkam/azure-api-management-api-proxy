namespace ApiManagementProxyService.Models
{
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Custom APIM user group
    /// </summary>
    [DataContract]
    public class ApimUserGroup
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "builtIn")]
        public bool BuiltIn { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// APIM group contract
    /// </summary>
    [DataContract]
    public class GroupContract
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "properties")]
        public GroupContractProperties Properties { get; set; }

        public ApimUserGroup ToApimUserGroup()
        {
            return new ApimUserGroup { BuiltIn = Properties.BuiltIn, Description = Properties.Description, DisplayName = Properties.Description, Type = Properties.Type, Id = Id.Split('/').Last() };
        }
    }

    /// <summary>
    /// APIM group contract properties. More properties might be supported. Refer to the documentation.
    /// </summary>
    [DataContract]
    public  class GroupContractProperties
    {
        [DataMember(Name = "builtIn")]
        public bool BuiltIn { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
