var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(); // Make sure you call this previous to AddMvc

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(
        options => options.WithOrigins("https://urban-waffle-6pgqvpq6v4v396w-5181.app.github.dev/").AllowAnyMethod()
    );


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
