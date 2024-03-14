using CulinaryRecipes.Common;
using CulinaryRecipes.Services.Settings;
using CulinaryRecipes.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace CulinaryNotes.Api.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public bool OpenApiEnabled => settings.Enabled;

        [BindProperty]
        public string Version => Assembly.GetExecutingAssembly().GetAssemblyVersion();

        private readonly SwaggerSettings settings;

        public IndexModel(SwaggerSettings settings)
        {
            this.settings = settings;
        }
    }
}
