using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class ClienteRepository : GenericRepository<Cliente>, ICliente
{
    private readonly DbAppContext _context;

    public ClienteRepository(DbAppContext context)
        : base(context)
    {
        _context = context;
    }

    
}