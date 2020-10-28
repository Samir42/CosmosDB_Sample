using CosmosDB_Sample.Models;
using CosmosDB_Sample.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CosmosDB_Sample.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ICosmosDbService _service;

        public UsersController(ICosmosDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(User userToCreate)
        {
            for (int i = 1; i < 120; i++)
            {
                userToCreate.Id += i.ToString();
                await _service.AddUserAsync(userToCreate);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            return Ok(await _service.GetUserAsync(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById()
        {
            return Ok(await _service.GetUsersAsync("SELECT * FROM c"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(User userToUpdate)
        {
            await _service.UpdateUserAsync(userToUpdate.Id, userToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _service.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
