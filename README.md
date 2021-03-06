# Xperience Blog Template

![NuGet Version](https://img.shields.io/nuget/v/BizStream.Xperience.BlogTemplate)
![License](https://img.shields.io/github/license/BizStream/xperience-blog-template)

The following repository contains a .NET Core CLI Template solution, intended to simplify the creation and spin-up of Kentico Xperience Mvc solutions.

## Getting Started

Before installing the template, it is recommened to have the [latest](https://dotnet.microsoft.com/download/dotnet/5.0) net5.0 SDK installed.

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
     - Ensure that `<Solution Name>.Infrastructure.Kentico.Xperience.Modules.AzureStorage` has been added to the Solution, along with `default.ruleset` and other root solution files.
     - Ensure that `CMSApp.csproj` references the `<Solution Name>.Infrastructure.Kentico.Xperience.Modules.AzureStorage` project.
     - Ensure that `CMSApp.csproj` includes and compiles `CMS\RegisterModules.cs`.

5. Import Kentico Objects

   - If the template was created successfully, your solution should now contain a `.\data` folder. This folder should contain `.zip` archives containing Kentico Object Types.
     - If these archives do not exists, execute the `.\tools\create-object-archive.ps1` within the root of the solution folder.
   - Import Objects into Kentico via the `Sites > Import site or objects` tool.
     - **NOTE**: After import, Page Types may need to be assigned to your Site.
   - The `.\data` folder may optionally be removed from the Solution.

### Media Storage

The templated solution includes an Xperience Module that configures all sites to use an Azure Storage Provider (`BlogTemplate.Infrastructure.Kentico.Xperience.Modules.AzureStorage`). By default, this module **is not registered**.

To enable it, uncomment the `RegisterModuleAttribute` line within the `RegisterModules.cs` files located at `src\Mvc\Kentico\Xperience\Xperience\RegisterModules.cs`, and `CMS\RegisterModules.cs`, and provide the relevant `CMSAzureAccountName` and `CMSAzureSharedKey` setting keys within `CMS\web.config` and `src\Mvc\App\appsettings.json`, and relevant transforms.

## Architecture

The provided solution's architecture is strongly influenced by SOLID principles, with the intention to organize, separate, and distinguish the logical couplings and dependencies within the solution.

Projects within the solution are organized in a nested-folder structure, wherein each nesting level is intended to encapsulate logical couplings/dependencies within that nesting level's "scope". Within the root of the `./src` folder, there are 3 nested folders; these folders are intended to define the "scope" (the layers) of the application:

- `Core`

  The "Core" layer of the solution is intended to define the domain-level abstractions (Domain Model), as well as other "low-level" abstractions/implementations. For example, the project within `Core/Abstractions` (`BlogTemplate.Core.Abstractions`) defines the Domain Models for a blog.

  A folder, `Core/Extensions` (`BlogTemplate.Core.Extensions`), could exists that may contain extension methods to Domain Models or System Types (such as `int`, `string`, `DateTime`). Projects within this layer should be light weight, decoupled, and avoid dependencies on specific technologies or platforms outside the solution's Domain or runtime.

- `Infrastructure`

  The "Infrastructure" layer is where we "do stuff". The "scope" of this layer is quite wide, as it is intended to contain nested "scopes" for coupled technologies/platforms (dependencies). The most important nested "scope" within this layer is `Infrastructure/Abstractions` (`BlogTemplate.Infrastructure.Abstractions`), which is intended to define contracts relevant to the solution's domain. An example of this is `IAuthorService`, a contract that defines how the `Author` domain model may be retrieved. Additional nested "scopes" within `Infrastructure` are intended to encapsulate dependencies to coupled technologies/platforms, ideally to facilitate the implementation of a contract defined within the `Infrastructure/Abstractions` scope.

  `Infrastructure/Kentico/Xperience` contains it's own `Infrastructure/Kentico/Xperience/Abstractions` (`BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions`) scope, intended for models and contracts relevant to integrating Kentico's Xperience paltform into the solution. Within the `Infrastructure/Kentico/Xperience/Xperience` (`BlogTemplate.Infrastructure.Kentico.Xperience`) scope, implementations of the domain-level infrastructure contracts that are coupled to the Kentico Xperience platform, can be found. For example, `BlogTemplate.Infrastructure.Kentico.Xperience.Services.AuthorService` implements the `IAuthorService` contract via Xperience's Content Tree (`TreeNode/Document` abstraction).

- `Mvc`

  The "Mvc" layer contains the various components that build a runnable Mvc web server. As with the "Infrastructure" layer, this layer is intended to contain nested "scopes" for coupled dependencies, but those _that are relevant to the Mvc/Web app's functionality_, it **should not** contain business rules or logic relevant to the high-level domain, as this is the intention of the "Infrastructure" layer.

  The `Mvc/Abstractions` (`BlogTemplate.Mvc.Abstractions`) scope is intended to define the Mvc-specific abstractions (View Models) and implementations. This layer is akin to `Core/Abstractions`, but for abstractions/implementations relevant to the Mvc app, rather than the high-level domain.

  The `Mvc/App` (`BlogTemplate.Mvc.App`) scope represents the runnable Mvc Site, and is intended to encapsulate the configuration and startup of the features that compose the Mvc App. Additionally, this scope is intended to contain implementations of Mvc-specific functionality that conforms to the Domain Model. For Mvc-specific functionality that is tightly-coupled to a dependency (tech/platform), it is recommended to create nested scopes to encapsulate the dependency's functionality (e.g. `BlogTemplate.Mvc.Kentico.Xperience`).

  The `Mvc/Kentico/Xperience` scope contains Mvc-specific implementations that are tightly coupled to the Kentico Xperience platform. This primarily entails features that utilize Page Builder/Form Builder functionality.
