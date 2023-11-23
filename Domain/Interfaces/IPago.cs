using Domain.Entities;

namespace Domain.Interfaces;

public interface IPago : IGenericRepository<Pago>
{
    Task<IEnumerable<Pago>> GetPorAño(int _anio, string _formaPago);
    Task<IEnumerable<object>> GetFormasPago();
}
