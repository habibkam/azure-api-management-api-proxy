namespace ApiManagementProxyService.Models
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Customer APIM user
    /// </summary>
    [DataContract]
    public class ApimUser
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "registrationDate")]
        public DateTime RegistrationDate { get; set; }
    }

    /// <summary>
    /// Custom APIM user create or update contract
    /// </summary>
    [DataContract]
    public class ApimUserCreateOrUpdateContact
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }

        public UserCreateOrUpdateContract ToUserCreateContract()
        {
            return new UserCreateOrUpdateContract
            {
                Properties = new UserCreateOrUpdateProperties
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.Password
                }
            };
        }
    }

    /// <summary>
    /// Custom APIM single sign on url result
    /// </summary>
    [DataContract]
    public class ApimSsoUrlResult
    {
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// APIM user create or update contract
    /// </summary>
    [DataContract]
    public class UserCreateOrUpdateContract
    {
        [DataMember(Name = "properties")]
        public UserCreateOrUpdateProperties Properties { get; set; }
    }

    /// <summary>
    /// APIM user create contract properties: more properties might exist. Refer to the documentation
    /// </summary>
    [DataContract]
    public class UserCreateOrUpdateProperties
    {
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }

    /// <summary>
    /// APIM user contract
    /// </summary>
    [DataContract]
    public class UserContract
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "properties")]
        public UserContractProperties Properties { get; set; }

        public ApimUser ToApimUser()
        {
            return new ApimUser { Id = Id.Split('/').Last(),
                Email = Properties.Email,
                FirstName = Properties.FirstName,
                LastName = Properties.LastName,
                State = Properties.State,
                RegistrationDate = Properties.RegistrationDate
            };
        }
    }

    /// <summary>
    /// APIM user create contract properties: more properties might exist. Refer to the documentation
    /// </summary>
    [DataContract]
    public class UserContractProperties : UserCreateOrUpdateProperties
    {
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "registrationDate")]
        public DateTime RegistrationDate { get; set; }
    }

    /// <summary>
    /// APIM Generate sso url result
    /// </summary>
    [DataContract]
    public class GenerateSsoUrlResult
    {
        [DataMember(Name = "value")]
        public string Value { get; set; }

        public ApimSsoUrlResult ToApimSsoUrlResult()
        {
            return new ApimSsoUrlResult { Value = Value };
        }
    }
}
