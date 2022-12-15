using BattleshipsApi;
using BattleshipsApi.Hubs;
using AutoMapper;
using BattleshipsApi.Facades;
using BattleshipsApi.Handlers;
using BattleshipsApi.Hubs.Handlers;
using BattleshipsApi.Mediator;


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
builder.Services.AddSingleton<BattleshipsMediator>();

builder.Services.AddTransient<JoinQueueHandler>();
builder.Services.AddTransient<StartGameHandler>();
builder.Services.AddTransient<SendGameDataHandler>();
builder.Services.AddTransient<AssignNewConnectionIdHandler>();
builder.Services.AddTransient<MakeMoveHandler>();
builder.Services.AddTransient<MoveUnitHandler>();
builder.Services.AddTransient<PlaceShipHandler>();
builder.Services.AddTransient<PlaceMineHandler>();
builder.Services.AddTransient<PlaceShipsHandler>();
builder.Services.AddTransient<RotateShipHandler>();
builder.Services.AddTransient<UndoPlaceShipHandler>();
builder.Services.AddTransient<AddComponentToShipHandler>();


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

