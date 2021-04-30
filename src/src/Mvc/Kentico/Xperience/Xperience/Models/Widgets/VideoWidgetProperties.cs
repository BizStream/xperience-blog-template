using System.Collections.Generic;
using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Models
{

    public class VideoWidgetProperties : IWidgetProperties
    {

        private const string AllowedExtensions = ".mp4;.m4v;.webm";

        [EditingComponent( MediaFilesSelector.IDENTIFIER, Order = 0, Label = "Video" )]
        [EditingComponentProperty( nameof( MediaFilesSelectorProperties.MaxFilesLimit ), 1 )]
        [EditingComponentProperty( nameof( MediaFilesSelectorProperties.AllowedExtensions ), AllowedExtensions )]
        public IList<MediaFilesSelectorItem> Video { get; set; }

        [EditingComponent( CheckBoxComponent.IDENTIFIER, Order = 1, Label = "Autoplay" )]
        public bool Autoplay { get; set; } = false;

        [EditingComponent( CheckBoxComponent.IDENTIFIER, Order = 2, Label = "Controls" )]
        public bool Controls { get; set; } = false;

        [EditingComponent( CheckBoxComponent.IDENTIFIER, Order = 3, Label = "Loop" )]
        public bool Loop { get; set; } = false;

        [EditingComponent( CheckBoxComponent.IDENTIFIER, Order = 4, Label = "Muted" )]
        public bool Muted { get; set; } = false;

    }

}
