# Internship at iTechArt
****
## NuGet Packages
To create your own NuGet package you need:
### 1. Configure project file for package generation
Add the following properties to your `.csproj` file:
```xml
<PropertyGroup>
  <GeneratePackageOnBuild>true</GeneratePackageOnBuild>
  <PackageId>CommunityHub.Contract.Package</PackageId>
  <Version>1.0.0</Version>
  <PackageOutputPath>../../packages</PackageOutputPath>
</PropertyGroup>
```
### 2. Adding local NuGet source for your project
```bash
dotnet nuget add source "<path_to_packeges_folder>" -n "<NameLocalSource>"
```
### 3. Reference the package in your project
```xml
<PackageReference Include="CommunityHub.Contract.Package" Version="1.0.0" />
```
To support versioning, change the version in the `.csproj` file to another one:
```xml
<Version>1.0.1</Version>
```
And build the project
