using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoTaxaJuros : ControllerBase
    {
        private static readonly decimal ValorTaxaJuros = Convert.ToDecimal("0,01");

        public CalculoTaxaJuros()
        {
        }

        /// <summary>
        /// Retorna o valor calculador sobre a taxa de juros da API1
        /// </summary>
        /// <param name="valorInicial">Valor inicial para calcular.</param>
        /// <param name="tempo">Tempo para calcular (em meses).</param>
        /// <returns>Resultado no formato #,##.</returns>
        [HttpGet("calculaJuros")]
        public async Task<decimal> GetCalculoTaxaJuros(decimal valorInicial, uint tempo)
        {
            string url = "http://localhost:10000/api/TaxaJuros/taxaJuros";
            using (HttpClient client = new HttpClient())
            {
                string responseTaxaJuros = await client.GetStringAsync(url);
                decimal taxaJuros = 1 + Convert.ToDecimal(responseTaxaJuros.Replace('.', ','));
                decimal result = valorInicial;
                for (var potencia = 1; potencia <= tempo; potencia++)
                {
                    result = result * taxaJuros;
                }
                string resultString = result.ToString();
                int resultStringCommaIndex = resultString.IndexOf(",");
                if (resultStringCommaIndex > 0)
                {
                    char resultStringNumber1AfterCommaIndex = resultString.ElementAtOrDefault(resultStringCommaIndex + 1);
                    char resultStringNumber2AfterCommaIndex = resultString.ElementAtOrDefault(resultStringCommaIndex + 2);
                    string resultStringBeforeComma = resultString.Substring(0, resultStringCommaIndex);
                    result = Convert.ToDecimal($"{resultStringBeforeComma},{resultStringNumber1AfterCommaIndex}{resultStringNumber2AfterCommaIndex}");
                }
                return await Task.FromResult(result);
            }
        }

        [HttpGet("showmethecode")]
        public async Task<string> GetUrlDoCodigoFonteNoGitHub()
        {
            return await Task.FromResult("https://github.com/levrangeles/calcula-juros");
        }
    }
}
