using Microsoft.EntityFrameworkCore;
using Heladeria.Data.DataContext;
using Heladeria.Data.Entidades;

namespace Heladeria.Data.Repositorios
{
    public class SaborRepositorio : ISaborRepositorio
    {
        private readonly HeladeriaDbContext _context;

        public SaborRepositorio(HeladeriaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sabor>> ObtenerTodosAsync()
        {
            return await _context.Sabores.Include(s => s.Categoria).ToListAsync();
        }

        public async Task<Sabor?> ObtenerPorIdAsync(int id)
        {
            return await _context.Sabores.Include(s => s.Categoria).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AgregarAsync(Sabor sabor)
        {
            await _context.Sabores.AddAsync(sabor);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Sabor sabor)
        {
            _context.Sabores.Update(sabor);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var sabor = await _context.Sabores.FindAsync(id);
            if (sabor != null)
            {
                _context.Sabores.Remove(sabor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Categoria>> ObtenerCategoriasAsync()
        {
            return await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync();
        }

    }
}

