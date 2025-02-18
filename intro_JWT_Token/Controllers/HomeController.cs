﻿using Microsoft.AspNetCore.Mvc;

namespace intro_JWT_Token.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly IConfiguration _configuration;

		public HomeController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public IActionResult GetJwtToken()
		{
			TokenModel jwtTokenModel = JwtTokenHandler.TokenOlustur(_configuration);
			return Ok();
		}
	}
}
