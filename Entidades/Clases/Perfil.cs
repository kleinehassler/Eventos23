using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class Perfil
{
    public int Idperfil { get; set; }

    public string? Nombre { get; set; }

    public string? Estado { get; set; }

    /*
    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    */
}
