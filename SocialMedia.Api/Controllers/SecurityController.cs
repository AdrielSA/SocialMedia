using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public SecurityController(
            ISecurityService securityService, 
            IMapper mapper, 
            IPasswordService passwordService)
        {
            _mapper = mapper;
            _securityService = securityService;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Inserta un usuario (seguridad del API)
        /// </summary>
        /// <param name="oSecurityDto">DTO del usuario a insertar</param>
        /// <returns>Usuario insertado</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SecurityDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public async Task<IActionResult> RegisterUser(SecurityDto oSecurityDto)
        {
            var security = _mapper.Map<Security>(oSecurityDto);
            security.Password = _passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);
            oSecurityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(oSecurityDto);
            return Ok(response);
        }
    }
}
