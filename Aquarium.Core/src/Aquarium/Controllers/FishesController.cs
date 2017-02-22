﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aquarium.Data;
using Aquarium.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aquarium.Controllers
{
    [Route("api/[controller]")]
    public class FishesController : Controller
    {

        private AquariumContext Context { get; set; }
        public List<Fish> Fishes { get; set; }

        public FishesController()
        {
            Context = new AquariumContext();
            Fishes = new List<Fish>();
        }


        [HttpGet]
        public List<Fish> Get()
        {
            return Context.Fishes.ToList();
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            var fish = Context.Fishes.FirstOrDefault(q => q.Id == id);
            return fish;
        }

        // POST api/values
        [HttpPost]
        public Fish Post([FromBody]Fish fish)
        {
            var newFish = Context.Fishes.Add(fish);
            Context.SaveChanges();

            return fish;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Fish Put(int id, [FromBody]Fish fish)
        {
            var newFish = Context.Fishes.FirstOrDefault(q => q.Id == id);
            newFish.Type = fish.Type;
            newFish.Name = fish.Name;
            newFish.Quantity = fish.Quantity;
            newFish.Description = fish.Description;
            newFish.Image = fish.Image;

            Context.SaveChanges();
            return fish;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var fish = Context.Fishes.Find(id);
            Context.Fishes.Remove(fish);
            Context.SaveChanges();
        }
    }
}