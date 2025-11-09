using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.Models;
using Vask_En_Tid_Library.Services;

namespace Vask_En_Tid.Pages
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    public class TenantsModel : PageModel
    {
        /// <summary>
        /// The tenant service
        /// </summary>
        private readonly TenantService _tenantService;
        /// <summary>
        /// The apartment service
        /// </summary>
        private readonly ApartmentService _apartmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantsModel"/> class.
        /// </summary>
        /// <param name="tenantService">The tenant service.</param>
        /// <param name="apartmentService">The apartment service.</param>
        public TenantsModel(TenantService tenantService, ApartmentService apartmentService)
        {
            _tenantService = tenantService;
            _apartmentService = apartmentService;
        }

        /// <summary>
        /// Gets or sets the tenant.
        /// </summary>
        /// <value>
        /// The tenant.
        /// </value>
        [BindProperty]
        public Tenant Tenant { get; set; }

        /// <summary>
        /// Gets or sets the apartments.
        /// </summary>
        /// <value>
        /// The apartments.
        /// </value>
        public List<Apartment> Apartments { get; set; }

        /// <summary>
        /// Called when [get].
        /// </summary>
        public void OnGet()
        {
            Apartments = _apartmentService.GetAll();
        }

        /// <summary>
        /// Called when [post].
        /// </summary>
        /// <returns></returns>
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