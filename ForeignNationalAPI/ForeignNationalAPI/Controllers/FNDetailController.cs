using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForeignNationalAPI.Models;

namespace ForeignNationalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FNDetailController : ControllerBase
    {
        private readonly FNDetailContext _context;

        public FNDetailController(FNDetailContext context)
        {
            _context = context;
        }

        // GET: api/FNDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FNDetail>>> GetFN_Details()
        {
            return await _context.FN_Details.ToListAsync();
        }

        // GET: api/FNDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FNDetail>> GetFNDetail(int id)
        {
            var fNDetail = await _context.FN_Details.FindAsync(id);

            if (fNDetail == null)
            {
                return NotFound();
            }

            return fNDetail;
        }

        // PUT: api/FNDetail/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFNDetail(int id, FNDetail fNDetail)
        {
            if (id != fNDetail.FNDetailId)
            {
                return BadRequest();
            }

            _context.Entry(fNDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FNDetailExists(id))
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

        // POST: api/FNDetail
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FNDetail>> PostFNDetail(FNDetail fNDetail)
        {
            _context.FN_Details.Add(fNDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFNDetail", new { id = fNDetail.FNDetailId }, fNDetail);
        }

        // DELETE: api/FNDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FNDetail>> DeleteFNDetail(int id)
        {
            var fNDetail = await _context.FN_Details.FindAsync(id);
            if (fNDetail == null)
            {
                return NotFound();
            }

            _context.FN_Details.Remove(fNDetail);
            await _context.SaveChangesAsync();

            return fNDetail;
        }

        private bool FNDetailExists(int id)
        {
            return _context.FN_Details.Any(e => e.FNDetailId == id);
        }
    }
}
