using System;
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
        private static DogRepository _dogRepository;

        [FunctionName("GetDogs")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            InitialiseRepositories();

            List<DogListVM> dogsToReturn = new List<DogListVM>();

            var qsPageSize = req.GetQueryNameValuePairs().SingleOrDefault(nv => nv.Key == "pageSize");
            var qsPageNumber = req.GetQueryNameValuePairs().SingleOrDefault(nv => nv.Key == "page");
            var qsSortBy = req.GetQueryNameValuePairs().SingleOrDefault(nv => nv.Key == "sortBy");

            int pageSize = 50;
            if ((qsPageSize.Equals(default(KeyValuePair<string,string>))) || (!int.TryParse(qsPageSize.Value, out pageSize)))
                pageSize = 50;

            int page = 1;
            if ((qsPageNumber.Equals(default(KeyValuePair<string, string>))) || (!int.TryParse(qsPageNumber.Value, out page)))
                page = 1;

            DogRepository.DogSortBy dogSortBy = DogRepository.DogSortBy.DateAdded;
            if (!Enum.TryParse<DogRepository.DogSortBy>(qsSortBy.Value, out dogSortBy))
                dogSortBy = DogRepository.DogSortBy.DateAdded;


            _dogRepository.GetDogs(pageSize, page, dogSortBy, true).ToList().ForEach(d => dogsToReturn.Add(new DogListVM()
            {
                Id = d.Id,
                Age = d.DogAge.ToString(),
                Name = d.Name,
                Breed = d.Breed.Name,
                Years = d.Age,
                DateAdded = d.Created,
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

        private static void InitialiseRepositories()
        {
            var sqlConnection = ConfigurationManager.ConnectionStrings["PetAdoptionContextConnection"].ConnectionString;

            var builder = new DbContextOptionsBuilder<PetAdoptionContext>();
            builder.UseSqlServer(sqlConnection);



            var petAdoptionContext = new PetAdoptionContext(builder.Options);
            //DbInitialiser.Initialize(petAdoptionContext);

            _dogRepository = new DogRepository(petAdoptionContext);// new Context.PetAdoptionContext(null, null));

        }
    }
}
