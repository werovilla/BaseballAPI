using System.Text.Json.Serialization;

namespace BaseballPlayersAPI.Models;

public class PlayerInfo
{
    [JsonIgnore]
    public int Id { get; set; }
    
    [JsonPropertyName("id")]
    public string IdString {
        get => Id.ToString();
        set => Id = int.Parse(value);
    }
    
    [JsonPropertyName("pro_team")]
    public string? ProTeam { get; set; }
    
    [JsonPropertyName("firstname")]
    public string? FirstName { get; set; }
    
    [JsonPropertyName("lastname")]
    public string? LastName { get; set; }
    
    [JsonIgnore]
    public string FullName => $"{FirstName} {LastName}";
    
    [JsonPropertyName("bats")]
    public string? Bats { get; set; }
}