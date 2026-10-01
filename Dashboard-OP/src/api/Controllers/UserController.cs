using Dashboard_OP.src.api.Models;
using Dashboard_OP.src.api.Services;
using Dashboard_OP.src.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_OP.src.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/User/pon-aqui-el-uuid
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody] User user)
        {
            var created = await _userService.CreateAsync(user);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/User/pon-aqui-el-uuid
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] User user)
        {
            var updated = await _userService.UpdateAsync(id, user);
            if (!updated) return NotFound();
            return NoContent();
        }

        // DELETE: api/User/pon-aqui-el-uuid
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}