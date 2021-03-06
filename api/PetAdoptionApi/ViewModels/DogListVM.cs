﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoptionApi.ViewModels
{
    public class PetListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Age { get; set; }
        public int Years { get; set; }
        public DateTime DateAdded { get; set; } 
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string OriginUrl { get; set; }
        public List<PetDetail> OtherDetails { get; set; } = new List<PetDetail>();
    }
}
