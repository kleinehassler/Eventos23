using BDEventos.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public abstract class Repositorio<T, DTO> : IRepositorio<T, DTO>
    {
        protected BDEventoContext _conexion;

        public Repositorio(string conexion) {
            var contextOptions = new DbContextOptionsBuilder<BDEventoContext>()
                .UseSqlServer(conexion)
                .Options;

            _conexion = new BDEventoContext(contextOptions);

        }
        public abstract List<DTO> Listar();
        public abstract List<DTO> Listar(Dictionary<string, object> parametros);


        public abstract T Crear(T entidad);

        public abstract T Actualizar(T entidad);

        public abstract T Eliminar(T entidad);

        public T1 Cast<T1>(Object DataFromBody)
         where T1 : class, new()
        {
            T1 dataResult = new T1();
            string dataJSON = JsonConvert.SerializeObject(DataFromBody);
            dataResult = JsonConvert.DeserializeObject<T1>(dataJSON);
            return dataResult;
        }
    }
}
