using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.Models;
using Vask_En_Tid_Library.Services;
using System.Collections.Generic;


namespace Vask_En_Tid.Pages
{
    public class TenantsModel : PageModel
    {
        private readonly TenantService _tenantService;
        private readonly ApartmentService _apartmentService;

        public TenantsModel(TenantService tenantService, ApartmentService apartmentService)
        {
            _tenantService = tenantService;
            _apartmentService = apartmentService;
        }

        [BindProperty]
        public Tenant Tenant { get; set; }

        public List<Apartment> Apartments { get; set; }

        public void OnGet()
        {
            Apartments = _apartmentService.GetAll();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Apartments = _apartmentService.GetAll();
                return Page();
            }

            try
            {
                _tenantService.RegisterTenant(Tenant);
                return RedirectToPage("/Index");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                Apartments = _apartmentService.GetAll();
                return Page();
            }
        }
    }
}
