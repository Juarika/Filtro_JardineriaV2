using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class PedidoRepository : GenericRepository<Pedido>, IPedido
{
    private readonly DbAppContext _context;

    public PedidoRepository(DbAppContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> GetPedidosEstado()
    {
        var entities = await _context
            .Pedidos
            .GroupBy(e => e.Estado)
            .Select(e => new 
                {
                    Estado = e.Key,
                    TotalPedidos = e.Count()
                }
            )
            .OrderByDescending(e => e.TotalPedidos)
            .ToListAsync();
        return entities;
    }
}