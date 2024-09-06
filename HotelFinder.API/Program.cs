using HotelFinder.Business.Abstract;
using HotelFinder.Business.Concrete;
using HotelFinder.DataAccess.Abstract;
using HotelFinder.DataAccess.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IHotelService, HotelManager>();
builder.Services.AddSingleton<IHotelRepository, HotelRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = (doc =>
    {
        doc.Info.Title = "All Hotels Api";
        doc.Info.Version = "1.013";
        doc.Info.Contact = new NSwag.OpenApiContact()
        {
            Name = "M.A. Alp Karakaya",
            Url = "https://www.linkedin.com/in/alp-krky/",
            Email = "muarkrky@gmail.com"
        };
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseAuthorization();

app.MapControllers();


app.Run();
