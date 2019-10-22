using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxaJuros : ControllerBase
    {
        private static readonly decimal ValorTaxaJuros = Convert.ToDecimal("0,01");

        public TaxaJuros()
        {
        }

        /// <summary>
        /// Retorna a taxa de juros
        /// </summary>
        /// <returns>Taxa de juros no formato #,##.</returns>
        [HttpGet("taxaJuros")]
        public Task<decimal> GetTaxaJuros()
        {
            return Task.FromResult(ValorTaxaJuros);
        }
    }
}
