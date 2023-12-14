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
    public class GrupoRepositorio : Repositorio<Grupo, GrupoDTO>
    {
        public GrupoRepositorio(string conexion) : base(conexion)
        {

        }
        public override Grupo Actualizar(Grupo entidad)
        {
            var tipoEventoExistente = _conexion.Grupos.FirstOrDefault(e => e.Idgrupo == entidad.Idgrupo);

            if (tipoEventoExistente != null)
            {
                // Paso 2: Realizar los cambios en los campos deseados
                if (entidad.Nombre != null)
                {
                    tipoEventoExistente.Nombre = entidad.Nombre;
                }
                if (entidad.Comentario != null)
                {
                    tipoEventoExistente.Comentario = entidad.Comentario;
                }

                // Paso 3: Guardar los cambios en la base de datos
                _conexion.SaveChanges();
            }
            else
            {
                // Paso 4: Generar una excepción si el evento no existe
                throw new Exception("El grupo con ID " + entidad.Idgrupo + " no fue encontrado.");
            }
            return tipoEventoExistente;
        }

        public override Grupo Crear(Grupo entidad)
        {
            EntityEntry<Grupo> inserted = _conexion.Grupos.Add(entidad);
            _conexion.SaveChanges();
            return inserted.Entity;
        }

        public override Grupo Eliminar(Grupo entidad)
        {
            if (entidad is null)
            {
                throw new Exception("El grupo enviado es nulo.");
            }
            var tipoEventoExistente = _conexion.Grupos.FirstOrDefault(e => e.Idgrupo == entidad.Idgrupo);

            if (tipoEventoExistente != null)
            {
                _conexion.Grupos.Remove(tipoEventoExistente);
                _conexion.SaveChanges();
            }
            else
            {
                throw new Exception("El grupo con ID " + entidad.Idgrupo + " no fue encontrado.");
            }
            return entidad;
        }

        public override List<GrupoDTO> Listar()
        {
            List<GrupoDTO> lista = new List<GrupoDTO>();

            var query = _conexion.Grupos;
            query.OrderBy(joinResult => joinResult.Nombre);
            lista = query
                .Select(joinResult => new
                GrupoDTO
                {
                    Idgrupo = joinResult.Idgrupo,
                    Nombre = joinResult.Nombre
                })
                .ToList();

            return lista;
        }

        public override List<GrupoDTO> Listar(Dictionary<string, object> parametros)
        {
            List<GrupoDTO> lista = new List<GrupoDTO>();
            int idevento = int.Parse(parametros.GetValueOrDefault("idevento", "0").ToString());

            var query = _conexion.Grupos.Where(p => p.Idevento == idevento);
            query.OrderBy(joinResult => joinResult.Nombre);
            lista = query
                .Select(joinResult => new
                GrupoDTO
                {
                    Idgrupo = joinResult.Idgrupo,
                    Nombre = joinResult.Nombre
                })
                .ToList();

            return lista;
        }

    }
}
