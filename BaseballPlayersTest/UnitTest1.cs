using BaseballPlayersAPI;
using BaseballPlayersAPI.DbModels;
using BaseballPlayersAPI.Models;
using BaseballPlayersAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BaseballPlayersTest;

public class Tests
{
    private IBaseballPlayersService _service;
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
        
        var builder = WebApplication.CreateBuilder();
        builder.Configuration.AddConfiguration(configuration);
        builder.Services.AddPooledDbContextFactory<BaseballPlayersContext>(options => {
            options.UseSqlite("Data Source=BaseballPlayers.db");});

        builder.Services.AddAutoMapper(typeof(MapProfile));
        builder.Services.AddSingleton<IBaseballPlayersService, BaseballPlayersService>();

        var app = builder.Build();
        
        _service = app.Services.GetService<IBaseballPlayersService>();
        var dbContext = app.Services.GetService<IDbContextFactory<BaseballPlayersContext>>();
        var context = dbContext.CreateDbContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }

    [Test]
    public async Task AddGetUpdateDelete()
    {
        var newPlayer = new PlayerInfo
        {
            Id = 0,
            FirstName = "Test",
            LastName = "Test",
            Bats = "L",
            ProTeam = "TEST"
        };
        
        //Add the player to the database or memory
        var result = await _service.AddPlayer(newPlayer);
        Assert.That(result.Id, Is.GreaterThan(0));
        Assert.That(result.FirstName, Is.EqualTo(newPlayer.FirstName));
        Assert.That(result.LastName, Is.EqualTo(newPlayer.LastName));
        Assert.That(result.Bats, Is.EqualTo(newPlayer.Bats));
        Assert.That(result.ProTeam, Is.EqualTo(newPlayer.ProTeam));

        //Ensures it got saved to the database or memory
        var getPlayer = await _service.GetPlayer(result.Id);
        Assert.IsTrue(AreObjectsEqual(getPlayer, result));
        
        var updatePlayer = new PlayerInfo
        {
            Id = result.Id,
            FirstName = "Test2",
            LastName = "Test2",
            Bats = "R",
            ProTeam = "TEST2"
        };
        
        //Update the player in the database or memory
        var updateResult = await _service.UpdatePlayer(updatePlayer);
        Assert.That(updateResult.Id, Is.EqualTo(result.Id));
        Assert.That(updateResult.FirstName, Is.EqualTo(updatePlayer.FirstName));
        Assert.That(updateResult.LastName, Is.EqualTo(updatePlayer.LastName));
        Assert.That(updateResult.Bats, Is.EqualTo(updatePlayer.Bats));
        Assert.That(updateResult.ProTeam, Is.EqualTo(updatePlayer.ProTeam));
        
        //Ensures it got saved to the database or memory
        var getUpdatedPlayer = await _service.GetPlayer(result.Id);
        Assert.IsTrue(AreObjectsEqual(getUpdatedPlayer, updateResult));

        //Delete the player from the database or memory
        var deleteResult = await _service.DeletePlayer(result.Id);
        Assert.That(deleteResult, Is.True);
        
        //Ensures it got deleted from the database or memory
        var deletedPlayer = await _service.GetPlayer(result.Id);
        Assert.That(deletedPlayer.Id, Is.EqualTo(0));
    }
    
    private bool AreObjectsEqual(PlayerInfo expected, PlayerInfo actual)
    {
        if (expected == null || actual == null)
        {
            return false;
        }

        var properties = typeof(PlayerInfo).GetProperties();

        foreach (var prop in properties)
        {
            var expectedValue = prop.GetValue(expected);
            var actualValue = prop.GetValue(actual);

            if (!Equals(expectedValue, actualValue))
            {
                return false;
            }
        }

        return true;
    }
}