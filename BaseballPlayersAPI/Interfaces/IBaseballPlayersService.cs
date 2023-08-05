using BaseballPlayersAPI.Models;

namespace BaseballPlayersAPI.Services;

public interface IBaseballPlayersService
{
    /// <summary>
    /// Adds a player to the database
    /// </summary>
    /// <param name="player"></param>
    /// <returns>A <see cref="PlayerInfo"/> object</returns>
    Task<PlayerInfo> AddPlayer(PlayerInfo player);
    
    /// <summary>
    /// Updates a player in the database
    /// </summary>
    /// <param name="player"></param>
    /// <returns>A < </returns>
    Task<PlayerInfo?> UpdatePlayer(PlayerInfo player);
    
    /// <summary>
    /// Deletes a player from the database
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True if the player was deleted, false otherwise</returns>
    Task<bool> DeletePlayer(int id);
    
    /// <summary>
    /// Get a list of players from the database
    /// </summary>
    /// <param name="bats"></param>
    /// <param name="proTeam"></param>
    /// <returns>A list of <see cref="PlayerInfo"/> objects</returns>
    Task<List<PlayerInfo>> GetPlayers(string bats ="", string proTeam ="");
    
    /// <summary>
    /// Get a player from the database by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A <see cref="PlayerInfo"/> object</returns>
    Task<PlayerInfo> GetPlayer(int id);
}