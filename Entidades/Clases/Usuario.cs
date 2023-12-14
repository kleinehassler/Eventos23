using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string? Usuario1 { get; set; }

    public string? Clave { get; set; }

    public int Idperfil { get; set; }

    public string? Estado { get; set; }

    /*
    public virtual Perfil IdperfilNavigation { get; set; } = null!;
    */
}


public partial class UsuarioDTO
{
    public int Idusuario { get; set; }

    public string Usuario1 { get; set; } = null!;

    public int Idperfil { get; set; }

    public string Estado { get; set; } = null!;

    public string? token { get; set; }

    /*
    public virtual Perfil IdperfilNavigation { get; set; } = null!;
    */
}
