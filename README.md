# Xperience Blog Template

![NuGet Version](https://img.shields.io/nuget/v/BizStream.Xperience.BlogTemplate)
![License](https://img.shields.io/github/license/BizStream/xperience-blog-template)

The following repository contains a .NET Core CLI Template solution, intended to simplify the creation and spin-up of Kentico Xperience Mvc solutions.

## Getting Started

Before installing the template, it is recommended to have the latest [net5.0](https://dotnet.microsoft.com/download/dotnet/5.0) or [net6.0](https://dotnet.microsoft.com/download/dotnet/6.0) SDK installed.

### Create a Solution

1. Install the Template

   - Via NuGet:
     - `dotnet new -i BizStream.Xperience.BlogTemplate`
   - Locally:
     - `git clone https://github.com/bizstream/xperience-blog-template`
     - `dotnet new -i .\xperience-blog-template`

2. Using the Kentico Installer, create a starting Kentico Solution.

   - Open `KenticoInstaller.exe`.
   - Select `Custom Installation`.
   - Choose _Development Model_: `ASP.NET Core`.
   - For _Installation Type_, choose a target folder to create the solution within.
     - For the _Site_ option, we recommend using the same codename to be used as a prefix for CodeName/ClassNames of other custom Kentico Objects (e.g. `BizStream`, `BZS`, `MyBlog`).
   - Continue installation of _Database_ and other _Components_, as needed.

3. Delete the templated Mvc project created by the Kentico Installer

   - Delete `WebApp.sln`, `<SiteName>.sln`, and the `.\<SiteName>` folder containing a blank Mvc project.

4. Initialize the Xperience Blog Template within the solution

   - Run `dotnet new xperience-blog -n <Solution Name> -o <Kentico Solution Folder>`
     - `<Solution Name>` will be used as the starting prefix of all generated class libraries, we recommend using the `SiteName` specified in the Kentico Installer (e.g. `dotnet new bzs-xp-blog -n MyBlog` will result in the creation of `MyBlog.sln`, `MyBlog.Core.Abstractions`, `MyBlog.Infrastracture.Abstractions`, etc).
     - `<Kentico Solution Folder>` should be the path to the parent folder containing the `CMSApp`, and other Kentico components created by the Kentico Installer.
   - Open `CMSApp.sln` to ensure the solution was configured
     - Ensure that `<Solution Name>.Infrastructure.Xperience.AzureMediaStorage` has been added to the Solution, along with `default.ruleset` and other root solution files.
     - Ensure that `CMSApp.csproj` references the `<Solution Name>.Infrastructure.Xperience.AzureMediaStorage` project.
     - Ensure that `CMSApp.csproj` includes and compiles `CMS\RegisterModules.cs`.

5. Import Kentico Objects

   - If the template was created successfully, your solution should now contain a `.\data` folder. This folder should contain `.zip` archives containing Kentico Object Types.
     - If these archives do not exists, execute the `.\tools\create-object-archive.ps1` within the root of the solution folder.
   - Import Objects into Kentico via the `Sites > Import site or objects` tool.
     - **NOTE**: After import, Page Types may need to be assigned to your Site.
   - The `.\data` folder may optionally be removed from the Solution.

### Media Storage

The templated solution includes an Xperience Module that configures all sites to use an Azure Storage Provider (`BlogTemplate.Infrastructure.Xperience.AzureMediaStorage`). By default, this module **is not registered**.

To enable it, uncomment the `RegisterModuleAttribute` line within the `RegisterModules.cs` files located at `src\Mvc\Infrastructure\Xperience\Xperience\RegisterModules.cs`, and `CMS\RegisterModules.cs`, and provide the relevant `CMSAzureAccountName` and `CMSAzureSharedKey` setting keys within `CMS\web.config` and `src\Mvc\App\appsettings.json`, and relevant transforms.

## Architecture & Philosophy

The provided solution attempts a mixed Layer and Featured based architecture, with the intention to organize, separate, and distinguish dependencies across logical groupings of functionality that compose an Mvc application.

Projects within the solution are organized in a nested-folder structure, wherein each nesting level is intended to encapsulate logical couplings/dependencies within that nesting level's "scope". Within the root of the `./src` folder, there are 2 nested folders; these folders are intended to define the "scope" (the layers) of the application:

- `Infrastructure`

  The "Infrastructure" layer is intended for encapsulation of high-level dependencies/functionality. The "scope" of this layer is quite wide, as it is intended to contain nested "scopes" for technologies/platforms (dependencies) that are relevant to the implementation of the solution application, but are not explicitly coupled to primitives/concepts of the Mvc application (e.g. a data retrieval service utilized by the Mvc application, as well a micro-service, such as an Azure Function or Cron Job).

  For example, the Kentico Xperience scope `Infrastructure/Xperience` contains it's own `Infrastructure/Xperience/Xperience/Abstractions` (`BlogTemplate.Infrastructure.Xperience.Abstractions`) scope, intended for models and contracts relevant to integrating Kentico's Xperience APIs into the solution. Within the `Infrastructure/Xperience/Xperience` (`BlogTemplate.Infrastructure.Xperience`) scope, implementations that are coupled to the Kentico Xperience platform can be found.

  For example, in an E-commerce site that integrates with a third party ERP system, an `Infrastructure/<ErpNamespaceRoot>` scope may also exist to encapsulate integration into the third party ERP system. This `Infrastructure/<ErpNamespaceRoot>` scope would ideally contain a `Infrastructure/<ErpNamespaceRoot>/Abstractions` scope that exposes various contracts for integration with the ERP, and an `Infrastructure/<ErpNamespaceRoot>/<ErpNamespaceRoot>` scope for implementations of contracts.

- `Mvc`

  The `Mvc/App` (`BlogTemplate.Mvc.App`) scope represents the runnable Mvc Site, and is intended to orchestrate the configuration and startup of the features that compose the Mvc application.

  The "Mvc" layer contains the various components, also referred to as "Mvc Feature Libraries", that compose the Mvc application. "Mvc Feature Libraries" are [_Razor Class Libraries_](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class) that function as [_Application Parts_](https://docs.microsoft.com/en-us/aspnet/core/mvc/advanced/app-parts), allowing an Mvc application to be composed of logical groupings of functionality implemented in distinct Class Libraries.

  Within the `Mvc` scope exists an `Mvc/Infrastructure` scope, this scope is intended
  to serve a similar purpose as the root `Infrastructure` scope, but for dependencies/technologies that are explicitly coupled to Mvc (more specifically, dependent upon `Microsoft.AspNetCore.*` APIs). For example, the `Mvc/Infrastructure/Xperience` scope is used to expose services that are coupled to Kentico Xperience's Mvc (`WebApp`) [package](https://www.nuget.org/packages/Kentico.Xperience.AspNetCore.WebApp). The `Mvc/Infrastructure` scope is additionally used for encapsulating code reused across various "Mvc Feature Libraries".
