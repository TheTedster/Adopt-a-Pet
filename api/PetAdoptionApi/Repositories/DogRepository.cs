using Microsoft.EntityFrameworkCore;
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
        

        private PetAdoptionContext _context;

        public DogRepository(PetAdoptionContext context)
        {
            _context = context;

        }
        public List<Dog> GetDogs()
        {
            return _context.Dogs.Include(d => d.Breed).OrderBy(d => d.Name).ToList();
        }

    }
}
