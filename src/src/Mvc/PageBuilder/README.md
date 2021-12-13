# `BlogTemplate.Mvc.PageBuilder`

Encapsulates custom PageBuilder Components (Widgets, Sections) and any associated Static Assets.

## Features

- Custom PageBuilder Components (Widgets, Sections)

## Notes

- StaticAssets are compiled+served during `dotnet build` via [`Microsoft.AspNetCore.ClientAssets`](https://www.nuget.org/packages/Microsoft.AspNetCore.ClientAssets).
  - https://devblogs.microsoft.com/dotnet/build-client-web-assets-for-your-razor-class-library
- This Class Library is intended for PageBuilder backing "infrastructure" (Widgets, Sections).
  - It is recommended to implement PageBuilder-enabled Page Types in their logical Mvc Feature Class Library (e.g. `AboutNode` is implemented within `BlogTemplate.Mvc.About`).

