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
    public class InvitadoRepositorio : Repositorio<Invitado, InvitadoDTO>
    {
        public InvitadoRepositorio(string conexion) : base(conexion)
        {

        }
        public override Invitado Actualizar(Invitado entidad)
        {
            //Valida si existe
            var eventoExistente = _conexion.Invitados.FirstOrDefault(e => e.Idevento == entidad.Idevento && e.Idinvitado == entidad.Idinvitado);
            if (eventoExistente != null)
            {
                //Realizar los cambios en los campos deseados
                if (entidad.Nombres != null)
                    eventoExistente.Nombres = entidad.Nombres;
                if (entidad.Apellidos != null)
                    eventoExistente.Apellidos = entidad.Apellidos;
                if (entidad.Correo != null)
                    eventoExistente.Correo = entidad.Correo;
                if (entidad.Telefono != null)
                    eventoExistente.Telefono = entidad.Telefono;
                if (entidad.Comentario != null)
                    eventoExistente.Comentario = entidad.Comentario;
                if (entidad.Idmenu != null)
                    eventoExistente.Idmenu = entidad.Idmenu;
                if (entidad.Idgrupo != null)
                    eventoExistente.Idgrupo = entidad.Idgrupo;
                if (entidad.Idmesa != null)
                    eventoExistente.Idmesa = entidad.Idmesa;
                if (entidad.Direccion != null)
                    eventoExistente.Direccion = entidad.Direccion;
                if (entidad.Sexo != null)
                    eventoExistente.Sexo = entidad.Sexo;
                if (entidad.TipoInvitado != null)
                    eventoExistente.TipoInvitado = entidad.TipoInvitado;
                if (entidad.InvitacionEnviada != null)
                    eventoExistente.InvitacionEnviada = entidad.InvitacionEnviada;
                if (entidad.Estado != null)
                    eventoExistente.Estado = entidad.Estado;
                //Guardar los cambios en la base de datos
                _conexion.SaveChanges();
            }
            else
            {
                // Paso 4: Generar una excepción si el evento no existe
                throw new Exception("El invitado con ID " + entidad.Idevento + " no fue encontrado.");
            }
            return eventoExistente;
        }

        public override Invitado Crear(Invitado entidad)
        {
            EntityEntry<Invitado> inserted = _conexion.Invitados.Add(entidad);
            _conexion.SaveChanges();
            return inserted.Entity;
        }

        public override Invitado Eliminar(Invitado entidad)
        {
            //Valida si no envia datos
            if (entidad is null)
            {
                throw new Exception("El invitado enviado es nulo.");
            }
            //Valida si existe elemento
            var eventoExistente = _conexion.Invitados.FirstOrDefault(e => e.Idinvitado == entidad.Idinvitado);
            if (eventoExistente != null)
            {
                //Pepara la eliminacion del elemento
                _conexion.Invitados.Remove(eventoExistente);
                //Actualiza los cambios en la BD
                _conexion.SaveChanges();
            }
            else
            {
                throw new Exception("El invitado con ID " + entidad.Idinvitado + " no fue encontrado.");
            }
            return entidad;
        }

        public override List<InvitadoDTO> Listar(Dictionary<string, object> dict)
        {

            List<InvitadoDTO> lista = new List<InvitadoDTO>();
            lista = _conexion.Invitados
                .Select(Result => new
                InvitadoDTO
                {
                    Idinvitado = Result.Idinvitado,
                    Nombres = Result.Nombres
                })
                .ToList();
            return lista;
        }

        public override List<InvitadoDTO> Listar()
        {
            throw new NotImplementedException();
        }

    }
}
