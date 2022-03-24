# Issue with [Anotar](https://github.com/Fody/Anotar)

When configuring [Serilog](https://github.com/serilog/serilog) with [IConfiguration](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration?view=dotnet-plat-ext-6.0) an exception like this will be thrown:

```text
Unhandled exception. System.IO.FileNotFoundException: Could not load file or assembly 'Anotar.Serilog, Culture=neutral, PublicKeyToken=null'. Das System kann die angegebene Datei nicht finden.
File name: 'Anotar.Serilog, Culture=neutral, PublicKeyToken=null'
   at System.Reflection.RuntimeAssembly.InternalLoad(ObjectHandleOnStack assemblyName, ObjectHandleOnStack requestingAssembly, StackCrawlMarkHandle stackMark, Boolean throwOnFileNotFound, ObjectHandleOnStack assemblyLoadContext, ObjectHandleOnStack retAssembly)
   at System.Reflection.RuntimeAssembly.InternalLoad(AssemblyName assemblyName, RuntimeAssembly requestingAssembly, StackCrawlMark& stackMark, Boolean throwOnFileNotFound, AssemblyLoadContext assemblyLoadContext)
   at System.Reflection.Assembly.Load(AssemblyName assemblyRef)
   at Serilog.Settings.Configuration.ConfigurationReader.LoadConfigurationAssemblies(IConfigurationSection section, AssemblyFinder assemblyFinder)
   at Serilog.Settings.Configuration.ConfigurationReader..ctor(IConfigurationSection configSection, AssemblyFinder assemblyFinder, IConfiguration configuration)
   at Serilog.ConfigurationLoggerConfigurationExtensions.Configuration(LoggerSettingsConfiguration settingConfiguration, IConfiguration configuration, String sectionName, DependencyContext dependencyContext)
   at Serilog.ConfigurationLoggerConfigurationExtensions.Configuration(LoggerSettingsConfiguration settingConfiguration, IConfiguration configuration, DependencyContext dependencyContext)
   at Program.<Main>$(String[] args) in #ProjectPath#\Program.cs:line 8
```

For some reason it wants to resolve a reference to the Assembly `Anotar.Serilog`.

## Involved Packages

| Package                                                                                                       | Version |
|---------------------------------------------------------------------------------------------------------------|---------|
| [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/6.0.0) | 6.0.0   |
| [Serilog](https://www.nuget.org/packages/Serilog/2.10.0)                                                      | 2.10.0  |
| [Serilog.Settings.Configuration](https://www.nuget.org/packages/Serilog.Settings.Configuration/3.3.0)         | 3.3.0   |
| [Anotar.Serilog.Fody](https://www.nuget.org/packages/Anotar.Serilog.Fody/6.0.0)                               | 6.0.0   |
| [Fody](https://www.nuget.org/packages/Fody/6.6.0)                                                             | 6.6.0   |

## Example

### `Program.cs`

```c#
using Microsoft.Extensions.Configuration;
using Serilog;

// Create empty configuration
var config = new ConfigurationBuilder().Build();

// Initialize Logging
new LoggerConfiguration()
    .ReadFrom.Configuration(config); // This throws the Exception
```

### `csproj`

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.Configuration" Version="6.0.0" />

    <PackageReference Include="Serilog" Version="2.10.0" />
    <PackageReference Include="Serilog.Settings.Configuration" Version="3.3.0" />

    <PackageReference Include="Anotar.Serilog.Fody" Version="6.0.0">
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="Fody" Version="6.6.0">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>
</Project>
```

### `FodyWeavers.xml`

```xml
<Weavers xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:noNamespaceSchemaLocation="FodyWeavers.xsd">
  <Anotar.Serilog />
</Weavers>
```