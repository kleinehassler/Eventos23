using BDEventos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class TipoEventoRepositorio : Repositorio<TipoEvento, TipoEventoDTO>
    {
        public TipoEventoRepositorio(string conexion) : base(conexion)
        {

        }
        public override TipoEvento Actualizar(TipoEvento entidad)
        {
            var tipoEventoExistente = _conexion.TipoEventos.FirstOrDefault(e => e.IdtipoEvento == entidad.IdtipoEvento);

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
                throw new Exception("El tipo de evento con ID " + entidad.IdtipoEvento + " no fue encontrado.");
            }
            return tipoEventoExistente;
        }

        public override TipoEvento Crear(TipoEvento entidad)
        {
            EntityEntry<TipoEvento> inserted = _conexion.TipoEventos.Add(entidad);
            _conexion.SaveChanges();
            return inserted.Entity;
        }

        public override TipoEvento Eliminar(TipoEvento entidad)
        {
            if (entidad is null)
            {
                throw new Exception("El tipo evento enviado es nulo.");
            }
            var tipoEventoExistente = _conexion.TipoEventos.FirstOrDefault(e => e.IdtipoEvento == entidad.IdtipoEvento);

            if (tipoEventoExistente != null)
            {
                _conexion.TipoEventos.Remove(tipoEventoExistente);
                _conexion.SaveChanges();
            }
            else
            {
                throw new Exception("El tipo evento con ID " + entidad.IdtipoEvento + " no fue encontrado.");
            }
            return entidad;
        }

        public override List<TipoEventoDTO> Listar()
        {
            List<TipoEventoDTO> lista = new List<TipoEventoDTO>();


            lista = this.Cast<List<TipoEventoDTO>>(_conexion.UspConsultarTipoEventos.FromSqlRaw("EXEC EVENTO.UspConsultarTipoEvento").ToList());



            /*var query = _conexion.TipoEventos;
            query.OrderBy(joinResult => joinResult.Nombre);
            lista = query
                .Select(joinResult => new
                TipoEventoDTO
                {
                    IdtipoEvento = joinResult.IdtipoEvento,
                    Nombre = joinResult.Nombre
                })
                .ToList();*/

            return lista;
        }

        public override List<TipoEventoDTO> Listar(Dictionary<string, object> parametros)
        {
            throw new NotImplementedException();
        }

    }
}
