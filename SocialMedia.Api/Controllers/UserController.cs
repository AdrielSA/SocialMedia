using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <param name="filter">Filtros a aplicar</param>
        /// <returns>LIsta de usuarios</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public IActionResult GetUsers([FromQuery] UserQueryFilter filter)
        {
            var users = _userService.GetUsers(filter);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            var response = new ApiResponse<IEnumerable<UserDto>>(usersDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un usuario
        /// </summary>
        /// <param name="id">Id del usuario a obtener</param>
        /// <returns>Usuario obtenido</returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);
            var userDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(userDto);
            return Ok(response);
        }

        /// <summary>
        /// Inserta un usuario
        /// </summary>
        /// <param name="oUserDto">DTO del usuario a insertar</param>
        /// <returns>Usuario insertado</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public async Task<IActionResult> InsertUser(UserDto oUserDto)
        {
            var user = _mapper.Map<User>(oUserDto);
            await _userService.InsertUser(user);
            oUserDto = _mapper.Map<UserDto>(user);
            var response = new ApiResponse<UserDto>(oUserDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un usuario
        /// </summary>
        /// <param name="id">Id del usuario a actualizar</param>
        /// <param name="oUserDto">DTO del usuario a actualizar</param>
        /// <returns>Booleano representando el estado de la operacion</returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public async Task<IActionResult> UpdateUser(int id, UserDto oUserDto)
        {
            var user = _mapper.Map<User>(oUserDto);
            user.Id = id;
            var result = await _userService.UpdateUser(user);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un usuario
        /// </summary>
        /// <param name="id">Id del usuario a eliminar</param>
        /// <returns>Booleano representando el estado de la operacion</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
