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
    public class MesaRepositorio : Repositorio<Mesa, MesaDTO>
    {
        public MesaRepositorio(string conexion) : base(conexion)
        {

        }
        public override Mesa Actualizar(Mesa entidad)
        {
            var tipoEventoExistente = _conexion.Mesas.FirstOrDefault(e => e.Idmesa == entidad.Idmesa);

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
                throw new Exception("La mesa con ID " + entidad.Idmesa + " no fue encontrado.");
            }
            return tipoEventoExistente;
        }

        public override Mesa Crear(Mesa entidad)
        {
            EntityEntry<Mesa> inserted = _conexion.Mesas.Add(entidad);
            _conexion.SaveChanges();
            return inserted.Entity;
        }

        public override Mesa Eliminar(Mesa entidad)
        {
            if (entidad is null)
            {
                throw new Exception("La mesa enviada es nula.");
            }
            var tipoEventoExistente = _conexion.Mesas.FirstOrDefault(e => e.Idmesa == entidad.Idmesa);

            if (tipoEventoExistente != null)
            {
                _conexion.Mesas.Remove(tipoEventoExistente);
                _conexion.SaveChanges();
            }
            else
            {
                throw new Exception("La mesa con ID " + entidad.Idmesa + " no fue encontrado.");
            }
            return entidad;
        }

        public override List<MesaDTO> Listar()
        {
            List<MesaDTO> lista = new List<MesaDTO>();

            var query = _conexion.Mesas;
            query.OrderBy(joinResult => joinResult.Nombre);
            lista = query
                .Select(joinResult => new
                MesaDTO
                {
                    Idmesa = joinResult.Idmesa,
                    Nombre = joinResult.Nombre
                })
                .ToList();

            return lista;
        }

        public override List<MesaDTO> Listar(Dictionary<string, object> parametros)
        {
            List<MesaDTO> lista = new List<MesaDTO>();
            int idevento = int.Parse(parametros.GetValueOrDefault("idevento", "0").ToString());

            var query = _conexion.Mesas.Where(p => p.Idevento == idevento);
            query.OrderBy(joinResult => joinResult.Nombre);
            lista = query
                .Select(joinResult => new
                MesaDTO
                {
                    Idmesa = joinResult.Idmesa,
                    Nombre = joinResult.Nombre
                })
                .ToList();

            return lista;
        }

    }
}
