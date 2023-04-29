namespace VerduleriaApi.Models
{
    public class RolModel : IRolModel
    {
        private readonly VerduleriaContext _context;

        public RolModel(VerduleriaContext context)
        {
            _context = context;
        }

        public Rol GetRolById(int id)
        {
            try
            {
                return _context.Rol.Find(id);
            }
            catch (Exception)
            {
                return new Rol();
            }
        }

        public List<Rol> GetRoles()
        {
            try
            {
                return _context.Rol.ToList();
            }
            catch (Exception)
            {
                return new List<Rol>();
            }
        }

        public Rol PostRol(Rol rol)
        {
            try
            {
                _context.Rol.Add(rol);
                _context.SaveChanges();
                return rol;
            }
            catch (Exception)
            {
                return new Rol();
            }
        }

        public Rol PutRol(Rol rol)
        {
            try
            {
                _context.Update(rol);
                _context.SaveChanges();
                return rol;
            }
            catch (Exception)
            {
                return new Rol();
            }
        }
    }
}
