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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();