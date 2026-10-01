using Heladeria.Data.Entidades;

namespace Heladeria.Data.Repositorios
{
    public interface ISaborRepositorio
    {
        Task<IEnumerable<Sabor>> ObtenerTodosAsync();
        Task<Sabor?> ObtenerPorIdAsync(int id);
        Task AgregarAsync(Sabor sabor);
        Task ActualizarAsync(Sabor sabor);
        Task EliminarAsync(int id);
        Task<IEnumerable<Categoria>> ObtenerCategoriasAsync();

    }
}

