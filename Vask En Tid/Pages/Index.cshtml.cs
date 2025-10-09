using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.IRepos;
using Vask_En_Tid_Library.Models;
using Microsoft.Extensions.Logging;
using Vask_En_Tid_Library;
using Microsoft.Data.SqlClient;
namespace Vask_En_Tid.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITenantRepo _repo;
        private readonly ILogger<IndexModel> _logger;

        public List<Tenant> Tenants { get; private set; } = new();
        [BindProperty] public Tenant NewTenant { get; set; } = new();

        public IndexModel(ITenantRepo repo, ILogger<IndexModel> logger)   // ← én konstruktør
        {
            _repo = repo;
            _logger = logger;
        }

        public void OnGet()
        {
            Tenants = _repo.GetAll();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Tenants = _repo.GetAll();
                return Page();
            }

            _repo.CreateTenant(NewTenant);
            return RedirectToPage();
        }
        public IActionResult OnPostDelete(int tenantId)
        {
            _repo.DeleteTenant(tenantId);
            TempData["Msg"] = "Tenant slettet.";
            return RedirectToPage();
        }
    }
}
