using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class DetallePedidoRepository : GenericRepository<DetallePedido>, IDetallePedido
{
    private readonly DbAppContext _context;

    public DetallePedidoRepository(DbAppContext context)
        : base(context)
    {
        _context = context;
    }

    
}