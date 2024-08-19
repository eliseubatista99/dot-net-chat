using DotNetChatApi.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetChatApi
{
    [Route("GetUserChats")]
    [ApiController]
    public class GetUserChatsController : DotNetChatApiBaseController<GetUserChatsOperationInputDto, GetUserChatsOperationOutputDto>
    {
        public GetUserChatsController(DatabaseContext context, IConfiguration configs, IDatabaseProvider databaseProvider) : base(context, configs, databaseProvider)
        {

        }

        /// <summary>
        /// Get the chats of a specific user.
        /// </summary>
        /// <param name="username" example="user"></param>
        /// <returns>List of user chats.</returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [AllowAnonymous]

        public async Task<ActionResult<BaseOperationOutputDto<GetUserChatsOperationOutputDto>>> RunAsync(GetUserChatsOperationInputDto input)
        {
            if (String.IsNullOrEmpty(input.UserName))
            {
                OperationResponse.SetError("Username is required!");
                return OperationResponse;
            }

            var userChats = databaseProvider.RetrieveChatsForUser(input.UserName);

            OperationResponse.SetData(new GetUserChatsOperationOutputDto
            {
                Chats = userChats,
            });

            return OperationResponse;
        }
    }
}
