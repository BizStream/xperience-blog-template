/*
 * This file is intended to be used for registering Kentico Widgets, and other PageBuilder Components, required by the Mvc app.
 *
 * Example:
 *  [assembly: RegisterWidget( "My.Widget", typeof( MyWidgetViewComponent ), "My Widget", typeof( MyWidgetProperties ) )]
 */

using BlogTemplate.Mvc.PageBuilder;
using BlogTemplate.Mvc.PageBuilder.Abstractions;

[assembly: RegisterSection( ComponentIdentifier.TwoColumnSection, "Two Columns", customViewName: "~/Views/Shared/Sections/_TwoColumnSection.cshtml", IconClass = "icon-l-cols-2" )]
[assembly: RegisterSection( ComponentIdentifier.GenericColumnSection, "Generic Column Section", typeof( GenericColumnSectionProperties ), customViewName: "~/Views/Shared/Sections/_GenericColumnSection.cshtml", IconClass = "icon-l-cols-3" )]
