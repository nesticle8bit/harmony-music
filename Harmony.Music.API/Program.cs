using Harmony.Music.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(config =>
    {
        // content negotiation
        config.RespectBrowserAcceptHeader = true;

        /* Tells the server that if the client tries to negotiate for the media type the
           server doesn't support, it should return the 406 Not Acceptable status code */
        config.ReturnHttpNotAcceptable = true;
    })
    .AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(Harmony.Music.Presentation.AssemblyReference).Assembly);

ConfigurationManager configuration = builder.Configuration;

builder.Services.ConfigureCors();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigurePostgresContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    var path = Path.Combine("config", "appsettings.json");
    builder.Configuration.AddJsonFile(path, false, true);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("HARMONYMUSICCORS");
app.UseRouting();
app.MapControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

app.Run();