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
    public class CatRepository
    {
        private PetAdoptionContext _context;

        public CatRepository(PetAdoptionContext context)
        {
            _context = context;

        }
        public IPagedList<Cat> GetCats(int pageSize, int page, PetSortBy sortBy, bool sortAsc)
        {
            IQueryable<Cat> catsToReturn = _context.Cats.Include(d => d.Breed);

            switch (sortBy)
            {
                case PetSortBy.Name:
                    if (sortAsc)
                        catsToReturn = catsToReturn.OrderBy(d => d.Name);
                    else
                        catsToReturn = catsToReturn.OrderByDescending(d => d.Name);
                    break;

                case PetSortBy.Age:
                    if (sortAsc)
                        catsToReturn = catsToReturn.OrderBy(d => d.Age).ThenBy(d => d.Name);
                    else
                        catsToReturn = catsToReturn.OrderByDescending(d => d.Age).ThenBy(d => d.Name);


                    break;

                case PetSortBy.Breed:
                    if (sortAsc)
                        catsToReturn = catsToReturn.OrderBy(d => d.Breed.Name).ThenBy(d => d.Name);
                    else
                        catsToReturn = catsToReturn.OrderByDescending(d => d.Breed.Name).ThenBy(d => d.Name);

                    break;

                case PetSortBy.DateAdded:

                    if (sortAsc)
                        catsToReturn = catsToReturn.OrderBy(d => d.Created).ThenBy(d => d.Name);
                    else
                        catsToReturn = catsToReturn.OrderByDescending(d => d.Created).ThenBy(d => d.Name);

                    break;

                default:
                    break;
            }
            return catsToReturn.ToPagedList(page, pageSize);
        }



    }
}
