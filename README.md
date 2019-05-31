# azure-api-management-api-proxy

This web API demonstrates how to use REST API provided by Azure API Management to perform operations on selected entities, such as users, groups, products, and subscriptions.
Not all functionality have been implemented in this proxy service and some of collections using paging that has not been made use of in this proxy.
Please, refer to [APIM docs](https://docs.microsoft.com/en-us/rest/api/apimanagement/) for full documentation.
It can be used as a facade to encapsulate the authentication and url setup for services that might require to interact with an APIM instance.

## Requirements

You have:
- Created an APIM instance with a few APIs/operations and products (the web api doesn't allow you create these entities, but that functionality can be added)
- Created an AAD application in the same subscription and have access to an app id and app key
- Given admin access to the above AAD application  for you APIM instance
- Setup applicationsettings.json file with the required parameters (you can use a more authentication mechanism using certificate so that you don't have to add you app key in the clear to the config file)

## Deployment

### Local

Build and deploy the web api locally.
You can use an app like Postman to test the endpoints.

### Public

The web API doesn't offer any authentication mechanism.
If you plan to expose it to the web you should supplement it with authentication mechanism.
