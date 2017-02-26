﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Aquarium.Data;
using Microsoft.AspNetCore.Authorization;
using Angular.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Angular.Web.Controllers.Controllers.API
{
    [Produces("application/json")]
    [Route("api/tank")]
    [Authorize]
    public class TankController : Controller
    {
        private readonly AquariumContext _context;
        private UserManager<ApplicationUser> _userManager { get; set; }

        public TankController(UserManager<ApplicationUser> userManager, AquariumContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("~/tank")]
        public IActionResult Owner()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<Tank> GetTank()
        {
            var userId = _userManager.GetUserId(User);
            return _context.Tanks.Where(q => q.OwnerId == userId).ToList();
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTank([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);
            Tank tank = await _context.Tanks
                .SingleOrDefaultAsync(p => p.OwnerId == userId && p.Id == id);

            if (tank == null)
            {
                return NotFound();
            }

            return Ok(tank);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTank([FromRoute] int id, [FromBody] Tank tank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tank.Id)
            {
                return BadRequest();
            }

            tank.Owner = await _userManager.GetUserAsync(User);
            _context.Entry(tank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TankExists(id))
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
        public async Task<IActionResult> PostTank([FromBody] Tank tank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tank.Owner = await _userManager.GetUserAsync(User);
            _context.Tanks.Add(tank);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TankExists(tank.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTank", new { id = tank.Id }, tank);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTank([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            Tank tank = await _context.Tanks
                .Where(q => q.OwnerId == userId)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (tank == null)
            {
                return NotFound();
            }

            _context.Tanks.Remove(tank);
            await _context.SaveChangesAsync();

            return Ok(tank);
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

        private bool TankExists(int id)
        {
            var userId = _userManager.GetUserId(User);
            return _context.Tanks.Any(e => e.OwnerId == userId && e.Id == id);
        }


    }
}