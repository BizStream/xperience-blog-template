using System.Collections.Generic;
using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Models
{

    public class ImageWidgetProperties : IWidgetProperties
    {

        private const string AllowedExtensions = ".png;.jpg;.jpeg;.bmp;.svg";

        [EditingComponent( MediaFilesSelector.IDENTIFIER, Order = 0, Label = "Image" )]
        [EditingComponentProperty( nameof( MediaFilesSelectorProperties.MaxFilesLimit ), 1 )]
        [EditingComponentProperty( nameof( MediaFilesSelectorProperties.AllowedExtensions ), AllowedExtensions )]
        public IList<MediaFilesSelectorItem> Image { get; set; }

        [EditingComponent( TextInputComponent.IDENTIFIER, Order = 1, Label = "Description" )]
        public string Description { get; set; }

    }

}
