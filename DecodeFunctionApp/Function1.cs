using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DecodeFunctionApp
{
    public static class Function1
    {
        [FunctionName("Decode")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var result = string.Empty;
            using (StreamReader reader
                  = new StreamReader(req.Body, Encoding.UTF7))
            {
                var bodyContent = await reader.ReadToEndAsync();
                if (bodyContent.Contains("ADM-ID:"))
                {
                    result = bodyContent.Split("ADM-ID:")[1].Split(",")[0].Trim();
                }
            }

            return new OkObjectResult(result);
        }
    }
}
