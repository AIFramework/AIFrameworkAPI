using MainLogic.ML.Models.Classifiers;
using MainLogic.ML.Models.Classifiers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigureServices();

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




void ConfigureServices() 
{
    builder.Services.AddScoped<ITextCL, TextRuleClassifierAPI>();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}