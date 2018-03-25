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
using PetAdoptionApi.Extensions;
using PetAdoptionApi.Repositories;
using PetAdoptionApi.ViewModels;

namespace PetAdoptionApi
{
    public static class GetPets
    {
        private static DogRepository _dogRepository;
        private static CatRepository _catRepository;

        [FunctionName("GetPets")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            InitialiseRepositories();

            List<PetListVM> petsToReturn = new List<PetListVM>();
            
            var qsPetType = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "petType", true) == 0).Value;
            var qsPageSize = req.GetQueryNameValuePairs().FirstOrDefault(nv => string.Compare(nv.Key, "pageSize",true) == 0).Value;
            var qsPageNumber = req.GetQueryNameValuePairs().FirstOrDefault(nv => string.Compare(nv.Key, "page", true) == 0).Value;
            var qsSortBy = req.GetQueryNameValuePairs().FirstOrDefault(nv => string.Compare(nv.Key, "sortBy", true) == 0).Value;

            PetType petType = PetType.All;
            if (!Enum.TryParse<PetType>(qsPetType, true, out petType))
            {
                if (String.IsNullOrWhiteSpace(qsPetType))
                    petType = PetType.All;
                else
                    return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid pet type on the query string");
            }
                

            int pageSize = 50;
            if ((!int.TryParse(qsPageSize, out pageSize)))
                pageSize = 50;

            int page = 1;
            if ((!int.TryParse(qsPageNumber, out page)))
                page = 1;

            PetSortBy petSortBy = PetSortBy.DateAdded;
            if (!Enum.TryParse<PetSortBy>(qsSortBy, true, out petSortBy))
                petSortBy = PetSortBy.DateAdded;

            switch (petType)
            {
                case PetType.All:
                    petsToReturn = GetAllPets(pageSize, page, petSortBy);
                    break;
                case PetType.Dog:
                    petsToReturn = GetDogs(pageSize, page, petSortBy);
                    break;
                case PetType.Cat:
                    petsToReturn = GetCats(pageSize, page, petSortBy);
                    break;
                case PetType.Hamster:
                    break;
                case PetType.Snake:
                    break;
                default:
                    break;
            }


            

            return req.CreateResponse<List<PetListVM>>(HttpStatusCode.OK, petsToReturn, "application/json"); 


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
            _catRepository = new CatRepository(petAdoptionContext);// new Context.PetAdoptionContext(null, null));

        }

        private static List<PetListVM> GetAllPets(int pageSize, int page, PetSortBy petSortBy)
        {
            List<PetListVM> petVMsToReturn = new List<PetListVM>();
            var dogs = GetDogs(pageSize / 2, page, petSortBy);
            var cats = GetCats(pageSize / 2, page, petSortBy);
            petVMsToReturn.AddRange(dogs);
            petVMsToReturn.AddRange(cats);
            petVMsToReturn.Shuffle();

            return petVMsToReturn;
        }


        private static List<PetListVM> GetDogs(int pageSize, int page, PetSortBy sortBy)
        {
            var dogModels = _dogRepository.GetDogs(pageSize, page, sortBy, true).ToList();

            List<PetListVM> petVMsToReturn = new List<PetListVM>();
            dogModels.ForEach(d => petVMsToReturn.Add(new PetListVM()
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

            return petVMsToReturn;
        }

        private static List<PetListVM> GetCats(int pageSize, int page, PetSortBy sortBy)
        {
            var catModels = _catRepository.GetCats(pageSize, page, sortBy, true).ToList();

            List<PetListVM> petVMsToReturn = new List<PetListVM>();
            catModels.ForEach(c => petVMsToReturn.Add(new PetListVM()
            {
                Id = c.Id,
                Age = c.Age.ToString(),
                Name = c.Name,
                Breed = c.Breed.Name,
                Years = c.Age,
                DateAdded = c.Created,
                ThumbnailUrl = c.ThumbnailUrl,
                ImageUrl = c.ImageUrl,
                OriginUrl = c.OriginUrl,
                OtherDetails = new List<PetDetail> { new PetDetail() { LabelText = "Indoor Cat", DetailsText = c.IsIndoor ? "Yes" : "No", iconCssId = "indoorCat" } }
            }));

            return petVMsToReturn;
        }

        private enum PetType
        {
            All,
            Dog,
            Cat,
            Hamster,
            Snake            
        }
    }
}
