using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReportProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IGenericRepository<Company> _companyRepository;

        public CompanyController(IGenericRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        // GET: api/company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetAll(ISpecification<Company> spec)
        {
            var companies = await _companyRepository.GetAllAsync(spec, null, true);
            return Ok(companies);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetById(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<Company>> Create([FromBody] Company company)
        {
            if (company == null)
            {
                return BadRequest();
            }

            await _companyRepository.AddAsync(company);
            return CreatedAtAction(nameof(GetById), new { id = company.Id }, company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            await _companyRepository.UpdateAsync(company);
            return NoContent(); // Güncelleme başarılı olduğunda 204 No Content döner
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            await _companyRepository.DeleteAsync(id);
            return NoContent(); // Silme başarılı olduğunda 204 No Content döner
        }
    }
}
