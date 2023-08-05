using System.ComponentModel.DataAnnotations;

namespace BaseballPlayersAPI.DbModels;

public class PlayerInfoDb
{
    [Key]
    public int Id { get; set; }
    public string? ProTeam { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bats { get; set; }
}