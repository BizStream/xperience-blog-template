/*
 * This file is intended to be used for registering Kentico Widgets, and other PageBuilder Components, required by the Mvc app.
 *
 * Example:
 *  [assembly: RegisterWidget( "My.Widget", typeof( MyWidgetViewComponent ), "My Widget", typeof( MyWidgetProperties ) )]
 */

using BlogTemplate.Mvc.Kentico.Xperience;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using BlogTemplate.Mvc.Kentico.Xperience.ViewComponents;
using Kentico.PageBuilder.Web.Mvc;

[assembly: RegisterWidget( ComponentIdentifier.TextWidget, typeof( TextWidgetViewComponent ), "Text", typeof( TextWidgetProperties ), IconClass = "icon-l-text" )]
[assembly: RegisterSection( ComponentIdentifier.TwoColumnSection, "Two Columns", customViewName: "~/Views/Shared/Sections/_TwoColumnSection.cshtml", IconClass = "icon-l-cols-2" )]
