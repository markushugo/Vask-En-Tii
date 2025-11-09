using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.Models;
using Vask_En_Tid_Library.Services;

namespace Vask_En_Tid.Pages
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RazorPages.PageModel" />
    public class BookingOverviewModel : PageModel
    {
        /// <summary>
        /// The booking service
        /// </summary>
        private readonly BookingService _bookingService;

        /// <summary>
        /// Gets or sets the bookings.
        /// </summary>
        /// <value>
        /// The bookings.
        /// </value>
        public List<Booking> Bookings { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingOverviewModel"/> class.
        /// </summary>
        /// <param name="bookingService">The booking service.</param>
        public BookingOverviewModel(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Called when [get].
        /// </summary>
        public void OnGet()
        {
            Bookings = _bookingService.GetAll();
        }
    }
}