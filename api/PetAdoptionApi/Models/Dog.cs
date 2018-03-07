using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionApi.Models
{
    public class Dog : Animal
    {
        public Breed Breed { get; set; }
        public DogAge DogAge {
            get
            {
                if (this.Age <= 1)
                {
                    return DogAge.Puppy;
                }
                else if (this.Age < 3)
                {
                    return DogAge.Juvenile;
                }
                else
                {
                    return DogAge.Adult;
                }
            }
        }
    }

    public enum DogAge
    {
        Puppy,
        Juvenile,
        Adult
    }
}
