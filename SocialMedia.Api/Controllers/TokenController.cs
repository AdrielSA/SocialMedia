using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public TokenController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene el token de seguridad
        /// </summary>
        /// <param name="userLoginDto">DTO del usuario que solicita el token</param>
        /// <returns>Bearer token</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public async Task<IActionResult> Authentication(UserLoginDto userLoginDto)
        {
            var userLogin = _mapper.Map<UserLogin>(userLoginDto);
            var validation = await _authService.IsValidUser(userLogin);
            if (validation.Item1)
            {
                var token = _authService.GenerateToken(validation.Item2);
                return Ok(new { token });
            }
            return NotFound();
        }
    }
}
