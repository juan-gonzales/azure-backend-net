using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using ProductosApi.Models;

namespace ProductosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IDbConnection _connection;

        public ProductosController(IDbConnection connection)
        {
            _connection = connection;
        }

        // GET api/productos
        [HttpGet]
        public async Task<IEnumerable<Producto>> Get()
        {
            var sql = "SELECT Id, Nombre, Precio FROM Productos";
            var productos = await _connection.QueryAsync<Producto>(sql);
            return productos;
        }

        // GET api/productos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var sql = "SELECT Id, Nombre, Precio FROM Productos WHERE Id = @Id";
            var producto = await _connection.QueryFirstOrDefaultAsync<Producto>(sql, new { Id = id });

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }
        // POST api/productos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            var sql = "INSERT INTO Productos (Nombre, Precio) VALUES (@Nombre, @Precio); SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = await _connection.ExecuteScalarAsync<int>(sql, producto);
            producto.Id = id;
            return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
        }
    }
}
