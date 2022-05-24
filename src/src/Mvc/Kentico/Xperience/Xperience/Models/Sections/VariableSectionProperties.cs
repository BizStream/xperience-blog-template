using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Models.Sections
{
    public class VariableSectionProperties : ISectionProperties
    {
        [EditingComponent( DropDownComponent.IDENTIFIER, Order = 0, Label = "Number of Columns" )]
        [EditingComponentProperty( nameof( DropDownProperties.DataSource ), "1;1\r\n2;2\r\n3;3\r\n4;4\r\n5;5\r\n6;6" )]
        [VisibilityCondition(nameof(Mode), ComparisonTypeEnum.IsEqualTo, "equal")]
        public string ColumnCount { get; set; } = "1";

        public string Mode { get; set; }
    }
}
