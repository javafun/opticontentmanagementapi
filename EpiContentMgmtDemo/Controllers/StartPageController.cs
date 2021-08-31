using EpiContentMgmtDemo.Models.Pages;
using EpiContentMgmtDemo.Models.ViewModels;
using EPiServer;
using EPiServer.Web;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace EpiContentMgmtDemo.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        private readonly IContentLoader _contentLoader;

        public StartPageController(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }
        public ActionResult Index(StartPage currentPage)
        {
            var model = PageViewModel.Create(currentPage);

            if (SiteDefinition.Current.StartPage.CompareToIgnoreWorkID(currentPage.ContentLink)) // Check if it is the StartPage or just a page of the StartPage type.
            {
                //Connect the view models logotype property to the start page's to make it editable
                var editHints = ViewData.GetEditHints<PageViewModel<StartPage>, StartPage>();
                editHints.AddConnection(m => m.Layout.Logotype, p => p.SiteLogotype);
                editHints.AddConnection(m => m.Layout.ProductPages, p => p.ProductPageLinks);
                editHints.AddConnection(m => m.Layout.CompanyInformationPages, p => p.CompanyInformationPageLinks);
                editHints.AddConnection(m => m.Layout.NewsPages, p => p.NewsPageLinks);
                editHints.AddConnection(m => m.Layout.CustomerZonePages, p => p.CustomerZonePageLinks);
            }
            var pages = _contentLoader.GetChildren<SitePageData>(currentPage.ContentLink);
            var unpublishedPage = _contentLoader.Get<SitePageData>(currentPage.GlobalNewsPageLink);
            return View(model);

        }

    }
}
