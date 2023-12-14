using BDEventos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class EventoRepositorio : Repositorio<Evento, EventoDTO>
    {
        public EventoRepositorio(string conexion) : base(conexion)
        {

        }
        public override Evento Actualizar(Evento entidad)
        {
            var existe = _conexion.Eventos.FirstOrDefault(p => p.Idevento == entidad.Idevento);
            if (existe != null)
            {

                if (entidad.IdtipoEvento != null)
                    existe.IdtipoEvento = entidad.IdtipoEvento;
                if (entidad.Nombre != null)
                    existe.Nombre = entidad.Nombre;
                if (entidad.Fecha != null)
                    existe.Fecha = entidad.Fecha;
                if (entidad.Presupuesto != null)
                    existe.Presupuesto = entidad.Presupuesto;
                if (entidad.Comentario != null)
                    existe.Comentario = entidad.Comentario;

                _conexion.SaveChanges();
            }
            else
            {

                throw new Exception("EL id no existe " + entidad.Idevento);
            }
            return existe;
        }

        public override Evento Crear(Evento entidad)
        {
            using var transaction = _conexion.Database.BeginTransaction();
            try
            {

                //Agrega los datos a DBSet
                EntityEntry<Evento> inserted = _conexion.Eventos.Add(entidad);
                _conexion.SaveChanges();

                //Agreag datos por defecto:
                _conexion.Grupos.Add(new Grupo() { Idevento = inserted.Entity.Idevento, Nombre = "Familia" });
                _conexion.Grupos.Add(new Grupo() { Idevento = inserted.Entity.Idevento, Nombre = "Amigos" });
                _conexion.Grupos.Add(new Grupo() { Idevento = inserted.Entity.Idevento, Nombre = "Colegas" });
                _conexion.SaveChanges();

                _conexion.Mesas.Add(new Mesa() { Idevento = inserted.Entity.Idevento, Nombre = "Mesa Principal" });
                _conexion.Mesas.Add(new Mesa() { Idevento = inserted.Entity.Idevento, Nombre = "Mesa 1" });
                _conexion.Mesas.Add(new Mesa() { Idevento = inserted.Entity.Idevento, Nombre = "Mesa 2" });
                _conexion.Mesas.Add(new Mesa() { Idevento = inserted.Entity.Idevento, Nombre = "Mesa 3" });
                _conexion.SaveChanges();

                _conexion.Menus.Add(new Menu() { Idevento = inserted.Entity.Idevento, Nombre = "Menu para adultos" });
                _conexion.Menus.Add(new Menu() { Idevento = inserted.Entity.Idevento, Nombre = "Menu infantil" });
                _conexion.Menus.Add(new Menu() { Idevento = inserted.Entity.Idevento, Nombre = "Menu vegetariano" });
                _conexion.Menus.Add(new Menu() { Idevento = inserted.Entity.Idevento, Nombre = "Menu Dietetico" });
                //Guarda los datos en la BD
                _conexion.SaveChanges();

                transaction.Commit();
                return inserted.Entity;
            }
            catch (DbUpdateException e)
            {
                // Si ocurre un error, puedes realizar acciones de compensación o simplemente hacer un rollback
                transaction.Rollback();
                throw new Exception("Hubo un error en la BD " + e.InnerException.Message);
            }
            catch (Exception e)
            {
                // Si ocurre un error, puedes realizar acciones de compensación o simplemente hacer un rollback
                transaction.Rollback();
                throw new Exception("Hubo un error en la BD " + e.Message);
            }
        }

        public override Evento Eliminar(Evento entidad)
        {

            var existeInvitados = _conexion.Invitados.Where(p => p.Idevento == entidad.Idevento).ToList();
            if (existeInvitados.Count() > 0) {
                throw new Exception("Existe " + existeInvitados.Count() + " invitado(s) para el evento a eliminar");
            }

            var existe = _conexion.Eventos.FirstOrDefault(p => p.Idevento == entidad.Idevento);
            if (existe != null)
            {
                _conexion.Eventos.Remove(existe);
                _conexion.SaveChanges();
            }
            else
            {
                throw new Exception("EL id no existe " + entidad.Idevento);
            }
            return existe;
        }

        public override List<EventoDTO> Listar(Dictionary<string, object> parametros)
        {
            //Para paginar
            int pagina = int.Parse(parametros.GetValueOrDefault("pagina", "0").ToString());
            int registros = int.Parse(parametros.GetValueOrDefault("registros", "0").ToString());


            //Filtra por codigo
            int idevento = int.Parse(parametros.GetValueOrDefault("idevento", "0").ToString());

            //Ordenamiento
            string orden = parametros.GetValueOrDefault("orden", "1").ToString();
            string direccion = parametros.GetValueOrDefault("direccion", "ASC").ToString();

            var idusuario = int.Parse(parametros.GetValueOrDefault("idusuario", "0").ToString());
            var detalle = parametros.GetValueOrDefault("detalle", "NO").ToString();

            List<EventoDTO> lista;

            //Evento join con Tipo de Evento
            var query = _conexion.Eventos.Join(
                _conexion.TipoEventos,
                Evento => Evento.IdtipoEvento,
                TipoEvento => TipoEvento.IdtipoEvento,
                (Evento, TipoEvento) => new { Evento = Evento, TipoEvento = TipoEvento }
                );


            //Filtra por id
            if (idevento > 0)
            {
                query = query.Where(p => p.Evento.Idevento == idevento);
            }


            // Aplicar el filtro dinámico - IdEvento
            if (idusuario > 0)
            {
                query = query.Where(joinResult => joinResult.Evento.Idusuario == idusuario);
            }

            switch (orden)
            {
                case "nombre":
                    if (direccion == "ASC")
                        query = query.OrderBy(p => p.Evento.Nombre);
                    else
                        query = query.OrderByDescending(p => p.Evento.Nombre);
                    break;
                case "fecha":
                    if (direccion == "ASC")
                        query = query.OrderBy(p => p.Evento.Fecha);
                    else
                        query = query.OrderByDescending(p => p.Evento.Fecha);
                    break;
            }

            lista = query
                .Skip((pagina - 1) * registros)
                .Take(registros)
                .Select(p => new EventoDTO
                {
                    Idevento = p.Evento.Idevento,
                    Nombre = p.Evento.Nombre,
                    IdtipoEvento = (int)p.TipoEvento.IdtipoEvento,
                    TipoEvento = p.TipoEvento.Nombre,
                    Fecha = p.Evento.Fecha,
                    FechaFormato = String.Format("dd/mm/yyyy",p.Evento.Fecha),
                    Presupuesto = p.Evento.Presupuesto,
                    Comentario = p.Evento.Comentario,
                    Estado = p.Evento.Estado
                }
                )
                .ToList();



            if (detalle == "SI")
            {

                lista.First().invitados = _conexion.Invitados.Where(p => p.Idevento == idevento).Select(
                    p => new InvitadoDTO
                    {
                        Idinvitado = p.Idinvitado,
                        Nombres = p.Nombres,
                        Apellidos = p.Apellidos,
                        Idevento = p.Idevento,
                        Idgrupo = p.Idgrupo,
                        Idmenu = p.Idmenu,
                        Idmesa = p.Idmesa,
                        InvitacionEnviada = p.InvitacionEnviada,
                        TipoInvitado = p.TipoInvitado,
                        Telefono = p.Telefono,
                        Correo = p.Correo,
                        Direccion = p.Direccion,
                        Estado = p.Estado,
                        Sexo = p.Sexo,
                        Comentario = p.Comentario
                    }
                    ).ToList();

                lista.First().grupos = _conexion.Grupos.Where(p => p.Idevento == idevento).Select(
                    p => new GrupoDTO
                    {
                        Idgrupo = p.Idgrupo,
                        Nombre = p.Nombre
                    }
                    ).ToList();

                lista.First().mesas = _conexion.Mesas.Where(p => p.Idevento == idevento).Select(
                    p => new MesaDTO
                    {
                        Idmesa = p.Idmesa,
                        Nombre = p.Nombre
                    }
                    ).ToList();

                lista.First().menus = _conexion.Menus.Where(p => p.Idevento == idevento).Select(
                    p => new MenuDTO
                    {
                        Idmenu = p.Idmenu,
                        Nombre = p.Nombre
                    }
                    ).ToList();
            }

            return lista;
        }

        public override List<EventoDTO> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
