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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Aquarium.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class FishesController : Controller
    {
        private readonly AquariumContext _context;
        private UserManager<ApplicationUser> _userManager { get; set; }

        public FishesController(UserManager<ApplicationUser> userManager, AquariumContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [HttpGet]
        public IEnumerable<Fish> GetFish()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Fishes.Where(q => q.Owner == userId).ToList();
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFish([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            Fish fish = await _context.Fishes
                .SingleOrDefaultAsync(p => p.Owner == userId && p.Id == id);

            if (fish == null)
            {
                return NotFound();
            }

            return Ok();
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

            fish.Owner = _userManager.GetUserId(User);
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

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostFish([FromBody] Fish fish)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            fish.Owner = _userManager.GetUserId(User);
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
                .Where(q => q.Owner == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if(fish == null)
            {
                return NotFound();
            }

            _context.Fishes.Remove(fish);
            await _context.SaveChangesAsync();

            return Ok(fish);
        }

        private bool FishExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Fishes.Any(e => e.Owner == userId && e.Id == id);
        }
    }
}