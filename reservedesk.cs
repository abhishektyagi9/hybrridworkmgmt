using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace csacontestdemohybrid
{
    public static class reservedesk
    {
        [FunctionName("reservedesk")]
        [OpenApiOperation(operationId: "reservedesk", tags: new[] { "Update Reservation" })]
        [OpenApiRequestBody(contentType:"application/json", bodyType:typeof(Request), Required=true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "The OK response")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "reserverdesk")] HttpRequest req,
            ILogger log,
            [Sql("dbo.tbldeskinfo", ConnectionStringSetting = "SqlConnectionString")] IAsyncCollector<OfficeDesk> reserevedeskitem,
         [Sql("dbo.tblreservationdetails", ConnectionStringSetting = "SqlConnectionString")] IAsyncCollector<reservationdetails> reservationdetails)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            OfficeDesk toDoItem = JsonConvert.DeserializeObject<OfficeDesk>(requestBody);
            try
            {
              
                await reserevedeskitem.AddAsync(toDoItem);
                await reserevedeskitem.FlushAsync();
                List<OfficeDesk> toDoItemList = new List<OfficeDesk> { toDoItem };
                var jsonToReturn = JsonConvert.SerializeObject(toDoItemList);

                reservationdetails reservationdetail = new reservationdetails();
                reservationdetail.enddate = toDoItem.endtime;
                reservationdetail.startdate = toDoItem.starttime;
                reservationdetail.workstationid = toDoItem.desknumber;
                reservationdetail.reservedby = toDoItem.assignedto;

                await reservationdetails.AddAsync(reservationdetail);
                await reservationdetails.FlushAsync();

                return new OkObjectResult(jsonToReturn);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
