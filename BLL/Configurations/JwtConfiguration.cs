namespace BLL.Configurations;

public class JwtConfiguration
{
    public static string SectionName = "JwtConfiguration";
    public string Key { get; set; }
    public int TokenValidityInMinutes { get; set; }
    public int RefreshTokenValidityInDays { get; set; }
}