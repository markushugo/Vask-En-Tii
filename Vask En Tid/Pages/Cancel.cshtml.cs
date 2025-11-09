using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.Services;

namespace Vask_En_Tid.Pages
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    public class CancelModel : PageModel
    {
        /// <summary>
        /// The booking service
        /// </summary>
        private readonly BookingService _bookingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelModel"/> class.
        /// </summary>
        /// <param name="bookingService">The booking service.</param>
        public CancelModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Called when [post].
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IActionResult OnPost(int id)
        {
            _bookingService.DeleteBooking(id);
            return RedirectToPage("/BookingOverview");
        }
    }
}