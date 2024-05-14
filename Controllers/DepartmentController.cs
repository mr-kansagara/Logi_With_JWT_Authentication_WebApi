using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Login_With_JWT_Authentication.Database;
using Login_With_JWT_Authentication.Model.Departments;
using Microsoft.AspNetCore.Authorization;

namespace Login_With_JWT_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DepartmentController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentModel>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentModel>> GetDepartmentModel(int? id)
        {
            var departmentModel = await _context.Departments.FindAsync(id);

            if (departmentModel == null)
            {
                return NotFound();
            }

            return departmentModel;
        }

        // PUT: api/Department/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartmentModel(int? id, DepartmentModel departmentModel)
        {
            if (id != departmentModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(departmentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentModelExists(id))
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

        // POST: api/Department
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DepartmentModel>> PostDepartmentModel(DepartmentModel departmentModel)
        {
            _context.Departments.Add(departmentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartmentModel", new { id = departmentModel.Id }, departmentModel);
        }

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentModel(int? id)
        {
            var departmentModel = await _context.Departments.FindAsync(id);
            if (departmentModel == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(departmentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentModelExists(int? id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}
