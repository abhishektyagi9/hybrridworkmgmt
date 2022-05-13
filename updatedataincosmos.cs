using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace csacontestdemohybrid
{
    public static class updatedataincosmos
    {
  //      [FunctionName("updatedataincosmos")]
  //      public static async   Task<IActionResult> Run
  //          ([QueueTrigger("hybridmessage", Connection = "AzureWebJobsStorage")] OfficeDesk updated,
  //           [CosmosDB(ConnectionStringSetting = "**Connection string**")] DocumentClient client,
  //ILogger log, string Id)
  //      {
  //          //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
  //          //var updated = JsonConvert.DeserializeObject<Todolist>(requestBody);

  //          var option = new FeedOptions { EnableCrossPartitionQuery = true };
  //          var collectionUri = UriFactory.CreateDocumentCollectionUri("my-database", "my-container");

  //          var document = client.CreateDocumentQuery(collectionUri, option).Where(t => t.Id == Id)
  //                .AsEnumerable().FirstOrDefault();

  //          if (document == null)
  //          {
  //              return new NotFoundResult();
  //          }
  //          document.SetPropertyValue("id", updated.id);
  //          document.SetPropertyValue("starttime", updated.starttime);
  //          //document.SetPropertyValue("Description", updated.Description);
  //          //document.SetPropertyValue("Priority", updated.Priority);
  //          //document.SetPropertyValue("Status", updated.Status);
  //          //document.SetPropertyValue("Date", updated.Date);
  //          await client.ReplaceDocumentAsync(document);

  //          return new OkResult();
  //      }
    }
}
