using System.Linq;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Converters;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.ViewComponents
{

    public class VideoWidgetViewComponent : ViewComponent
    {
        #region Fields
        private IMediaFileInfoProvider mediaFileInfoProvider;
        #endregion

        VideoWidgetViewComponent(IMediaFileInfoProvider mediaFileInfoProvider)
            => this.mediaFileInfoProvider = mediaFileInfoProvider;

        public IViewComponentResult Invoke( ComponentViewModel<VideoWidgetProperties> componentViewModel )
        {
            var viewModel = new VideoWidgetViewModel();

            if (componentViewModel?.Properties?.Video?.Any() == true)
            {
                var converter = new MediaUriConverter();
                var fileInfo = mediaFileInfoProvider.Get(
                    componentViewModel.Properties.Video.First().FileGuid,
                    SiteContext.CurrentSiteID
                );
                viewModel.VideoUrl = converter.Convert(
                    MediaLibraryHelper.GetPermanentUrl(fileInfo),
                    null
                );

                viewModel.Autoplay = componentViewModel.Properties.Autoplay;
                viewModel.Controls = componentViewModel.Properties.Controls;
                viewModel.Loop = componentViewModel.Properties.Loop;
            }

            return View( viewModel );
        }

    }

}
