using System.Text.Json;
using AutoMapper;
using BaseballPlayersAPI.DbModels;
using BaseballPlayersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseballPlayersAPI.Services;

public class BaseballPlayersService : IBaseballPlayersService
{
    private readonly IDbContextFactory<BaseballPlayersContext> _dbContext;
    private readonly IMapper _mapper;
    private readonly PlayersSettings? _playersSettings;

    public BaseballPlayersService(IConfiguration configuration, IDbContextFactory<BaseballPlayersContext> dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;

        _playersSettings = configuration.GetSection("PlayersSettings").Get<PlayersSettings>();
        if(_playersSettings == null)
        {
            throw new Exception("PlayersSettings not found in appsettings.json");
        }
        if(_playersSettings.SeedPlayers)
            SeedData();
    }

    /// <summary>
    /// Seeds data from the json file to the sqlite database
    /// </summary>
    /// <exception cref="Exception"></exception>
    private void SeedData()
    {
        var playersJson = File.ReadAllText(_playersSettings.PlayersJsonPath);
        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(playersJson);
        if(apiResponse == null)
        {
            throw new Exception($"No players found on the file");
        }
        var players = apiResponse.Body.Players;
        if(players == null)
        {
            throw new Exception($"File not found {_playersSettings.PlayersJsonPath}");
        }
        var context = _dbContext.CreateDbContext(); 
        context.Database.EnsureCreated();
        
        if (!context.Players.Any())
        {
            context.Players.AddRange(_mapper.Map<List<PlayerInfoDb>>(players));
            context.SaveChanges();
        }
    }
    
    public async Task<PlayerInfo> AddPlayer(PlayerInfo player)
    {
        await using var context = await _dbContext.CreateDbContextAsync();
        var playerDb = _mapper.Map<PlayerInfoDb>(player);
        context.Players.Add(playerDb);
        await context.SaveChangesAsync();
        return _mapper.Map<PlayerInfo>(playerDb);
    }
    
    public async Task<PlayerInfo?> UpdatePlayer(PlayerInfo player)
    {
        await using var context = await _dbContext.CreateDbContextAsync();
        var playerDb = await context.Players.FirstOrDefaultAsync(p => p.Id == player.Id);
        
        if (playerDb == null)
        {
            return null;
        }
        playerDb = _mapper.Map(player, playerDb);
        
        context.Players.Update(playerDb);
        await context.SaveChangesAsync();
        return player;
    }
    
    public async Task<bool> DeletePlayer(int id)
    {
        await using var context = await _dbContext.CreateDbContextAsync();
        var player = await context.Players.FirstOrDefaultAsync(p => p.Id == id);
        if (player == null)
        {
            return false;
        }
        context.Players.Remove(player);
        var result = await context.SaveChangesAsync();
        return result > 0;
    }
    
    public async Task<List<PlayerInfo>> GetPlayers(string bats ="", string proTeam ="")
    {
        await using var context = await _dbContext.CreateDbContextAsync();
        var players = context.Players.AsQueryable();
        if (bats != "")
        {
            players = players.Where(p=>p.Bats== bats);
        }

        if (proTeam != "")
        {
            players = players.Where(p=>p.ProTeam== proTeam);
        }

        var result = await players.ToListAsync();
        
        return _mapper.Map<List<PlayerInfo>>(result);
    }
    
    public async Task<PlayerInfo> GetPlayer(int id)
    {
        await using var context = await _dbContext.CreateDbContextAsync();
        var player = await context.Players.FirstOrDefaultAsync(p => p.Id == id) ?? new PlayerInfoDb();
        return _mapper.Map<PlayerInfo>(player);
    }
}