using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTOs;

public class UpdateUser
{
    [Required(ErrorMessage = "User name is required")]
    public string userName { get; set; }

    // [Required(ErrorMessage = "Password name is required")]
    // public string Password { get; set; }
    
    [Required(ErrorMessage = "Password name is required")]
    public bool IsWin { get; set; }
}