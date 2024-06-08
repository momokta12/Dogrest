using DogLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
options.AddPolicy(name: "AllowAll",
                        policy =>
                        {
                            policy.AllowAnyOrigin()
                            .AllowAnyMethod().
                            AllowAnyHeader();
                        }));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DogsRepository>(new DogsRepository()); 

// her tilføjer vi vores repository til vores services collection
// det betyder at vi kan injecte vores repository i vores controller
//new dogsRepository() er en singleton, hvilket betyder at der kun er en instans af klassen i hele applikationen
// det er en god ide at bruge singleton til repositories, da vi kun skal have en instans af vores repository i hele applikationen
// og vi vil gerne have at alle controllers bruger den samme instans af vores repository

//(new dogsRepository()) er en instans af vores repository, som vi tilføjer til services collection

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
