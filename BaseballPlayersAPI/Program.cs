using BaseballPlayersAPI;
using BaseballPlayersAPI.DbModels;
using BaseballPlayersAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<BaseballPlayersContext>(options => {
    options.UseSqlite("Data Source=BaseballPlayers.db");
});

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddSingleton<IBaseballPlayersService, BaseballPlayersService>();
builder.Services.AddControllers();


var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());
var startService = app.Services.GetService<IBaseballPlayersService>();
app.Run();
