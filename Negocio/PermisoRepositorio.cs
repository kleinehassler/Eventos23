using BDEventos.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class PemrisoRepositorio : Repositorio<Permiso, PermisoDTO>
    {
        public PemrisoRepositorio(string conexion) : base(conexion)
        {

        }
        public override Permiso Actualizar(Permiso entidad)
        {
            throw new NotImplementedException();
        }

        public override Permiso Crear(Permiso entidad)
        {
            throw new NotImplementedException();
        }

        public override Permiso Eliminar(Permiso entidad)
        {
            throw new NotImplementedException();
        }

        public override List<PermisoDTO> Listar(Dictionary<string, object> parametros)
        {
            List<PermisoDTO> lista = new List<PermisoDTO>();
            var pagina = int.Parse(parametros["pagina"].ToString());
            var elementosPorPagina = int.Parse(parametros["registros"].ToString());
            var filtro = int.Parse(parametros["filtro"].ToString());
            var orden = parametros["orden"].ToString();
            var direccion = parametros["direccion"].ToString();
            //lista = _conexion.Permisos.Include(c=>c.IdtipoPermisoNavigation).ToList();
            var query = _conexion.Permisos.Join(
                _conexion.OpcionMenus,
                Permiso => Permiso.IdopcionMenu,
                OpcionMenu => OpcionMenu.IdopcionMenu,
                (Permiso, OpcionMenu) => new { Permiso = Permiso, OpcionMenu = OpcionMenu });

            query = query.Where(joinResult => joinResult.OpcionMenu.Estado == "ACTIVO");

            // Aplicar el filtro dinámico
            if (filtro > 0)
            {
                query = query.Where(joinResult => joinResult.Permiso.Idperfil == filtro);
            }

            // Aplicar el ordenamiento dinámico

            switch (orden)
            {
                case "idpermiso":
                    if (direccion == "DESC")
                        query = query.OrderByDescending(joinResult => joinResult.Permiso.Idpermiso);
                    else
                        query = query.OrderBy(joinResult => joinResult.Permiso.Idpermiso);
                    break;
                case "OpcionMenu":
                    if (direccion == "DESC")
                        query = query.OrderByDescending(joinResult => joinResult.OpcionMenu.Nombre);
                    else
                        query = query.OrderBy(joinResult => joinResult.OpcionMenu.Nombre);
                    break;
            }

            lista = query
                .Skip((pagina - 1) * elementosPorPagina)
                .Take(elementosPorPagina)
                .Select(joinResult => new
                PermisoDTO
                {
                    Idpermiso = joinResult.Permiso.Idpermiso,
                    IdopcionMenu = joinResult.Permiso.IdopcionMenu,
                    OpcionMenu = joinResult.OpcionMenu.Nombre,
                    Link = joinResult.OpcionMenu.Link,
                    IdopcionMenu_ref = joinResult.OpcionMenu.IdopcionMenuRef
                })
                .ToList();

            return lista;
        }

        public override List<PermisoDTO> Listar()
        {
            throw new NotImplementedException();
        }

    }
}
