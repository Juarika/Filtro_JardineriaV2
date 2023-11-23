using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class OficinaRepository : GenericRepository<Oficina>, IOficina
{
    private readonly DbAppContext _context;

    public OficinaRepository(DbAppContext context)
        : base(context)
    {
        _context = context;
    }

    
}