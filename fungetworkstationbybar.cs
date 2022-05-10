using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace csacontestdemohybrid
{
    public static class fungetworkstationbybar
    {
        [FunctionName("fungetworkstationbybar")]
        [OpenApiOperation(operationId: "fungetworkstationbybar", tags: new[] { "barcode" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "barcode", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **barcode** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "fungetworkstationbybar")] HttpRequest req,
          [Sql("select * from dbo.tbldeskinfo where deskbarcode = @barcode",
                CommandType = System.Data.CommandType.Text,
                Parameters = "@barcode={Query.barcode}",
                ConnectionStringSetting = "SqlConnectionString")]
            IEnumerable<OfficeDesk> toDoItems)

        {
            var jsonToReturn = JsonConvert.SerializeObject(toDoItems);
            return new OkObjectResult(jsonToReturn);
        }
    }
}
