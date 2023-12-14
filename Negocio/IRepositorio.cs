namespace Negocio
{
    public interface IRepositorio<T,DTO>
    {
        //Listados
        List<DTO> Listar(Dictionary<string, object> parametros);
        List<DTO> Listar();

        //CRUD
        T Crear(T entidad);
        T Actualizar(T entidad);
        T Eliminar(T entidad);

    }
}