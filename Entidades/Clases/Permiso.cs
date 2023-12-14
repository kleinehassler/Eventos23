using System;
using System.Collections.Generic;

namespace BDEventos.Models;

public partial class Permiso
{
    public int Idpermiso { get; set; }

    public int? Idperfil { get; set; }

    public int? IdopcionMenu { get; set; }


    /*
    public virtual OpcionMenu? IdopcionMenuNavigation { get; set; }

    public virtual Perfil? IdperfilNavigation { get; set; }
    */
}


public partial class PermisoDTO:Permiso
{
    public string OpcionMenu { get; set; }

    public string Link { get; set; }

    public int? IdopcionMenu_ref { get; set; }
}