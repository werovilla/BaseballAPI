using System.Text.Json.Serialization;
using BaseballPlayersAPI.DbModels;

namespace BaseballPlayersAPI.Models;

public class ApiResponse
{
    [JsonPropertyName("statusMessage")]
    public string StatusMessage { get; set; }
    
    [JsonPropertyName("body")]
    public Body Body { get; set; }
}

public class Body
{
    [JsonPropertyName("players")]
    public List<PlayerInfo> Players { get; set; }
}