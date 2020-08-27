using BlogTemplate.Mvc.Xperience.Models;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Xperience.ViewComponents
{

    public class TextWidgetViewComponent : ViewComponent
    {
        #region Fields
        private IPageBuilderFeature pageBuilder => HttpContext.Kentico().PageBuilder();
        #endregion

        public IViewComponentResult Invoke( ComponentViewModel<TextWidgetProperties> viewModel )
        {
            if( pageBuilder?.EditMode == true )
            {
                return View( "Edit", viewModel );
            }

            return View( viewModel );
        }

    }

}
