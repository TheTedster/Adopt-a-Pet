using PetAdoptionApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionApi.Context
{
    public static class DbInitialiser
    {
        public static void Initialize(PetAdoptionContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            // Look for any students.
            if (context.Dogs.Any())
            {
                return;   // DB has been seeded
            }


            var alsation = new Breed() {  Name = "Alsation", AnimalType = AnimalType.Dog };
            var jackRussell = new Breed() { Name = "Jack Russell", AnimalType = AnimalType.Dog };
            var labrador = new Breed() { Name = "Labrador", AnimalType = AnimalType.Dog };

            context.Breeds.Add(alsation);
            context.Breeds.Add(jackRussell);
            context.Breeds.Add(labrador);
            context.SaveChanges();

            List<Dog> testDogs = new List<Dog>();
            testDogs.Add(new Dog() { Name = "Sam", Age = 2, Gender = Gender.Male, Breed = labrador, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/CHARLIE/ref/BSA2074301/rehome", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=146432", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=146432" });
            testDogs.Add(new Dog() { Name = "Ben", Age = 4, Gender = Gender.Male, Breed = labrador, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/CUSH/ref/BSA2073932/rehome", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144095", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144095" });
            testDogs.Add(new Dog() { Name = "JR", Age = 2, Gender = Gender.Male, Breed = jackRussell, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/MONKEY/ref/BSA2072934/rehome", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=139488", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=139488" });
            testDogs.Add(new Dog() { Name = "Rover", Age = 2, Gender = Gender.Male, Breed = alsation, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/CHARLIE/ref/BSA2074039/rehome", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144623", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144623" });
            foreach (Dog d in testDogs)
            {
                context.Dogs.Add(d);
            }
            context.SaveChanges();


            var tabby = new Breed() { Name = "Tabby", AnimalType = AnimalType.Cat };
            var siamese = new Breed() { Name = "Siamese", AnimalType = AnimalType.Cat };

            context.Breeds.Add(tabby);
            context.Breeds.Add(siamese);
            context.SaveChanges();

            List<Cat> testCats = new List<Cat>();
            testCats.Add(new Cat() { Name = "Mr Tiddles", IsIndoor = false, Age = 4, Breed = tabby, Gender = Gender.Male, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/MISTY/ref/BSA2073945/rehome", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144158", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144158" });
            testCats.Add(new Cat() { Name = "Misty", IsIndoor = false, Age = 8, Breed = tabby, Gender = Gender.Male, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/MISTY/ref/BSA2073945/rehome", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144158", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144158" });
            testCats.Add(new Cat() { Name = "Bubbles", IsIndoor = false, Age = 1, Breed = siamese, Gender = Gender.Male, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/MISTY/ref/BSA2073945/rehome", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144158", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144158" });
            testCats.Add(new Cat() { Name = "Bobbles", IsIndoor = false, Age = 2, Breed = siamese, Gender = Gender.Male, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/MISTY/ref/BSA2073945/rehome", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144158", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144158" });
            testCats.Add(new Cat() { Name = "Plopper", IsIndoor = true, Age = 0, Breed = tabby, Gender = Gender.Male, OriginUrl = "https://www.rspca.org.uk/findapet/details/-/Animal/MISTY/ref/BSA2073945/rehome", ThumbnailUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=medium&imageId=144158", ImageUrl = "https://www.rspca.org.uk/GenericImage/CallGenericImage?source=petSearch&size=large&imageId=144158" });
            context.Cats.AddRange(testCats);
            context.SaveChanges();
        }
        
    }
}

