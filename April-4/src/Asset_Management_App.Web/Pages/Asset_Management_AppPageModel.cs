using Asset_Management_App.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Asset_Management_App.Web.Pages;

public abstract class Asset_Management_AppPageModel : AbpPageModel
{
    protected Asset_Management_AppPageModel()
    {
        LocalizationResourceType = typeof(Asset_Management_AppResource);
    }
}
