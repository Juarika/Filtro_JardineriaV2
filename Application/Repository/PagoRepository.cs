using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class PagoRepository : GenericRepository<Pago>, IPago
{
    private readonly DbAppContext _context;

    public PagoRepository(DbAppContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pago>> GetPorAño(int _anio, string _formaPago)
    {
        var entities = await _context
            .Pagos
            .Where(e => e.FechaPago.Year == _anio && e.FormaPago.ToLower() == _formaPago.ToLower())
            .OrderByDescending(e => e.FechaPago)
            .ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<object>> GetFormasPago()
    {
        var entities = await _context
            .Pagos
            .Select(e => e.FormaPago)
            .Distinct()
            .ToListAsync();
        return entities;
    }
}