using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Erp_Repo.DTOs;    
using Erp_Repo.Interfaces;
using System;
using System.Threading.Tasks;

namespace ERP_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT token required for all endpoints
    public class MathController : ControllerBase
    {
        private readonly IMathService _mathService;

        public MathController(IMathService mathService)
        {
            _mathService = mathService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var operations = await _mathService.GetAllAsync();
            return Ok(operations);
        }

        [HttpGet("{id}")]
        
        public async Task<IActionResult> GetById(int id)
        {
            var operation = await _mathService.GetByIdAsync(id);
            if (operation == null)
                return NotFound(new { Message = $"Operation with ID {id} not found." });

            return Ok(operation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMathOperationDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _mathService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MathOperationUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "ID mismatch." });

            try
            {
                var updated = await _mathService.UpdateAsync(dto);
                if (updated == null)
                    return NotFound(new { Message = $"Operation with ID {id} not found." });

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _mathService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = $"Operation with ID {id} not found." });

            return Ok(new { Message = $"Operation {id} deleted successfully." });
        }
    }
}
