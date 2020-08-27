/*
 * This file is intended to be used for registering Kentico Modules that are "shared" by both the CMS and Mvc Apps.
 *
 * Example:
 *  [assembly: RegisterModule( typeof( MySharedModule ) )]
 */

using BlogTemplate.Infrastructure.Xperience.CMSIntegration.Modules;
using CMS;

[assembly: RegisterModule( typeof( AzureStorageInitializationModule ) )]
