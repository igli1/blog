namespace BLL.Models;

public class RefreshTokenResponse
{
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}