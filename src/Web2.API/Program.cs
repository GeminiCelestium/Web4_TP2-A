using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using Web2.API.BusinessLogic;
using Web2.API.Extentions;
using Web2.API.Filters;
using Web2.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("TP2A_Context");
builder.Services.AddDbContext<TP2A_Context>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<ICategoryBL, CategoryBL>();
builder.Services.AddScoped<IVilleBL, VillesBL>();
builder.Services.AddScoped<IEvenementBL, EvenementBL>();
builder.Services.AddScoped<IParticipationBL, ParticipationBL>();

builder.Services.AddControllers(o =>
{
    o.AllowEmptyInputInBodyModelBinding = true;
    o.Filters.Add<HtppResponseExceptionFilter>();
})
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true)
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web2.API", Version = "v1" });
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.CreatDBIfNotExists();
app.Run();
