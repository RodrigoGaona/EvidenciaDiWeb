using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models; // Cambiado aquí

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieDbContext _context;

        // Constructor para inyectar el contexto de la base de datos
        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        // Acción GET para obtener todas las películas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // Acción GET para obtener una película por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();  // Retorna un 404 si la película no se encuentra
            }

            return movie;  // Retorna la película encontrada
        }

        // Acción POST para agregar una nueva película
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // Acción PUT para actualizar una película
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();  // Si el ID no coincide con la película, retorna un error 400
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();  // Intenta guardar los cambios
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();  // Si no encuentra la película, retorna un 404
                }
                else
                {
                    throw;
                }
            }

            return NoContent();  // Si todo va bien, retorna un 204 (sin contenido)
        }

        // Acción DELETE para eliminar una película
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();  // Si la película no se encuentra, retorna un 404
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();  // Si todo va bien, retorna un 204 (sin contenido)
        }

        // Método auxiliar para verificar si una película existe
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
