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
    public class UsuarioRepositorio : Repositorio<Usuario, UsuarioDTO>
    {
        public UsuarioRepositorio(string conexion) : base(conexion)
        {

        }
        public override Usuario Actualizar(Usuario entidad)
        {
            var existe = _conexion.Usuarios.FirstOrDefault(p => p.Idusuario == entidad.Idusuario);
            if (existe != null)
            {

                if (entidad.Usuario1 != null)
                    existe.Usuario1 = entidad.Usuario1;

                if (entidad.Clave != null)
                    existe.Clave = obtenerClave(entidad.Clave);

                _conexion.SaveChanges();
            }
            else
            {
                throw new Exception("EL id no existe " + entidad.Idusuario);
            }
            return existe;
        }

        public override Usuario Crear(Usuario entidad)
        {
            EntityEntry<Usuario> insert = _conexion.Add(entidad);
            _conexion.SaveChanges();
            return insert.Entity;
        }

        public override Usuario Eliminar(Usuario entidad)
        {
            var existe = _conexion.Usuarios.FirstOrDefault(p => p.Idusuario == entidad.Idusuario);
            if (existe != null)
            {
                _conexion.Usuarios.Remove(existe);
                _conexion.SaveChanges();
            }
            else
            {
                throw new Exception("EL id no existe " + entidad.Idusuario);
            }
            return existe;
        }

        public override List<UsuarioDTO> Listar(Dictionary<string, object> parametros)
        {
            //Para paginar
            int pagina = int.Parse(parametros.GetValueOrDefault("pagina", "0").ToString());
            int registros = int.Parse(parametros.GetValueOrDefault("registros", "0").ToString());


            //Filtra por codigo
            int idusuario = int.Parse(parametros.GetValueOrDefault("idusuario", "0").ToString());

            //Ordenamiento
            string orden = parametros.GetValueOrDefault("orden", "1").ToString();
            string direccion = parametros.GetValueOrDefault("direccion", "ASC").ToString();

            List<UsuarioDTO> lista;

            //Usuario join con Tipo de Usuario
            var query = _conexion.Usuarios.AsQueryable();


            //Filtra por id
            if (idusuario > 0)
            {
                query = query.Where(p => p.Idusuario == idusuario);
            }

            switch (orden)
            {
                case "usuario":
                    if (direccion == "ASC")
                        query = query.OrderBy(p => p.Usuario1);
                    else
                        query = query.OrderByDescending(p => p.Usuario1);
                    break;
                case "idusuario":
                    if (direccion == "ASC")
                        query = query.OrderBy(p => p.Idusuario);
                    else
                        query = query.OrderByDescending(p => p.Idusuario);
                    break;
            }

            lista = query
                .Skip((pagina - 1) * registros)
                .Take(registros)
                .Select(p => new UsuarioDTO
                {
                    Idusuario = p.Idusuario,
                    Usuario1 = p.Usuario1
                }
                )
                .ToList();

            return lista;
        }


        public List<UsuarioDTO> Autenticar(string usuario, string clave)
        {

            List<UsuarioDTO> lista;

            //Usuario join con Tipo de Usuario
            var query = _conexion.Usuarios.AsQueryable();

            var nueva_clave = obtenerClave(clave);

            query = query.Where(p => p.Usuario1 == usuario && p.Clave == nueva_clave);




            lista = query
                .Select(p => new UsuarioDTO
                {
                    Idusuario = p.Idusuario,
                    Usuario1 = p.Usuario1
                }
                )
                .ToList();

            return lista;
        }

        public string obtenerClave(string clave)
        {
            // Generar una sal única para el usuario
            //string Sal = GenerarSal();

            // Concatenar la contraseña y la sal
            string claveConSal = clave;

            // Calcular el hash SHA-256 de la contraseña + sal
            string ClaveHash = CalcularHashSHA256(claveConSal);
            return ClaveHash;
        }

        private string CalcularHashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convertir el hash a una representación de cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public override List<UsuarioDTO> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
