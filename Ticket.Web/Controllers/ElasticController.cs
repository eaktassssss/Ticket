using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Dtos.Elastic;
using Ticket.Dtos.Tickets;

namespace Ticket.Web.Controllers
{
    public class ElasticController : Controller
    {
        [HttpGet]
        public ActionResult CreateIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateIndex(ElasticDto dto)
        {

            try
            {
                ElasticDto elasticDto = new ElasticDto();
                elasticDto.Name = "EVREN";
                elasticDto.Id = Guid.NewGuid().ToString();

               var connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DisablePing()
                .DisableDirectStreaming(true)
                .SniffOnStartup(false)
                .SniffOnConnectionFault(false)
                .BasicAuthentication("elastic", "pwd123456**");
               var connection= new ElasticClient(connectionSettings);

                TicketQueueDto ticket = new TicketQueueDto();
                ticket.CreatedDate = DateTime.Now;
                if (!connection.Indices.Exists("tickets1").Exists)
                {
                    var newIndexName = "tickets1" + System.DateTime.Now.Ticks;

                    var indexSettings = new IndexSettings();
                    indexSettings.NumberOfReplicas = 1;
                    indexSettings.NumberOfShards = 3;

                    var response = connection.Indices.Create(newIndexName, index =>
                       index.Map<ElasticDto>(m => m.AutoMap()
                              )
                      .InitializeUsing(new IndexState() { Settings = indexSettings })
                      .Aliases(a => a.Alias("tickets1")));

                }
                IndexResponse responseIndex = connection.Index<ElasticDto>(elasticDto, idx => idx.Index("tickets1"));

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return View();
        }
    }
}
