

Consultas Requeridas

1. Devuelve un listado con todos los pagos que se realizaron en el año 2008 mediante Paypal. Ordene el resultado de mayor a menor.

    ```
        http://localhost:5124/api/Pago/GetPorAnio?anio=2008&formaPago=paypal
    ```

    ```
        public async Task<IEnumerable<Pago>> GetPorAño(int _anio, string _formaPago)
        {
            var entities = await _context
                .Pagos
                .Where(e => e.FechaPago.Year == _anio && e.FormaPago.ToLower() == _formaPago.ToLower()) // Metodo para Buscar el año y la forma de pago correspondiente
                .OrderByDescending(e => e.FechaPago) // Metodo de ordenamiento, en este caso se ordeno por la fecha de pago, de forma descendiente
                .ToListAsync();
            return entities;
        }
    ```

2. Devuelve un listado con todas las formas de pago que aparecen en la tabla pago. Tenga en cuenta que no deben aparecer formas de pago repetidas.

    ```
        http://localhost:5124/api/Pago/GetFormasPago
    ```

    ```
        public async Task<IEnumerable<object>> GetFormasPago()
        {
            var entities = await _context
                .Pagos
                .Select(e => e.FormaPago) // Se selecciona solo la forma de pago de cada pago realizado
                .Distinct() // Se hace la distincion, para que solo muestre una vez cada forma de pago y no se repita
                .ToListAsync();
            return entities;
        }
    ```

3. Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus representates junto con la ciudad de la oficina a la que pertenece el representante.

    ```
        http://localhost:5124/api/Cliente/GetRepOfi
    ```

    ```
        public async Task<IEnumerable<object>> GetRepOfi()
        {
            var entities = await _context
                .Clientes
                .Where(e => e.Pagos.Any()) // Se buscan los clientes que hayan realizado algun pago, el Any ayuda a que queden los clientes que hayan realizado cualquier pago
                .Select(e => new 
                {
                    NombreCliente = e.NombreCliente,
                    NombreRepresentante = $"{e.EmpleadoRepVentas.Nombre} {e.EmpleadoRepVentas.Apellido1}",
                    Oficina = e.EmpleadoRepVentas.Oficina.Ciudad
                }) // Con el select lo que se hace es crear el objecto con los datos rewqueridos en la consulta
                .ToListAsync();
            return entities;
        }
    ```

4. Devuelve un listado que muestre el nombre de cada empleado, el nombre de su jefe y el nombre del jefe de su jefe.

    ```
        http://localhost:5124/api/Empleado/GetJefe
    ```

    ```
        public async Task<IEnumerable<Empleado>> GetJefe()
        {
            var entities = await _context
                .Empleados
                .Include(e => e.Jefe) // Me incluye los datos del jefe del empleado
                .ThenInclude(e => e.Jefe) // Me incluye los datos del jefe del jefe del empleado
                .ToListAsync();
            return entities;
        }
    ```

5. Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripcion y la imgen del producto.

    ```
        http://localhost:5124/api/Producto/GetSinPedido
    ```

    ```
        public async Task<IEnumerable<Producto>> GetSinPedido()
        {
            var entities = await _context
                .Productos
                .Where(e => !_context.DetallePedidos.Any(d => d.CodigoProducto == e.CodigoProducto)) // Busca los productos que no tengan ningun dato en la tabla DetallesPedidos, y me selecciona solo esos.
                .ToListAsync();
            return entities;
        }
    ```

6. Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripcion y la imgen del producto.

    ```
        http://localhost:5124/api/Producto/GetSinPedido
    ```

    ```
        public async Task<IEnumerable<Producto>> GetSinPedido()
        {
            var entities = await _context
                .Productos
                .Where(e => !_context.DetallePedidos.Any(d => d.CodigoProducto == e.CodigoProducto)) // Busca los productos que no tengan ningun dato en la tabla DetallesPedidos, y me selecciona solo esos.
                .ToListAsync();
            return entities;
        }
    ```

7. ¿Cuantos pedidos hay en cada estado? Ordena el resultado de forma descentende por el numero de pedidos.

    ```
        http://localhost:5124/api/Pedido/GetPedidosEstado
    ```

    ```
        public async Task<IEnumerable<object>> GetPedidosEstado()
    {
        var entities = await _context
            .Pedidos
            .GroupBy(e => e.Estado) // Se agrupan los datos segun el estado
            .Select(e => new 
                {
                    Estado = e.Key, // Se toma la key, que en este caso es el estado
                    TotalPedidos = e.Count() // se cuentan el total de pedidos por estado, como se agruparon por estado, va a contar todos los datos de cada estado
                }
            ) // Se crea un objeto con los datos requeridos en la consulta
            .OrderByDescending(e => e.TotalPedidos) // Se ordena de forma descendente segun el total de datos
            .ToListAsync();
        return entities;
    }
    ```

8. Devuelve un listado que muestre solamente los clientes que no han realizado ningun pago.

    ```
        http://localhost:5124/api/Cliente/GetSinPago
    ```

    ```
        public async Task<IEnumerable<Cliente>> GetSinPago()
        {
            var entities = await _context
                .Clientes
                .Where(e => !_context.Pagos.Any(d => d.CodigoCliente == e.CodigoCliente)) // Busca los cliente que no tengan ningun dato en la tabla Pagos, y me selecciona solo esos.
                .ToListAsync();
            return entities;
        }
    ```

9. Devuelve el listado clientes donde aparezca el nombre del cliente,el nombre y el primer apellido de su representante de ventas y la ciudad donde esta su oficina.

    ```
        http://localhost:5124/api/Cliente/GetRepOfiTodos
    ```

    ```
        public async Task<IEnumerable<object>> GetRepOfiTodos()
        {
            var entities = await _context
                .Clientes
                .Select(e => new
                {
                    NombreCliente = e.NombreCliente, // Se toma el Nombre
                    NombreRepresentante = $"{e.EmpleadoRepVentas.Nombre} {e.EmpleadoRepVentas.Apellido1}", // Se toma el Nombre y apellido del empleado representante de ventas de ese cliente
                    Oficina = e.EmpleadoRepVentas.Oficina.Ciudad // Se toma la ciudad donde esta ubicada la oficina de ese empleado
                }) // Se crea un objeto con los datos requeridos en la consulta
                .ToListAsync();
            return entities;
        }
    ```

10. Devuelve el nombre del cliente,el nombre y el primer apellido de su representante de ventas y el numero de telefono de la oficina del representante de ventas, de aquellos clientes que no hayan realizado ningun pago.

    ```
        http://localhost:5124/api/Cliente/GetRepOfiTel
    ```

    ```
        public async Task<IEnumerable<object>> GetRepOfiTel()
        {
            var entities = await _context
                .Clientes
                .Where(e => !_context.Pagos.Any(d => d.CodigoCliente == e.CodigoCliente))
                .Select(e => new
                {
                    NombreCliente = e.NombreCliente, // Se toma el Nombre
                    NombreRepresentante = $"{e.EmpleadoRepVentas.Nombre} {e.EmpleadoRepVentas.Apellido1}", // Se toma el Nombre y apellido del empleado representante de ventas de ese cliente
                    TelefonoOficina = e.EmpleadoRepVentas.Oficina.Telefono // Se toma el telefono de la oficina de ese empleado
                }) // Se crea un objeto con los datos requeridos en la consulta
                .ToListAsync();
            return entities;
        }
    ```