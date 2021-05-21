/*
 * This file is intended to be used for registering Kentico Widgets, and other PageBuilder Components, required by the Mvc app.
 *
 * Example:
 *  [assembly: RegisterWidget( "My.Widget", typeof( MyWidgetViewComponent ), "My Widget", typeof( MyWidgetProperties ) )]
 */

using BlogTemplate.Mvc.Kentico.Xperience;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using BlogTemplate.Mvc.Kentico.Xperience.Models.Sections;
using BlogTemplate.Mvc.Kentico.Xperience.Models.Widgets;
using BlogTemplate.Mvc.Kentico.Xperience.ViewComponents;
using Kentico.PageBuilder.Web.Mvc;

[assembly: RegisterWidget( ComponentIdentifier.TextWidget, typeof( TextWidgetViewComponent ), "Text", typeof( TextWidgetProperties ), IconClass = "icon-l-text" )]
[assembly: RegisterWidget( ComponentIdentifier.CarouselWidget, typeof( CarouselWidgetViewComponent ), "Carousel", typeof( CarouselWidgetProperties ), IconClass = "icon-carousel" )]
[assembly: RegisterSection( ComponentIdentifier.TwoColumnSection, "Two Columns", customViewName: "~/Views/Shared/Sections/_TwoColumnSection.cshtml", IconClass = "icon-l-cols-2" )]
[assembly: RegisterSection( ComponentIdentifier.GenericColumnSection, "Generic Column Section", typeof( GenericColumnSectionProperties ), customViewName: "~/Views/Shared/Sections/_GenericColumnSection.cshtml", IconClass = "icon-l-cols-3" )]
