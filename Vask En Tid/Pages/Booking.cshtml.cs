using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.IRepos;
using Vask_En_Tid_Library.Models;
using Microsoft.Extensions.Logging;

namespace Vask_En_Tid.Pages
{
    public class BookingModel : PageModel
    {
        public class CreateBooking : PageModel
        {
            private readonly IBookingRepo _repo;
            private readonly ILogger<CreateBooking> _logger;

            public CreateBooking(IBookingRepo repo, ILogger<CreateBooking> logger)
            {
                _repo = repo;
                _logger = logger;
            }

            public List<Booking> Bookings { get; private set; } = new();

            [BindProperty]
            public Booking NewBooking { get; set; } = new();

            public void OnGet()
            {
                Bookings = _repo.GetAll();
            }

            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    Bookings = _repo.GetAll();
                    return Page();
                }

                _repo.CreateBooking(NewBooking);
                TempData["Msg"] = "Booking oprettet.";
                return RedirectToPage();
            }

            public IActionResult OnPostDelete(int bookingId)
            {
                _repo.DeleteBooking(bookingId);
                TempData["Msg"] = "Booking slettet.";
                return RedirectToPage();
            }
        }
    }
}
