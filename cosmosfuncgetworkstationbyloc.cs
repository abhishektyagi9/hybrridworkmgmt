using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace csacontestdemohybrid
{
    public static class cosmosfuncgetworkstationbyloc
    {
        [FunctionName("cosmosfuncgetworkstationbyloc")]
        [OpenApiOperation(operationId: "id", tags: new[] { "id" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "cosmosworkstaby")] HttpRequest req,
              [CosmosDB(
                databaseName: "dbworkstationmgmt",
                collectionName: "workstationdetails",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "select *  from c where c.officeId = @id")]
                IEnumerable<officedeskcosmos> toDoItems,
            ILogger log)

        {
            try
            {
                //Uri collectionUri = UriFactory.CreateDocumentCollectionUri("cosmoshybridappcsa", "Items");


                // Container container = client.GetDatabase("cosmoshybridappcsa").GetContainer("workstationdetails");
                var jsonToReturn = JsonConvert.SerializeObject(toDoItems);
                return new OkObjectResult(jsonToReturn);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
