using DotNetChatApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DotNetChatApi
{
    public class DotNetChatApiBaseController<InputDto, OutputDto> : ControllerBase
    {
        protected readonly DatabaseContext _context;
        protected readonly IConfiguration _configs;

        protected BaseOperationOutputDto<OutputDto> OperationResponse;

        public DotNetChatApiBaseController(DatabaseContext context, IConfiguration configs)
        {
            _context = context;
            _configs = configs;

            this.OperationResponse = new BaseOperationOutputDto<OutputDto>();
        }
    }
}
