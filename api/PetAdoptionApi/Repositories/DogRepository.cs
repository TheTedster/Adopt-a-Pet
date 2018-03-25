using Microsoft.EntityFrameworkCore;
using PagedList;
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
        public IPagedList<Dog> GetDogs(int pageSize, int page, PetSortBy sortBy, bool sortAsc)
        {
            IQueryable<Dog> dogsToReturn = _context.Dogs.Include(d => d.Breed);

            switch (sortBy)
            {                    
                case PetSortBy.Name:
                    if(sortAsc)
                        dogsToReturn = dogsToReturn.OrderBy(d => d.Name);
                    else
                        dogsToReturn = dogsToReturn.OrderByDescending(d => d.Name);
                    break;

                case PetSortBy.Age:
                    if (sortAsc)
                        dogsToReturn = dogsToReturn.OrderBy(d => d.Age).ThenBy(d => d.Name);
                    else
                        dogsToReturn = dogsToReturn.OrderByDescending(d => d.Age).ThenBy(d => d.Name);


                    break;

                case PetSortBy.Breed:
                    if (sortAsc)
                        dogsToReturn = dogsToReturn.OrderBy(d => d.Breed.Name).ThenBy(d => d.Name);
                    else
                        dogsToReturn = dogsToReturn.OrderByDescending(d => d.Breed.Name).ThenBy(d => d.Name); 
                    
                    break;

                case PetSortBy.DateAdded:

                    if (sortAsc)
                        dogsToReturn = dogsToReturn.OrderBy(d => d.Created).ThenBy(d => d.Name);
                    else
                        dogsToReturn = dogsToReturn.OrderByDescending(d => d.Created).ThenBy(d => d.Name);
                    
                    break;

                default:
                    break;
            }
            return dogsToReturn.ToPagedList(page, pageSize);
        }


        
    }
}
