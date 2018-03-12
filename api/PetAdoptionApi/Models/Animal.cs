﻿namespace PetAdoptionApi.Models
{
    public abstract class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public string ThumbnailUrl { get; internal set; }
        public string ImageUrl { get; internal set; }
        public string OriginUrl { get; internal set; }
    }
}