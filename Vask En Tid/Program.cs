using Vask_En_Tid_Library.IRepos;
using Vask_En_Tid_Library.Repos;
using Vask_En_Tid_Library.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// hent connectionstring én gang
var cs = builder.Configuration.GetConnectionString("Default")!;

// -------------------
// Repositories
// -------------------
builder.Services.AddScoped<ITenantRepo>(_ => new TenantRepo(cs));
builder.Services.AddScoped<IApartmentRepo>(_ => new ApartmentRepo(cs));
builder.Services.AddScoped<IBookingRepo>(_ => new BookingRepo(cs));
builder.Services.AddScoped<ITimeslotRepo>(_ => new TimeslotRepo(cs));
builder.Services.AddScoped<IUnitRepo>(_ => new UnitRepo(cs));

// -------------------
// Services (forretningslag)
// -------------------
builder.Services.AddScoped<TenantService>();
builder.Services.AddScoped<ApartmentService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<TimeslotService>();
builder.Services.AddScoped<UnitService>();

var app = builder.Build();

// -------------------
// Middleware
// -------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();