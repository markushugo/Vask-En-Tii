using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.Models;
using Vask_En_Tid_Library.Services;

namespace Vask_En_Tid.Pages
{
    public class BookingsModel : PageModel
    {
        private readonly BookingService _bookingService;
        private readonly TimeslotService _timeslotService;
        private readonly UnitService _unitService;
        private readonly TenantService _tenantService;

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

            // Sikrer at NewBooking aldrig er null
            NewBooking = new Booking
            {
                BookingDate = DateTime.Today
            };

            // Sikrer at lister ikke er null ved første load
            Timeslots = new List<Timeslot>();
            Units = new List<Unit>();
            Tenants = new List<Tenant>();
        }

        [BindProperty]
        public Booking NewBooking { get; set; }

        public List<Timeslot> Timeslots { get; set; }
        public List<Unit> Units { get; set; }
        public List<Tenant> Tenants { get; set; }

        public void OnGet()
        {
            LoadLists();
        }

        public IActionResult OnPost()
        {
            LoadLists();

            if (!ModelState.IsValid)
                return Page();

            // Sikrer at datoen altid er gyldig
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

        private void LoadLists()
        {
            Timeslots = _timeslotService?.GetAll() ?? new List<Timeslot>();
            Units = _unitService?.GetAll() ?? new List<Unit>();
            Tenants = _tenantService?.GetAllTenants() ?? new List<Tenant>();
        }
    }
}
