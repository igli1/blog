namespace WebAPI.Dtos.User;

public class TokenDto
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiryTime { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}