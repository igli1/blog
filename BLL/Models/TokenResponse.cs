namespace BLL.Models;

public class TokenResponse
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiryTime { get; set; }
}