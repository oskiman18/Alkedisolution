using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alkedis.Modelos
{
    public class DB : DbContext
    {

        
        public DB(DbContextOptions<DB> options) : base(options)
        {
             
        }
        public DbSet<Personaje> Personaje { get; set; }
        public DbSet<Pelicula> Pelicula { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    
    }

    public class Personaje
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string nombre { get; set; }
        [Required]
        public int edad { get; set; }
        [Required]
        public float peso { get; set; }
        [Required]
        [StringLength(300)]
        public string historia { get; set; }
        [Required]
        [StringLength(100)]
        public string imagen { get; set; }
        public ICollection<Pelicula> peliculas { get; set; }

    }
    
    public class Pelicula
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string titulo { get; set; }
        [Required]
        public DateTime fCreacion { get; set; }
        [Required]
        [Range(1, 5)]
        public int calif { get; set; }
        [Required]
        [StringLength(100)]
        public string imagen { get; set; }
        [Required]
        public int generoId { get; set; }
        public ICollection<Personaje> Personajes { get; set; }
        
    }
 

    public class Genero
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string nombre { get; set; }
        [Required]
        [StringLength(100)]
        public string imagen { get; set; }
        public ICollection<Pelicula> Peliculas { get; set; }
       
           
    }
    public class Usuario
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string mail { get; set; }
        [Required]
        public string pass { get; set; }
        public string token { get; set; }
        [Required]
        [StringLength(30)]
        public string nombre { get; set; }
        [Required]
        [StringLength(30)]
        public string apellido { get; set; }
    }

}

