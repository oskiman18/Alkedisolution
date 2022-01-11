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
    [Route("/characters")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private readonly DB _context;

        public PersonajesController(DB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personaje>>> GetPersonaje()
        {
            var lp = await _context.Personaje.ToListAsync();
            List<VistaPersonaje> lista = new List<VistaPersonaje>();
            foreach (var item in lp)
            {
                VistaPersonaje vp = new VistaPersonaje();
                vp.imagen = item.imagen;
                vp.nombre = item.nombre;


                lista.Add(vp);
            }

            return Ok(lista);

        }

        [HttpGet("details/{id}")]
        public async Task<ActionResult<Personaje>> GetPersonaje(int id)
        {
            var pers = await _context.Personaje.FindAsync(id);

            var pp = _context.Personaje.Where(p => p.Id == id).Include(x => x.peliculas).ThenInclude(y => y.Personajes).FirstOrDefault();

            if (pp != null)
            {
                foreach (var item in pp.peliculas)
                {
                    item.Personajes = null;
                    pers.peliculas.Add(item);
                }
            }
            if (pers == null)
            {
                return NotFound();
            }

            return Ok(pers);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> PutPersonaje(int id, Personaje personaje)
        {
            if (id != personaje.Id)
            {
                return BadRequest();
            }

            _context.Entry(personaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Personaje>> PostPersonaje(Personaje personaje)
        {
            _context.Personaje.Add(personaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonaje", new { id = personaje.Id }, personaje);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePersonaje(int id)
        {
            var personaje = await _context.Personaje.FindAsync(id);
            if (personaje == null)
            {
                return NotFound("No existe");
            }

            _context.Personaje.Remove(personaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        [HttpGet("characters/name={nombre}")]
        public ActionResult<IEnumerable<Personaje>> FiltroNombre(string nombre)
        {
            var lista = _context.Personaje.Where(p => p.nombre.Contains(nombre));
            if (!lista.Any())
            {
                return NotFound();
            }
            return Ok(lista);

        }


        [HttpGet("characters/movie={idMovie}")]
        public async Task<ActionResult<IEnumerable<Personaje>>> FiltroPeliculaAsync(int idMovie)
        {
            var lista = await _context.Personaje.ToArrayAsync();
            var pp = _context.Pelicula.Where(p => p.Id == idMovie).Include(x => x.Personajes).ThenInclude(y => y.peliculas).FirstOrDefault();
            var lista2 = new List<Personaje>();

            if (pp != null)
            {
                foreach (var item in pp.Personajes)
                {
                    item.peliculas = null;
                    lista2.Add(item);
                }
            }

            if (!lista2.Any())
            {
                return NotFound();
            }

            return Ok(lista2);

        }

        [HttpGet("characters/age={edad}")]
        public ActionResult<IEnumerable<Personaje>> FiltroEdad(int edad)
        {
            var lista = _context.Personaje.Where(p => p.edad == edad);
            if (!lista.Any())
            {
                return NotFound();
            }
            return Ok(lista);

        }


        private bool PersonajeExists(int id)
        {
            return _context.Personaje.Any(e => e.Id == id);
        }




    }
}
