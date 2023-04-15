﻿using System.ComponentModel.DataAnnotations;
namespace VerduleriaApi.Models
{
    public class Promocion
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public int IdTipo { get; set; }
        public List<ProductoPromocion>? ProductoPromocion { get; set; }
    }
}
