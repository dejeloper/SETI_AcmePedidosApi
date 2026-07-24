using AcmePedidosApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var soapBaseUrl = builder.Configuration["SoapService:BaseUrl"] ?? throw new InvalidOperationException("Falta configurar SoapService:BaseUrl.");

builder.Services.AddHttpClient<ISoapService, SoapService>(client =>
{
    client.BaseAddress = new Uri(soapBaseUrl);
});
builder.Services.AddSingleton<XmlService>();
builder.Services.AddSingleton<SoapMockService>();

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

app.Run();
