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
        }

        [BindProperty]
        public Booking NewBooking { get; set; } = new Booking
        {
            BookingDate = DateTime.Today
        };

        public List<Timeslot> Timeslots { get; set; }
        public List<Unit> Units { get; set; }
        public List<Tenant> Tenants { get; set; }

        public void OnGet()
        {
            LoadLists();
        }

        private void LoadLists()
        {
            Timeslots = _timeslotService.GetAll();
            Units = _unitService.GetAll();
            Tenants = _tenantService.GetAllTenants();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadLists();
                return Page();
            }

            try
            {
                _bookingService.CreateBooking(NewBooking);
                return RedirectToPage("/BookingOverview");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                LoadLists();
                return Page();
            }
        }
    }
}

