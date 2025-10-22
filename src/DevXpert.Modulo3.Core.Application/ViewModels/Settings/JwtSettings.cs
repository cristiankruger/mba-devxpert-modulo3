namespace DevXpert.Modulo3.Core.Application.ViewModels.Settings
{
    public class JwtSettings
    {
        public const string ConfigName = "JWTSettings";

        public string Emissor { get; set; }
        public int ExpiracaoTokenMinutos { get; set; }
        public int ExpiracaoRefreshTokenMinutos { get; set; }
        public string Jwt { get; set; }
        public string[] ValidoEm { get; set; }
    }
}
