namespace VerduleriaApi.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdTipo { get; set; }
        public List<Usuario>? Usuarios { get; set; }
    }
}
