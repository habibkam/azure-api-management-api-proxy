namespace ApiManagementProxyService.Models
{
    using System.Runtime.Serialization;

    /// <summary>
    /// APIM generic collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class ApimCollection<T>
    {
        [DataMember(Name = "value")]
        public T[] Value { get; set; }
        [DataMember(Name = "nextLink")]
        public string NextLink { get; set; }
    }
}
