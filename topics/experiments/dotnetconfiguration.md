# ASP.NET Configuration

### Documentation references

[Configuration in ASP.NET Core (ASP.NET Core 7.0)](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0) - Configuration is a key part of ASP.NET Core, starting right from File > New Project

[Configuration in .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration) - .NET has configuration, too.  There are some differences and you often have to actively opt-in to it rather than it being included in the templates.

## .NET

### Adding to a console app
Neither `File > New Project` in VS IDE nor `dotnet new` templates expose configuration for console apps.

- Add package reference to `Microsoft.Extensions.Configuration`
