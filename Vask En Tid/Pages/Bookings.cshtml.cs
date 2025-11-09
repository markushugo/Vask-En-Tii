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
    public class BookingsModel : PageModel
    {
        /// <summary>
        /// The booking service
        /// </summary>
        private readonly BookingService _bookingService;
        /// <summary>
        /// The timeslot service
        /// </summary>
        private readonly TimeslotService _timeslotService;
        /// <summary>
        /// The unit service
        /// </summary>
        private readonly UnitService _unitService;
        /// <summary>
        /// The tenant service
        /// </summary>
        private readonly TenantService _tenantService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingsModel"/> class.
        /// </summary>
        /// <param name="bookingService">The booking service.</param>
        /// <param name="timeslotService">The timeslot service.</param>
        /// <param name="unitService">The unit service.</param>
        /// <param name="tenantService">The tenant service.</param>
        public BookingsModel(
            BookingService bookingService,
            TimeslotService timeslotService,
            UnitService unitService,
            TenantService tenantService)
        {
            _bookingService = bookingService;
            _timeslotService = timeslotService;
            _unitService = unitService;
            _tenantService = tenantService;

           
            NewBooking = new Booking
            {
                BookingDate = DateTime.Today
            };

            
            Timeslots = new List<Timeslot>();
            Units = new List<Unit>();
            Tenants = new List<Tenant>();
        }

        /// <summary>
        /// Creates new booking.
        /// </summary>
        /// <value>
        /// The new booking.
        /// </value>
        [BindProperty]
        public Booking NewBooking { get; set; }

        /// <summary>
        /// Gets or sets the timeslots.
        /// </summary>
        /// <value>
        /// The timeslots.
        /// </value>
        public List<Timeslot> Timeslots { get; set; }
        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>
        /// The units.
        /// </value>
        public List<Unit> Units { get; set; }
        /// <summary>
        /// Gets or sets the tenants.
        /// </summary>
        /// <value>
        /// The tenants.
        /// </value>
        public List<Tenant> Tenants { get; set; }

        /// <summary>
        /// Called when [get].
        /// </summary>
        public void OnGet()
        {
            LoadLists();
        }

        /// <summary>
        /// Called when [post].
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            LoadLists();

            if (!ModelState.IsValid)
                return Page();

            
            if (NewBooking.BookingDate < new DateTime(1753, 1, 1))
                NewBooking.BookingDate = DateTime.Today;

            try
            {
                _bookingService.CreateBooking(NewBooking);
                return RedirectToPage("/BookingOverview");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }

        /// <summary>
        /// Loads the lists.
        /// </summary>
        private void LoadLists()
        {
            Timeslots = _timeslotService?.GetAll() ?? new List<Timeslot>();
            Units = _unitService?.GetAll() ?? new List<Unit>();
            Tenants = _tenantService?.GetAllTenants() ?? new List<Tenant>();
        }
    }
}