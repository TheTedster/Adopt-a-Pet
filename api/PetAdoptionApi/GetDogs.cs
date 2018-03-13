using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using PetAdoptionApi.Context;
using PetAdoptionApi.Repositories;
using PetAdoptionApi.ViewModels;

namespace PetAdoptionApi
{
    public static class GetDogs
    {        
        [FunctionName("GetDogs")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            var sqlConnection = ConfigurationManager.ConnectionStrings["PetAdoptionContextConnection"].ConnectionString;

            var builder = new DbContextOptionsBuilder<PetAdoptionContext>();
            builder.UseSqlServer(sqlConnection);
            
            

            var petAdoptionContext = new PetAdoptionContext(builder.Options);
            DbInitialiser.Initialize(petAdoptionContext);

            var dogRepository = new DogRepository(petAdoptionContext);// new Context.PetAdoptionContext(null, null));
            List<DogListVM> dogsToReturn = new List<DogListVM>();

            dogRepository.GetDogs().ForEach(d => dogsToReturn.Add(new DogListVM()
            {
                Id = d.Id,
                Age = d.DogAge.ToString(),
                Name = d.Name,
                Breed = d.Breed.Name,
                Years = d.Age,
                ThumbnailUrl = d.ThumbnailUrl,
                ImageUrl = d.ImageUrl,
                OriginUrl = d.OriginUrl
            }));

            return req.CreateResponse<List<DogListVM>>(HttpStatusCode.OK, dogsToReturn, "application/json"); 


            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Set name to query string or body data
            name = name ?? data?.name;

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        }
    }
}
