# Quick guide to deploying a new web app to Azure Web Apps
This guide walks through how to:
- Create a new web app with dotnet CLI
    - Extra: Create in Visual Studio
- Deploy the app to docker hub in a container
    - Extra: Deploy the app to an Azure Container Registry
- Deploy the app to Azure Web Apps
    - Extra: Deploy the app to Azure Container Instances
    - Extra: Deploy the app to Azure Container Apps
- Add a custom domain to the app
- Allow HTTPS traffic to the app with a certificate from LetsEncrypt
    - Extra: Allow HTTPS traffic to the app with built-in certificate support in Azure Web Apps

## Create a new web app with dotnet CLI
1. Create a new web app with dotnet CLI
    Let's make this a .NET 7 Blazor Server app.
    ``` powershell
    # create the app called Blazor7App in a Blazor7App subfolder
    dotnet new blazorserver -o Blazor7App -n Blazor7App -f net7.0

    # note the version you ended up with
    type .\Blazor7App\Blazor7App.csproj
    ```

1. Run the app locally
    ```bash
    cd Blazor7App
    dotnet run

    # can test local https dev support with
    dotnet run --urls "http://localhost:5100;https://localhost:5101"



    ```
    For now don't worry that this opens up the app as http only. You should still be able to reach the app and see it running.

1. Add a Dockerfile to the project folder (in this case it will be in the Blazor7App folder alongside the .sln and .csproj file).
    
    - Add a .dockerignore file to remove unndeded files
        ```
        **/.classpath
        **/.dockerignore
        **/.env
        **/.git
        **/.gitignore
        **/.project
        **/.settings
        **/.toolstarget
        **/.vs
        **/.vscode
        **/*.*proj.user
        **/*.dbmdl
        **/*.jfm
        **/azds.yaml
        **/bin
        **/charts
        **/docker-compose*
        **/Dockerfile*
        **/node_modules
        **/npm-debug.log
        **/obj
        **/secrets.dev.yaml
        **/values.dev.yaml
        LICENSE
        README.md
        ```

    ```docker
    # candidate
    FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
    WORKDIR /app
    EXPOSE 80
    EXPOSE 443

    FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
    WORKDIR /src
    COPY . .
    RUN dotnet restore
    RUN dotnet build "Blazor7App.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "Blazor7App.csproj" -c Release -o /app/publish /p:UseAppHost=false

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "Blazor7App.dll"]
    ```


    ``` docker
    FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
    WORKDIR /app
    EXPOSE 80
    EXPOSE 443

    FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
    WORKDIR /src
    COPY ["Blazor7App/Blazor7App.csproj", "Blazor7App/"]
    RUN dotnet restore "Blazor7App/Blazor7App.csproj"
    COPY . .
    WORKDIR "/src/Blazor7App"
    RUN dotnet build "Blazor7App.csproj" -c Release -o /app/build

    FROM build AS publish
    RUN dotnet publish "Blazor7App.csproj" -c Release -o /app/publish /p:UseAppHost=false

    FROM base AS final
    WORKDIR /app
    COPY --from=publish /app/publish .
    ENTRYPOINT ["dotnet", "Blazor7App.dll"]    
    ```

1. Now build the image and tag it in prepartion for push to Dockerhub
    ``` bash
    # use your repository name and tag
    docker build -t spaceshot/blazor7app:1.0.0 .

    # use your repository name and tag
    docker push spaceshot/blazor7app:1.0.0
    ```

1. Create the Azure Web App
    ``` bash
    # create the resource group
    az group create --resource-group test-blazorapp-rg --location eastus

    az appservice plan create --name test-blazorapp-asp --resource-group test-blazorapp-rg --is-linux --no-wait --sku B1

    # check status
    az appservice plan list --query "[].{Name:name, Status:status}"

    # create the web app
    az webapp create --name test-blazorapp --plan test-blazorapp-asp --resource-group test-blazorapp-rg --deployment-container-image-name docker.io/spaceshot/blazorapp:1.0.4 --https-only true

    # to see status
    # --query "{name:name, state:state} 
    ```