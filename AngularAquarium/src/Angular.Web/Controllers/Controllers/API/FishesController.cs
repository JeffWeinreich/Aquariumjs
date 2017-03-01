using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aquarium.Data;
using Aquarium.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Angular.Web.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aquarium.Controllers
{
    [Produces("application/json")]
    [Route("api/fishes")]
    [Authorize]
    public class FishesController : Controller
    {
        private readonly AquariumContext _context;
        private UserManager<ApplicationUser> _userManager { get; set; }
       //private Tank _tank { get; set; }             //new try
        

        public FishesController(UserManager<ApplicationUser> userManager, AquariumContext context)
        {
            _userManager = userManager;
            _context = context;
           // _tank = tank;
        }

        [Route("~/fishes")]
        public IActionResult Owner()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<Fish> GetFish()
        {
            var userId = _userManager.GetUserId(User);
            //var tankId = _tank.OwnerId;
            return _context.Fishes.Where(q => q.OwnerId == userId).ToList();
        }


        // GET api/values/5
        [HttpGet("~/api/tanks/{id}/fishes")]
        public async Task<IActionResult> GetFish([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            var fish = await _context.Fishes
                .Where(p => p.TankId == id).ToListAsync();

            if (fish == null)
            {
                return NotFound();
            }

            return Ok(fish);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFish([FromRoute] int id, [FromBody] Fish fish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fish.Id)
            {
                return BadRequest();
            }

            
            fish.Owner = await _userManager.GetUserAsync(User);
            _context.Entry(fish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

     
        [HttpPost("~/api/tanks/{tankId}/fishes")]
        public async Task<IActionResult> PostFish(int tankId, [FromBody] Fish fish)
        {
            var tank = _context.Tanks.FirstOrDefault(q => q.Id == tankId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            fish.Owner = await _userManager.GetUserAsync(User);
            fish.Tank = tank;
                      
            _context.Fishes.Add(fish);
                        
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if(FishExists(fish.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFish", new { id = fish.Id }, fish);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFish([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Fish fish = await _context.Fishes
                .Where(q => q.OwnerId == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if(fish == null)
            {
                return NotFound();
            }

            _context.Fishes.Remove(fish);
            await _context.SaveChangesAsync();

            return Ok(fish);
        }

        //[HttpDelete]
        //public async Task<IActionResult> ClearFish([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var userId = _userManager.GetUserId(User);

        //    Fish fish = _context.Fishes
        //        .Where(x => x.Owner == userId)
        //        .ForEachAsync(n => n.Id == id);
        //}

        private bool FishExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Fishes.Any(e => e.OwnerId == userId && e.Id == id);
        }


    }
}