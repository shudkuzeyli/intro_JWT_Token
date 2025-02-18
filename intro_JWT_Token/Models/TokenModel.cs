namespace intro_JWT_Token.Models
{
	public class TokenModel
	{
		public string AccessToken { get; set; }
		public DateTime AccessTokenExpiration { get; set; }
		public string RefreshToken { get; set; }
	}
}
