using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alkedis.Modelos;
using Alkedis.Modelos.Request;

namespace Alkedis.Controllers
{
    [Route("/movies")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly DB _context;

        public PeliculasController(DB context)
        {
            _context = context;
        }

        // GET: /movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetPelicula()
        {
            var lp = await _context.Pelicula.ToListAsync();
            List<VistaPelicula> lista = new List<VistaPelicula>();
            foreach (var item in lp)
            {   
                VistaPelicula vp = new VistaPelicula();
                vp.fCreacion = item.fCreacion;
                vp.imagen = item.imagen;
                vp.titulo = item.titulo;
                
                lista.Add(vp);
            }
            return Ok(lista);
        }

  

        // GET: api/Peliculas/details/5
        [HttpGet("details/{id}")]
        public async Task<ActionResult<Pelicula>> GetPelicula(int id)
        {
           
            var pelicula = await _context.Pelicula.FindAsync(id);
            var pp = _context.Pelicula.Where(p => p.Id == id).Include(x => x.Personajes).ThenInclude(y => y.peliculas).FirstOrDefault();
            if (pp != null)
            {
                foreach (var item in pp.Personajes)
                {
                    item.peliculas = null;
                    pelicula.Personajes.Add(item);
                }
            }
            if (pelicula == null)
            {
                return NotFound();
            }

            return pelicula;
        }

        // PUT: api/movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutPelicula(int id, [FromBody] Modelos.Request.PeliculaEditRequest item)
        {
            var pelicula = await _context.Pelicula.FindAsync(id);

            if (pelicula == null)
            {
                return BadRequest();
            }
            pelicula.calif = item.calificacion;
            pelicula.fCreacion = item.fCreacion;
            pelicula.generoId = item.generoid;
            pelicula.imagen = item.imagen;
            pelicula.titulo = item.titulo;

            _context.Entry(pelicula).State = EntityState.Modified;
         
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(id))
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

        // POST: api/movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pelicula>> PostPelicula(Pelicula pelicula)
        {
            _context.Pelicula.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPelicula", new { id = pelicula.Id }, pelicula);
        }

        // DELETE: api/movies/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePelicula(int id)
        {
            var pelicula = await _context.Pelicula.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _context.Pelicula.Remove(pelicula);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeliculaExists(int id)
        {
            return _context.Pelicula.Any(e => e.Id == id);
        }
    }
}
