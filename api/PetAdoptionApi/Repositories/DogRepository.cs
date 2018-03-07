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

        public DogRepository()
        {
            var alsation = new Breed() { Id = 1, Name = "Alsation" };
            var jackRussell = new Breed() { Id = 1, Name = "Jack Russell" };
            var labrador = new Breed() { Id = 1, Name = "Labrador" };


            testDogs.Add(new Dog() { Id = 1, Name = "Sam", Age = 2, Gender = Gender.Male, Breed = labrador });
            testDogs.Add(new Dog() { Id = 2, Name = "Ben", Age = 4, Gender = Gender.Male, Breed = labrador });
            testDogs.Add(new Dog() { Id = 3, Name = "JR", Age = 2, Gender = Gender.Male, Breed = jackRussell });
            testDogs.Add(new Dog() { Id = 4, Name = "Rover", Age = 2, Gender = Gender.Male, Breed = alsation });
        }
        public List<Dog> GetDogs()
        {
            return testDogs;
        }

    }
}
