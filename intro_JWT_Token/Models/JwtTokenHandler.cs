using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace intro_JWT_Token.Models
{
	public static class JwtTokenHandler
	{
		//IConfiguration -> Setting dosyasındaki verilere erişmek için kullanılır.
		//private readonly IConfiguration _configuration;
		public static TokenModel TokenOlustur(IConfiguration configuration)
		{
			TokenModel tokenModel = new TokenModel();
			//SymmetricSecurityKey -> Token oluştururken kullanılacak key değerini belirtir.
			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtTokenParams:IssuerSigningKey"]));

			//Claim -> Token içerisine ekstra bilgiler eklemek için kullanılır.
			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			tokenModel.AccessTokenExpiration = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["JwtTokenParams:TokenExpiryInMinutes"]));

			//JwtSecurityToken -> Token oluşturmak için kullanılır.
			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				issuer: configuration["JwtTokenParams:ValidIssuer"],
				audience: configuration["JwtTokenParams:ValidAudience"],
				expires: tokenModel.AccessTokenExpiration,
				signingCredentials: signingCredentials
			);

			//JwtSecurityTokenHandler -> Token'ı oluşturur.
			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			tokenModel.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

			byte[] degerSeti = new byte[32];
			using RandomNumberGenerator randomGenerator = RandomNumberGenerator.Create();
			randomGenerator.GetBytes(degerSeti);

			tokenModel.RefreshToken = Convert.ToBase64String(degerSeti);

			return tokenModel;
		}
	}
}
