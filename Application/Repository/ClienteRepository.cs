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

    public async Task<IEnumerable<object>> GetRepOfi()
    {
        var entities = await _context
            .Clientes
            .Where(e => e.Pagos.Any())
            .Select(e => new
            {
                NombreCliente = e.NombreCliente,
                NombreRepresentante = $"{e.EmpleadoRepVentas.Nombre} {e.EmpleadoRepVentas.Apellido1}",
                Oficina = e.EmpleadoRepVentas.Oficina.Ciudad
            })
            .ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<Cliente>> GetSinPago()
    {
        var entities = await _context
            .Clientes
            .Where(e => !_context.Pagos.Any(d => d.CodigoCliente == e.CodigoCliente))
            .ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<object>> GetRepOfiTodos()
    {
        var entities = await _context
            .Clientes
            .Select(e => new
            {
                NombreCliente = e.NombreCliente,
                NombreRepresentante = $"{e.EmpleadoRepVentas.Nombre} {e.EmpleadoRepVentas.Apellido1}",
                Oficina = e.EmpleadoRepVentas.Oficina.Ciudad
            })
            .ToListAsync();
        return entities;
    }

    public async Task<IEnumerable<object>> GetRepOfiTel()
    {
        var entities = await _context
            .Clientes
            .Where(e => !_context.Pagos.Any(d => d.CodigoCliente == e.CodigoCliente))
            .Select(e => new
            {
                NombreCliente = e.NombreCliente,
                NombreRepresentante = $"{e.EmpleadoRepVentas.Nombre} {e.EmpleadoRepVentas.Apellido1}",
                TelefonoOficina = e.EmpleadoRepVentas.Oficina.Telefono
            })
            .ToListAsync();
        return entities;
    }
}