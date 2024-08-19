using DotNetChatApi.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DotNetChatApi
{
    public class DotNetChatApiBaseController<InputDto, OutputDto> : ControllerBase
    {
        protected readonly DatabaseContext context;
        protected readonly IConfiguration configs;
        protected readonly IDatabaseProvider databaseProvider;

        protected BaseOperationOutputDto<OutputDto> OperationResponse;

        public DotNetChatApiBaseController(DatabaseContext context, IConfiguration configs, IDatabaseProvider databaseProvider)
        {
            this.context = context;
            this.configs = configs;
            this.databaseProvider = databaseProvider;

            this.OperationResponse = new BaseOperationOutputDto<OutputDto>();
        }
    }
}
