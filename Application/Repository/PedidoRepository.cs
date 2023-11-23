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

    
}