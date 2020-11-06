// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage( "Globalization", "CA1305:The behavior of 'string.Format(string, object, object, object)' could vary based on the current user's locale settings", Justification = "Passing an 'IFormatProvider' is not required." )]
[assembly: SuppressMessage( "Globalization", "CA1307:The behavior of 'string.LastIndexOf(string)' could vary based on the current user's locale settings", Justification = "Passing an 'IFormatProvider' is not required." )]

[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1000:The keyword 'if' should be followed by a space", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1003:Operator '!' should not be preceded by whitespace", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1008:Opening parenthesis should be spaced correctly", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1009:Closing parenthesis should be spaced correctly", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should not be followed by a space", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1011:Closing square brackets should not be preceded by a space", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1021:Negative sign should not be preceded by a space", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1111:Closing parenthesis should be on line of last parameter", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1201", Justification = "Properties should come before the ctor, and after Fields, within a region." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1202", Justification = "Members should be sorted alphabetically." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1300", Justification = "Local function declarations can be camelCase." )]
[assembly: SuppressMessage( "StyleCop.CSharp.NamingRules", "SA1313", Justification = "Members may begin with an underscore, specifically 'discards'." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1505:An opening brace should not be followed by a blank line", Justification = "Formatting is configured via .editorconfig." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1623:The property's documentation summary text should begin with: 'Gets or sets'", Justification = "Getting/Setting is assumed, prefer to describe the properties usage." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1627:The documentation text within the 'exception' tag should not be empty", Justification = "It is recommended to document exception reasons, but not required. Documenting exceptions, such as ArgumentNullException, is redundant." )]
[assembly: SuppressMessage( "StyleCop.CSharp.SpacingRules", "SA1508:A closing brace should not be preceded by a blank line", Justification = "Formatting is configured via .editorconfig." )]
