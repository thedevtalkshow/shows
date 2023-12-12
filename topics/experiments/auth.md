# Guide to adding authentication to Blazor app

## Simple setup from File > New Project to Microsoft accounts

Create a new folder, change to it, and run the following:

``` bash
dotnet new blazor
```

This creates a new Blazor project with ASP.NET Core 8 (RC1 for now).

Now add the `Microsoft.AspNetCore.Authentication.MicrosoftAccount` package (,net 8 rc1 version)
``` bash
dotnet add package Microsoft.AspNetCore.Authentication.MicrosoftAccount --version 8.0.0-rc.1.23421.29
```

``` bash
az ad app list --query "[].{DisplayName:displayName, Id:id}"


az ad app create --display-name "TheDevTalkShow Web Site" --sign-in-audience "AzureADandPersonalMicrosoftAccount" --web-redirect-uris "https://localhost:7273/signin-microsoft" --required-resource-accesses "manifest.json"


az ad sp create --id bf34f11b-0fa9-49be-a54f-a63e7a9f06ee --query objectId --output tsv



{
  "appId": "a440564b-1f09-4be9-be6a-4248c29fb1d8",
  "password": "SO38Q~11RjqU45J2.FBz-2sdyajZEGoDDzOxzb0T",
  "tenant": "7339da3c-2dfd-4623-9303-fbbcfd900381"
}


## back to project
dotnet user-secrets init


manifest.json
``` json
 [
    {
    "resourceAccess": [
        {
        "id": "e1fe6dd8-ba31-4d61-89e7-88639da4683d",
        "type": "Scope"
        }
    ],
    "resourceAppId": "00000003-0000-0000-c000-000000000000"
    }
]
``````