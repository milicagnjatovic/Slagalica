using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int NumberOfPlayedGames { get; set; } = 0;
    public int NumberOfWonGames { get; set; } = 0;
    
    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}