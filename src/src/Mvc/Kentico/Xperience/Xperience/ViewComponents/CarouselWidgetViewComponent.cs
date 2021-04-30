using System;
using System.Threading.Tasks;
using System.Linq;
using BlogTemplate.Mvc.Kentico.Xperience.Models.Widgets;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.ViewComponents
{
    public class CarouselWidgetViewComponent : ViewComponent
    {
        #region Fields
        private IMediaFileInfoProvider mediaFileInfoProvider;
        private IPageBuilderFeature pageBuilder => HttpContext.Kentico().PageBuilder();
        #endregion

        public CarouselWidgetViewComponent( IMediaFileInfoProvider mediaFileInfoProvider )
        {
            this.mediaFileInfoProvider = mediaFileInfoProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync( ComponentViewModel<CarouselWidgetProperties> viewModel )
        {
            if( viewModel == null )
            {
                throw new ArgumentNullException( nameof( viewModel ) );
            }

            // if( pageBuilder?.EditMode == true )
            // {
            //     return View( "Edit", viewModel );
            // }

            var imageGuids = viewModel.Properties?.Images?.Select( item => item.FileGuid );
            viewModel.Properties.ImageUrls = imageGuids?.Any() != true
                ? Enumerable.Empty<string>()
                : await Task.WhenAll(
                    imageGuids.Select( imageGuid => mediaFileInfoProvider.GetAsync( imageGuid, SiteContext.CurrentSiteID ) )
                ).ContinueWith( task => task.Result.Select( MediaLibraryHelper.GetDirectUrl ) );

            return View( viewModel );
        }
    }
}
