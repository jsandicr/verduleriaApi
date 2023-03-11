using Microsoft.EntityFrameworkCore;
using VerduleriaApi.Models;

namespace VerduleriaApi.Models
{
    public class VerduleriaContext : DbContext
    {
        public VerduleriaContext(DbContextOptions<VerduleriaContext> opciones)
            : base(opciones)
        {

        }

        //Entidades
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Promocion> Promocion { get; set; }
    }
}
