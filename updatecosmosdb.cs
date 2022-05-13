using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public static class updatecosmosdb
    {
        [FunctionName("updatecosmosdb")]
        [OpenApiOperation(operationId: "updatesekdetails", tags: new[] { "Update Reservation" })]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Request), Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "The OK response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB(ConnectionStringSetting = "CosmosDBConnection")] DocumentClient client,
  ILogger log)
        
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            officedeskcosmos toDoItem = JsonConvert.DeserializeObject<officedeskcosmos> (requestBody);

            var option = new FeedOptions { EnableCrossPartitionQuery = true };
            var collectionUri = UriFactory.CreateDocumentCollectionUri("dbworkstationmgmt", "workstationdetails");

            var document = client.CreateDocumentQuery(collectionUri, option).Where(t => t.Id == toDoItem.id.ToString())
                  .AsEnumerable().FirstOrDefault();
           // var jsonToReturn = JsonConvert.SerializeObject(toDoItemList);
            if (document == null)
            {
                return new NotFoundResult();
            }
            document.SetPropertyValue("id", toDoItem.id);
            document.SetPropertyValue("starttime", toDoItem.starttime);
            document.SetPropertyValue("endtime", toDoItem.endtime);
            document.SetPropertyValue("status", toDoItem.status);
            document.SetPropertyValue("assignedto", toDoItem.assignedto);

            //document.SetPropertyValue("Description", updated.Description);
            //document.SetPropertyValue("Priority", updated.Priority);
            //document.SetPropertyValue("Status", updated.Status);
            //document.SetPropertyValue("Date", updated.Date);
            await client.ReplaceDocumentAsync(document);
            var jsonToReturn = JsonConvert.SerializeObject(toDoItem);

            return new OkObjectResult(jsonToReturn);
        }
    }
}

