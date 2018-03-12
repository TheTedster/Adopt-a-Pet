using PetAdoptionApi.Context;
using PetAdoptionApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionApi.Repositories
{
    public class DogRepository
    {
        private List<Dog> testDogs = new List<Dog>() ;

        private PetAdoptionContext context;

        public DogRepository(PetAdoptionContext context)
        {
            var alsation = new Breed() { Id = 1, Name = "Alsation" };
            var jackRussell = new Breed() { Id = 1, Name = "Jack Russell" };
            var labrador = new Breed() { Id = 1, Name = "Labrador" };


            testDogs.Add(new Dog() { Id = 1, Name = "Sam", Age = 2, Gender = Gender.Male, Breed = labrador , OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/CHARLIE/ref/BSA2074301/rehome", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=146432", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=146432" });
            testDogs.Add(new Dog() { Id = 2, Name = "Ben", Age = 4, Gender = Gender.Male, Breed = labrador , OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/CUSH/ref/BSA2073932/rehome" , ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144095", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144095" });
            testDogs.Add(new Dog() { Id = 3, Name = "JR", Age = 2, Gender = Gender.Male, Breed = jackRussell , OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/MONKEY/ref/BSA2072934/rehome", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=139488", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=139488" });
            testDogs.Add(new Dog() { Id = 4, Name = "Rover", Age = 2, Gender = Gender.Male, Breed = alsation, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/CHARLIE/ref/BSA2074039/rehome", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144623", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144623" });
        }
        public List<Dog> GetDogs()
        {
            return testDogs;
        }

    }
}
