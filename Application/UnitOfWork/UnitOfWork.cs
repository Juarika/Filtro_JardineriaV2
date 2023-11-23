using Application.Repository;
using Domain.Interfaces;
using Persistence.Data;


namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DbAppContext _context;
    private ICliente _Clientes;
    private IDetallePedido _DetallePedidos;
    private IEmpleado _Empleados;
    private IGamaProducto _GamaProductos;
    private IOficina _Oficinas;
    private IPago _Pagos;
    private IPedido _Pedidos;
    private IProducto _Productos;

    
    
    public UnitOfWork(DbAppContext context)
    {
        _context = context;
    }

    public ICliente Clientes
    {
        get
        {
            if(_Clientes == null)
            {
                _Clientes = new ClienteRepository(_context);
            }
            return _Clientes;
        }
    }

    public IDetallePedido DetallePedidos
    {
        get
        {
            if(_DetallePedidos == null)
            {
                _DetallePedidos = new DetallePedidoRepository(_context);
            }
            return _DetallePedidos;
        }
    }

    public IEmpleado Empleados
    {
        get
        {
            if(_Empleados == null)
            {
                _Empleados = new EmpleadoRepository(_context);
            }
            return _Empleados;
        }
    }

    public IGamaProducto GamaProductos
    {
        get
        {
            if(_GamaProductos == null)
            {
                _GamaProductos = new GamaProductoRepository(_context);
            }
            return _GamaProductos;
        }
    }

    public IOficina Oficinas
    {
        get
        {
            if(_Oficinas == null)
            {
                _Oficinas = new OficinaRepository(_context);
            }
            return _Oficinas;
        }
    }

    public IPago Pagos
    {
        get
        {
            if(_Pagos == null)
            {
                _Pagos = new PagoRepository(_context);
            }
            return _Pagos;
        }
    }

    public IPedido Pedidos
    {
        get
        {
            if(_Pedidos == null)
            {
                _Pedidos = new PedidoRepository(_context);
            }
            return _Pedidos;
        }
    }

    public IProducto Productos
    {
        get
        {
            if(_Productos == null)
            {
                _Productos = new ProductoRepository(_context);
            }
            return _Productos;
        }
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    
}