using Vask_En_Tid_Library.IRepos;
using Vask_En_Tid_Library.Repos;
using Vask_En_Tid_Library.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// repos
builder.Services.AddScoped<ITenantRepo>(
    _ => new TenantRepo(builder.Configuration.GetConnectionString("Default")!)
);
builder.Services.AddScoped<IApartmentRepo>(
    _ => new ApartmentRepo(builder.Configuration.GetConnectionString("Default")!)
);
builder.Services.AddScoped<IBookingRepo>(
    _ => new BookingRepo(builder.Configuration.GetConnectionString("Default")!)
);
builder.Services.AddScoped<ITimeslotRepo>(
    _ => new TimeslotRepo(builder.Configuration.GetConnectionString("Default")!)
);
builder.Services.AddScoped<IUnitRepo>(
    _ => new UnitRepo(builder.Configuration.GetConnectionString("Default")!)
);

// services (vigtigt!)
builder.Services.AddScoped<TenantService>();
builder.Services.AddScoped<ApartmentService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<TimeslotService>();
builder.Services.AddScoped<UnitService>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();