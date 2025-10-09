using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vask_En_Tid_Library.IRepos;
using Vask_En_Tid_Library.Models;

namespace Vask_En_Tid.Pages
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class CreateBookingModel : PageModel
    {
        private readonly IBookingRepo _repo;
        public CreateBookingModel(IBookingRepo repo) => _repo = repo;

        // Valgbar liste
        public readonly string[] MachineTypes = new[] { "Washer", "Dryer", "Roller" };

        public class Availability
        {
            public int Used { get; set; }
            public int Max { get; set; }
            public int Free => Math.Max(0, Max - Used);
            public bool IsAvailable => Used < Max;
        }

        public Dictionary<string, Availability> AvailabilityByType { get; private set; } = new();

        [BindProperty]
        public Booking NewBooking { get; set; } = new Booking
        {
            MachineType = "Washer",
            BookingDate = DateTime.Today,
            BookingTime = new TimeSpan(8, 0, 0),
            IsBooked = true // repo sætter også 1 ved succes
        };

        public List<Booking> Bookings { get; private set; } = new();

        // Bevar valg ved redirect (QueryString)
        public void OnGet(string? machineType, DateTime? date, string? time)
        {
            if (!string.IsNullOrWhiteSpace(machineType))
                NewBooking.MachineType = machineType;

            if (date.HasValue)
                NewBooking.BookingDate = date.Value.Date;

            if (!string.IsNullOrWhiteSpace(time) && TimeSpan.TryParse(time, out var ts))
                NewBooking.BookingTime = ts;

            Bookings = _repo.GetAll() ?? new List<Booking>();
            LoadAvailability();
        }

        public IActionResult OnPostRefresh()
        {
            Bookings = _repo.GetAll() ?? new List<Booking>();
            LoadAvailability();
            return Page();
        }

        public IActionResult OnPost()
        {
            // sikkerhed: validér valgt type
            if (!MachineTypes.Contains(NewBooking.MachineType, StringComparer.OrdinalIgnoreCase))
                ModelState.AddModelError(nameof(NewBooking.MachineType), "Vælg en gyldig maskinetype.");

            if (!ModelState.IsValid)
            {
                Bookings = _repo.GetAll() ?? new List<Booking>();
                LoadAvailability();
                return Page();
            }

            try
            {
                _repo.CreateBooking(NewBooking);               // kaster hvis fuldt
                TempData["Msg"] = "Booking oprettet.";
                // Bevar brugerens valg efter succes, så availability passer
                return RedirectToPage(new
                {
                    machineType = NewBooking.MachineType,
                    date = NewBooking.BookingDate.ToString("yyyy-MM-dd"),
                    time = NewBooking.BookingTime.ToString(@"hh\:mm")
                });
            }
            catch (InvalidOperationException ex)
            {
                TempData["Msg"] = ex.Message;                  // “Ingen ledige maskiner …”
                Bookings = _repo.GetAll() ?? new List<Booking>();
                LoadAvailability();
                return Page();
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Kunne ikke oprette booking: " + ex.Message;
                Bookings = _repo.GetAll() ?? new List<Booking>();
                LoadAvailability();
                return Page();
            }
        }

        public IActionResult OnPostDelete(int bookingId)
        {
            _repo.DeleteBooking(bookingId);
            TempData["Msg"] = "Booking slettet.";
            return RedirectToPage();
        }

        private void LoadAvailability()
        {
            var caps = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            { ["Washer"] = 3, ["Dryer"] = 2, ["Roller"] = 1 };

            AvailabilityByType = new();
            foreach (var type in MachineTypes)
            {
                var used = _repo.CountBookings(NewBooking.BookingDate, NewBooking.BookingTime, type);
                AvailabilityByType[type] = new Availability { Used = used, Max = caps[type] };
            }
        }
    }
}
