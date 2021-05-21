using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var imageUrls = new List<string>();
            var imageGuids = viewModel.Properties?.Images?.Select( item => item.FileGuid );

            if( imageGuids?.Any() == true )
            {
                foreach( var imageGuid in imageGuids )
                {
                    imageUrls.Add(
                      await mediaFileInfoProvider.GetAsync( imageGuid, SiteContext.CurrentSiteID )
                        .ContinueWith( task => MediaLibraryHelper.GetDirectUrl( task.Result ) )
                    );
                }
            }
            viewModel.Properties.ImageUrls = imageUrls;

            return View( viewModel );
        }
    }
}
