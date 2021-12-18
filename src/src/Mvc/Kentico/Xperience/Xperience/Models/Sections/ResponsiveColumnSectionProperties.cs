using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Models.Sections
{
    public class ResponsiveColumnSectionProperties : ISectionProperties
    {
        private const string Tooltip = "Keep the sum of the two columns less than or equal to 100%";

        [EditingComponent( IntInputComponent.IDENTIFIER, Order = 0, Label = "Left Column Width (Percent)", Tooltip = Tooltip )]
        public int LeftColumnWidth { get; set; } = 60;

        [EditingComponent( IntInputComponent.IDENTIFIER, Order = 0, Label = "Right Column Width (Percent)", Tooltip = Tooltip )]
        public int RightColumnWidth { get; set; } = 40;
    }
}
