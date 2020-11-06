/*
 * This file is a placeholder intended to be used for registering Kentico Modules required by the CMS App.
 * 
 * Example:
 *  [assembly: RegisterModule( typeof( MyCMSModule ) )]
 */

using BlogTemplate.Infrastructure.Kentico.Xperience.Modules.AzureStorage;
using CMS;

[assembly: RegisterModule( typeof( AzureStorageInitializationModule ) )]
