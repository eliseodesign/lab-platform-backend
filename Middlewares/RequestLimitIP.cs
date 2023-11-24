using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using LabPlatform.Services.Statics;

namespace LabPlatform
{
    public class RequestLimitIP
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public RequestLimitIP(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task Invoke(HttpContext context)
        {
            // Verifica si la solicitud está dirigida a un endpoint específico
            if (context.Request.Path == "/api/chat/playground" || context.Request.Path == "/api/chat/test")
            {
                // Obtiene la dirección IP del cliente
                string clientIP = context.Connection.RemoteIpAddress.ToString();

                System.Console.WriteLine("IP: " + clientIP);
                // Crea una clave única para rastrear el límite de solicitudes por IP
                string cacheKey = "req_playground_limit_" + clientIP;

                // Verificar si la IP ha alcanzado el límite
                if (_cache.TryGetValue(cacheKey, out int requestCount) && requestCount >= 5)
                {
                    // Aquí, el límite es de 5 solicitudes, pero puedes ajustarlo según tus necesidades.
                    await UtilsService.BadRequest(context, "Esta IP ha alcanzado el límite de solicitudes permitidas.");
                    return;
                }

                // Incrementar el contador de solicitudes y establecerlo en la caché
                requestCount++;
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1) // Establece un tiempo de expiración
                };
                _cache.Set(cacheKey, requestCount, cacheEntryOptions);

                try
                {
                    await _next(context);
                    return;
                }
                catch (Exception ex)
                {
                    await UtilsService.BadRequest(context, $"Error al intentar obtener el chat: {ex.Message}");
                    return;
                }
            }

            await _next(context);
        }
    }
}
