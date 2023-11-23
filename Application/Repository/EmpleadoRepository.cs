using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repository;

public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleado
{
    private readonly DbAppContext _context;

    public EmpleadoRepository(DbAppContext context)
        : base(context)
    {
        _context = context;
    }

    
}