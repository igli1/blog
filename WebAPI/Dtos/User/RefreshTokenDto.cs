namespace WebAPI.Dtos.User;

public class RefreshTokenDto
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
}