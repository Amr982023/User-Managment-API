using Application_Layer.DTOs;
using Application_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace User_Managment_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        // Register
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RegisterUserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                var result = await _userService.RegisterAsync(dto);
                if (result.Isfailure)
                    return BadRequest(result._ErrorMessage);

                var userDto = result._Value;
                return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // Login
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _userService.LoginAsync(dto);

            if (token == null)
                return Unauthorized("Invalid username or password");

            return Ok(new {token});
        }


        // Get by Id
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        // Get All
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }


        // Update
        [HttpPut("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var updated = await _userService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }


        // Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

    }
}
