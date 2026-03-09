using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CyberGuard.Infrastructure.Persistence; 
using CyberGuard.Domain;

namespace CyberGuard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VulnerabilitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<VulnerabilitiesController> _logger;

        public VulnerabilitiesController(ApplicationDbContext context, ILogger<VulnerabilitiesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Vulnerabilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vulnerability>>> GetAll()
        {
            _logger.LogInformation("Consultation de la liste des vulnérabilités.");
            var list = await _context.Vulnerabilities.ToListAsync();
            return Ok(list);
        }

        // GET: api/Vulnerabilities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vulnerability>> GetById(int id)
        {
            var vulnerability = await _context.Vulnerabilities.FindAsync(id);
            if (vulnerability == null)
            {
                _logger.LogWarning("Tentative d'accès à une vulnérabilité inexistante (ID: {Id}).", id);
                return NotFound("Vulnérabilité non trouvée.");
            }
            return Ok(vulnerability);
        }

        // POST: api/Vulnerabilities
        [HttpPost]
        public async Task<IActionResult> Create(Vulnerability vulnerability)
        {
            _logger.LogInformation("Tentative d'enregistrement : {Name}", vulnerability.Name);
            
            _context.Vulnerabilities.Add(vulnerability);
            await _context.SaveChangesAsync();
            
            _logger.LogWarning("SÉCURITÉ : Nouvelle vulnérabilité '{Name}' ajoutée avec succès (ID: {Id}).", 
                vulnerability.Name, vulnerability.Id);
            
            return CreatedAtAction(nameof(GetById), new { id = vulnerability.Id }, vulnerability);
        }

        // PUT: api/Vulnerabilities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Vulnerability vulnerability)
        {
            if (id != vulnerability.Id)
            {
                _logger.LogError("ÉCHEC UPDATE : Incohérence d'ID (URL: {IdUrl}, Body: {IdBody})", id, vulnerability.Id);
                return BadRequest("L'ID ne correspond pas.");
            }

            _context.Entry(vulnerability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("Mise à jour réussie pour la vulnérabilité ID: {Id}", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Vulnerabilities.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Vulnerabilities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var vulnerability = await _context.Vulnerabilities.FindAsync(id);
            if (vulnerability == null)
            {
                _logger.LogError("ÉCHEC DELETE : Tentative de suppression d'un ID inexistant ({Id})", id);
                return NotFound("Impossible de supprimer : ID introuvable.");
            }

            _context.Vulnerabilities.Remove(vulnerability);
            await _context.SaveChangesAsync();

            _logger.LogCritical("NETTOYAGE : La vulnérabilité ID: {Id} a été supprimée de la base.", id);

            return NoContent();
        }
    }
}