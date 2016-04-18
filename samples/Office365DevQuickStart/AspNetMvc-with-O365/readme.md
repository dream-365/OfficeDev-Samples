step 1: Manually register your app with Azure AD so it can access Office 365 APIs ([details](https://msdn.microsoft.com/office/office365/howto/add-common-consent-manually)) (skip this step if your already had an app registered)

step 2: set the redirect uri to 'http://localhost:2659/' in app registration

step 3: change the ClientId, ClientSecret and TenantId in your web.config


```
    <add key="ida:ClientId" value="{your_client_id}" />
    <add key="ida:ClientSecret" value="{your_client_secret}
    <add key="ida:TenantId" value="{your_tenant_td}" />
```

step 4: run the project in Visual Studio

