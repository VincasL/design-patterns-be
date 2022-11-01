using BattleshipsApi;
using BattleshipsApi.Hubs;
using AutoMapper;
using BattleshipsApi.Facades;
using BattleshipsApi.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient("HttpClientWithSSLUntrusted").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    ServerCertificateCustomValidationCallback =
        (_, _, _, _) => true
});

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            policy.WithOrigins("http://localhost:4200");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        });
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSignalRSwaggerGen();
});

builder.Services.AddSingleton<QueueHandler>();
builder.Services.AddTransient<GameLogicHandler>();
builder.Services.AddTransient<GameDataSender>();
builder.Services.AddSingleton<BattleshipsFacade>();

builder.Services.AddSignalR(c =>
{
    c.EnableDetailedErrors = true;
    c.ClientTimeoutInterval = TimeSpan.FromSeconds(300);
    c.KeepAliveInterval = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();
app.MapHub<BattleshipHub>("/api/battleship");

app.Run();

