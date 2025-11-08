using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.Services;

namespace Vask_En_Tid.Pages
{
    public class CancelModel : PageModel
    {
        private readonly BookingService _bookingService;

        public CancelModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

       
        public IActionResult OnPost(int id)
        {
            _bookingService.DeleteBooking(id);
            return RedirectToPage("/BookingOverview");
        }
    }
}
