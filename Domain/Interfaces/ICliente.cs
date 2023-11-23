using Domain.Entities;

namespace Domain.Interfaces;

public interface ICliente : IGenericRepository<Cliente>
{
    Task<IEnumerable<object>> GetRepOfi();
    Task<IEnumerable<Cliente>> GetSinPago();
    Task<IEnumerable<object>> GetRepOfiTodos();
    Task<IEnumerable<object>> GetRepOfiTel();
}
