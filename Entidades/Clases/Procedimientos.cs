using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Clases
{
    [Keyless]
    [NotMapped]
    public class UspConsultarTipoEvento
    {
        // Propiedades que representan las columnas devueltas por el procedimiento almacenado
        public int? IdtipoEvento { get; set; }

        public string? Nombre { get; set; }

        public string? Comentario { get; set; }

        public string? Estado { get; set; }
        // ...
    }



}
