namespace WebAPI.Dtos.User;

public class TokenDto
{
    public RefreshTokenDto RefreshToken { get; set; }
    public string AccessToken { get; set; }
    public DateTime Expiration { get; set; }
}