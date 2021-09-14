/*
 * This file is a placeholder intended to be used for registering Kentico Modules required by the Mvc app.
 *
 * Example:
 *  [assembly: CMS.RegisterModule( typeof( MyMvcModule ) )]
 */

using BizStream.Kentico.Xperience.AspNetCore.StaticWebAssetsStorage;
using BlogTemplate.Infrastructure.Kentico.Xperience.Modules.AzureStorage;
using CMS;

[assembly: RegisterModule( typeof( AzureStorageInitializationModule ) )]
[assembly: RegisterModule( typeof( StaticWebAssetsStorageModule ) )]
