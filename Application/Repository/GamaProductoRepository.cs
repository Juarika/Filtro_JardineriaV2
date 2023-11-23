using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class GamaProductoRepository : GenericRepository<GamaProducto>, IGamaProducto
{
    private readonly DbAppContext _context;

    public GamaProductoRepository(DbAppContext context)
        : base(context)
    {
        _context = context;
    }

    
}