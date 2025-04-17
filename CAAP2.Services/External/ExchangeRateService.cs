using System.Net.Http.Json;
using CAAP2.Models.External;
using System;

namespace CAAP2.Services.External
{
    public sealed class ExchangeRateService : IExchangeRateService
    {
        //Instancia estática única (Lazy garantiza inicialización perezosa y seguridad en hilos múltiples)
        private static readonly Lazy<ExchangeRateService> _instance = new(() => new ExchangeRateService());

        //Cliente HTTP que será reutilizado
        private readonly HttpClient _httpClient;

        //Propiedad pública para acceder a la instancia Singleton
        public static ExchangeRateService Instance => _instance.Value;

        //Constructor privado impide la creación desde fuera
        private ExchangeRateService()
        {
            _httpClient = new HttpClient();
        }

        //Método que consulta el tipo de cambio desde una API externa
        public async Task<ExchangeRateResponse?> GetExchangeRateAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ExchangeRateResponse>("https://tipodecambio.paginasweb.cr/api");
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
