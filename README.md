# azure-backend-net

Ejemplo de API RESTful en **.NET 6** para gestionar un recurso `Producto`.

## Endpoints

- `GET /api/productos` Obtiene la lista de productos desde SQL Server usando **Dapper**.
- `POST /api/productos` Crea un nuevo producto.

La cadena de conexión se define en `appsettings.json`.
