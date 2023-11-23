using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class ProductoRepository : GenericRepository<Producto>, IProducto
{
    private readonly DbAppContext _context;

    public ProductoRepository(DbAppContext context)
        : base(context)
    {
        _context = context;
    }

    
}