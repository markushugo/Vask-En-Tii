using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.IRepos;
using Vask_En_Tid_Library.Models;
using Microsoft.Extensions.Logging;
using Vask_En_Tid_Library;
using Microsoft.Data.SqlClient;

namespace Vask_En_Tid.Pages
{
    public class CreateApartmentModel : PageModel
    {
        private readonly IApartmentRepo _repo;
        private readonly ILogger<CreateApartmentModel> _logger;

        public CreateApartmentModel(IApartmentRepo repo, ILogger<CreateApartmentModel> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public List<Apartment> Apartments { get; private set; } = new();

        [BindProperty]
        public Apartment NewApartment { get; set; } = new();

        public void OnGet()
        {
            Apartments = _repo.GetAll();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Apartments = _repo.GetAll();
                return Page();
            }

            _repo.CreateApartment(NewApartment);
            TempData["Msg"] = "Lejlighed oprettet.";
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int apartmentId)
        {
            _repo.DeleteApartment(apartmentId);
            TempData["Msg"] = "Lejlighed slettet.";
            return RedirectToPage(); 
        }
    }
}



