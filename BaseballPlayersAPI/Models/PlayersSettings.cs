namespace BaseballPlayersAPI.Models;

public class PlayersSettings
{
    public string PlayersJsonPath { get; set; }
    public string ConnectionString { get; set; }
    public bool SeedPlayers { get; set; }
}