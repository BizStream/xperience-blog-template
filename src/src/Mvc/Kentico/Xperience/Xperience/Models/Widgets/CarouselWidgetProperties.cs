using System;
using System.Collections.Generic;
using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Models.Widgets
{

    public class CarouselWidgetProperties : IWidgetProperties
    {

        [EditingComponent( MediaFilesSelector.IDENTIFIER )]
        [EditingComponentProperty( nameof( MediaFilesSelectorProperties.MaxFilesLimit ), 0 )]
        public IList<MediaFilesSelectorItem> Images { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

    }

}
