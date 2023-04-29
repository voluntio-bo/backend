using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Voluntio.Data;
using Voluntio.Data.Repository;
using Voluntio.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IOrganizationService, OrganizationService>();
builder.Services.AddTransient<IVoluntioRepository, VoluntioRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped(_ => {
    return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage"));
});

//entity framework config
var connectionString = builder.Configuration.GetConnectionString("VoluntioDB");
builder.Services.AddDbContext<Voluntio_DBContext>(x => x.UseNpgsql(connectionString));


//CORS configuration
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => {
        options.AllowAnyOrigin();
        options.AllowAnyMethod();
        options.AllowAnyHeader();
        //options.WithOrigins("https://ncv-application.web.app/", "http://localhost:5009/").AllowAnyHeader().AllowAnyMethod();

    });
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); //dates in postgresql

var app = builder.Build();

//CORS 
app.UseCors(options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();
