namespace BLL.Models;

public class AccessTokenResponse
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiryTime { get; set; }
}