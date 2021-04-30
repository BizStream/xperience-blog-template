using System.Linq;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Converters;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.ViewComponents
{

    public class ImageWidgetViewComponent : ViewComponent
    {
        #region Fields
        private IMediaFileInfoProvider mediaFileInfoProvider;
        private IPageBuilderFeature pageBuilder => HttpContext.Kentico().PageBuilder();
        #endregion

        public ImageWidgetViewComponent( IMediaFileInfoProvider mediaFileInfoProvider )
            => this.mediaFileInfoProvider = mediaFileInfoProvider;

        public IViewComponentResult Invoke( ComponentViewModel<ImageWidgetProperties> componentViewModel)
        {
            var viewModel = new ImageWidgetViewModel();

            if (componentViewModel?.Properties?.Image?.Any() == true)
            {
                var converter = new MediaUriConverter();
                var fileInfo = mediaFileInfoProvider.Get(
                    componentViewModel.Properties.Image.First().FileGuid,
                    SiteContext.CurrentSiteID
                );
                viewModel.ImageUrl = converter.Convert(
                    MediaLibraryHelper.GetPermanentUrl(fileInfo),
                    null
                );

                viewModel.Description = componentViewModel.Properties.Description;
            }

            return View( viewModel );
        }

    }

}
