using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RP.App.Tasks.GetAlbumByUsers;

namespace RP.Api.Features.GetAlbumByUsers
{
    public class GetAlbumByUserController : ControllerBase
    {
        private readonly IGetAlbumsByUser _getAlbumsByUser;

        public GetAlbumByUserController(IGetAlbumsByUser getAlbumsByUser)
        {
            _getAlbumsByUser = getAlbumsByUser;
        }
        
        [HttpGet]
        [Route("users/{userId}/albums")]
        public async Task<IActionResult> Get(int? userId)
        {
            if (!userId.HasValue || userId == 0)
            {
                return BadRequest("User id is not valid");
            }
            
            var response = await _getAlbumsByUser.Get(new GetAlbumsByUserRequest(userId.Value));

            if (!response.IsSuccess)
            {
                return BadRequest(response.FailureMessage);
            }
            
            return Ok(response.Value);
        }
    }
}