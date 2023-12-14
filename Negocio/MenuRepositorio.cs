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
    public class MenuRepositorio : Repositorio<Menu, MenuDTO>
    {
        public MenuRepositorio(string conexion) : base(conexion)
        {

        }
        public override Menu Actualizar(Menu entidad)
        {
            var tipoEventoExistente = _conexion.Menus.FirstOrDefault(e => e.Idmenu == entidad.Idmenu);

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
                throw new Exception("El menu con ID " + entidad.Idmenu + " no fue encontrado.");
            }
            return tipoEventoExistente;
        }

        public override Menu Crear(Menu entidad)
        {
            EntityEntry<Menu> inserted = _conexion.Menus.Add(entidad);
            _conexion.SaveChanges();
            return inserted.Entity;
        }

        public override Menu Eliminar(Menu entidad)
        {
            if (entidad is null)
            {
                throw new Exception("El menu enviado es nulo.");
            }
            var tipoEventoExistente = _conexion.Menus.FirstOrDefault(e => e.Idmenu == entidad.Idmenu);

            if (tipoEventoExistente != null)
            {
                _conexion.Menus.Remove(tipoEventoExistente);
                _conexion.SaveChanges();
            }
            else
            {
                throw new Exception("El menu con ID " + entidad.Idmenu + " no fue encontrado.");
            }
            return entidad;
        }

        public override List<MenuDTO> Listar()
        {
            List<MenuDTO> lista = new List<MenuDTO>();

            var query = _conexion.Menus;
            query.OrderBy(joinResult => joinResult.Nombre);
            lista = query
                .Select(joinResult => new
                MenuDTO
                {
                    Idmenu = joinResult.Idmenu,
                    Nombre = joinResult.Nombre
                })
                .ToList();

            return lista;
        }

        public override List<MenuDTO> Listar(Dictionary<string, object> parametros)
        {
            List<MenuDTO> lista = new List<MenuDTO>();
            int idevento = int.Parse(parametros.GetValueOrDefault("idevento", "0").ToString());

            var query = _conexion.Menus.Where(p => p.Idevento == idevento);
            query.OrderBy(joinResult => joinResult.Nombre);
            lista = query
                .Select(joinResult => new
                MenuDTO
                {
                    Idmenu = joinResult.Idmenu,
                    Nombre = joinResult.Nombre
                })
                .ToList();

            return lista;
        }

    }
}
