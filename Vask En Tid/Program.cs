using Vask_En_Tid_Library.IRepos;
using Vask_En_Tid_Library.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// I Development læser .NET automatisk User Secrets
builder.Services.AddScoped<ITenantRepo>(
    _ => new TenantRepo(builder.Configuration.GetConnectionString("Default")!)
);
builder.Services.AddScoped<IApartmentRepo>(
    _ => new ApartmentRepo(builder.Configuration.GetConnectionString("Default")!)
);
builder.Services.AddScoped<IBookingRepo>(
    _ => new BookingRepo(builder.Configuration.GetConnectionString("Default")!)
);



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
