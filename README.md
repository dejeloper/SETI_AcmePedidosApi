# Acme Pedidos API

## Descripción

Acme Pedidos API es un servicio REST desarrollado en ASP.NET Core que actúa como un adaptador entre un cliente REST y un servicio SOAP.

La API recibe solicitudes en formato JSON, transforma la información al formato SOAP/XML requerido por el servicio externo, consume dicho servicio y finalmente convierte la respuesta nuevamente a JSON para el cliente.

---

## Tecnologías utilizadas

- C#
- .NET 8
- ASP.NET Core Web API
- HttpClient
- XML (SOAP)
- Swagger (OpenAPI)
- Docker

---

## Configuración

La URL del servicio SOAP se configura mediante `appsettings.json`.

Ejemplo:

```json
{
  "SoapService": {
    "BaseUrl": "https://smb2b095807450.free.beeceptor.com"
  }
}
```

Si el endpoint cambia, únicamente es necesario modificar este valor.

---

## Ejecución del proyecto

### Clonar el repositorio

```bash
git clone https://github.com/dejeloper/SETI_AcmePedidosApi.git
```

Ingresar al directorio:

```bash
cd SETI_AcmePedidosApi
```

### Ejecutar con .NET

```bash
dotnet restore
dotnet run
```

La API quedará disponible en la URL indicada por ASP.NET Core al iniciar la aplicación (por ejemplo `http://localhost:5117`).

---

## Swagger

Una vez iniciada la aplicación, la documentación de la API está disponible en:

```
http://localhost:<puerto>/swagger
```

---

## Ejecución con Docker

Construir la imagen:

```bash
docker build -t acme-pedidos-api .
```

Ejecutar el contenedor:

```bash
docker run -d --name acme-api -p 8080:8080 acme-pedidos-api
```

### Verificar la ejecución

Una vez iniciado el contenedor, la API estará disponible en:

```
http://localhost:8080
```

Y la documentación Swagger en:

```
http://localhost:8080/swagger
```

---

## Testing

Las pruebas unitarias se encuentran en el proyecto `AcmePedidosApi.Tests` y fueron desarrolladas con xUnit, Moq y FluentAssertions.

Para ejecutarlas:

```bash
dotnet test
```

---

## Endpoints

```
POST /api/pedidos
```

Recibe una solicitud JSON con la información del pedido y devuelve una respuesta JSON con el resultado del servicio SOAP.

Body:

```json
{
  "enviarPedido": {
    "numPedido": "75630275",
    "cantidadPedido": "1",
    "codigoEAN": "00110000765191002104587",
    "nombreProducto": "Armario INVAL",
    "numDocumento": "1113987400",
    "direccion": "CR 72B 45 12 APT 301"
  }
}
```

También se incluye un endpoint de prueba:

```
POST /api/pedidos/mock
```

Este endpoint simula la respuesta del servicio SOAP sin realizar llamadas externas.

Para conocer el contrato completo de la API y realizar pruebas interactivas, consulta la documentación disponible en Swagger.

---

## Flujo de la aplicación

```text
Cliente REST ->  API ASP.NET Core -> (JSON → SOAP/XML) ->  Servicio SOAP -> (SOAP/XML → JSON) -> Cliente REST
```

---

## Referencias

- Crear imágenes Docker para aplicaciones .NET: https://learn.microsoft.com/dotnet/core/docker/build-container
