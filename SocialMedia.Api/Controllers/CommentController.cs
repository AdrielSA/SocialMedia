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
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene un comentario
        /// </summary>
        /// <param name="id">Id del comentario a obtener</param>
        /// <returns>Comentario</returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CommentDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await _commentService.GetComment(id);
            var commentDto = _mapper.Map<CommentDto>(comment);
            var response = new ApiResponse<CommentDto>(commentDto);
            return Ok(response);
        }

        /// <summary>
        /// Obtiene los comentarios de la publicación especificada
        /// </summary>
        /// <param name="filter">Filtros a aplicar</param>
        /// <returns>Lista de comentarios</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CommentDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> GetCommentsByPost([FromQuery] CommentQueryFilter filter)
        {
            var comments = await _commentService.GetCommentsByPost(filter);
            var commentDto = _mapper.Map<IEnumerable<CommentDto>>(comments);
            var response = new ApiResponse<IEnumerable<CommentDto>>(commentDto);
            return Ok(response);
        }

        /// <summary>
        /// Inserta un comentario
        /// </summary>
        /// <param name="oCommentDto">DTO del comentario a insertar</param>
        /// <returns>Comentario insertado</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CommentDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> InserComment(CommentDto oCommentDto)
        {
            var comment = _mapper.Map<Comment>(oCommentDto);
            await _commentService.InsertComment(comment);
            oCommentDto = _mapper.Map<CommentDto>(comment);
            var response = new ApiResponse<CommentDto>(oCommentDto);
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un comentario
        /// </summary>
        /// <param name="id">Id del comentario a actualizar</param>
        /// <param name="oCommentDto">Comentario a actualizar</param>
        /// <returns>Booleano representando el estado de la operacion</returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> UpdateComment(int id, CommentDto oCommentDto)
        {
            var comment = _mapper.Map<Comment>(oCommentDto);
            comment.Id = id;
            var result = await _commentService.UpdateComment(comment);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// Elimina un comentario
        /// </summary>
        /// <param name="id">Id del comentario a eliminar</param>
        /// <returns>Booleano representando el estado de la operacion</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]

        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.DeleteComment(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
