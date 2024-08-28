using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace MyFirstAPI
{
    public class GetAPI
    {
        private readonly ILogger<GetAPI> _logger;

        public GetAPI(ILogger<GetAPI> log)
        {
            _logger = log;
        }

        [FunctionName("Read")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "FirstAPI" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "Pass the ID")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            int id = int.Parse(req.Query["id"]);
            dynamic responseMessage = null;

            switch (id)
            {
                case 1:
                    responseMessage = JsonConvert.SerializeObject(
                    new
                    {
                        ErrorMessage = "",
                        Details = new
                        {
                            ID = 1,
                            Description = new
                            {
                                FirstName = "Naman",
                                LastName = "Sinha"
                            },
                            Username = "naman@bpb.com"
                        }
                    });
                    break;

                case 2:
                    responseMessage = JsonConvert.SerializeObject(
                    new
                    {
                        ErrorMessage = "",
                        Details = new
                        {
                            ID = 1,
                            Description = new
                            {
                                FirstName = "User",
                                LastName = "2"
                            },
                            Username = "user2@bpb.com"
                        }
                    });
                    break;
            }

            return new OkObjectResult(JsonConvert.DeserializeObject(responseMessage));
        }
    }
}

