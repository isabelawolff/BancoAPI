using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Dominio
{
    public class Fundo
    {
        public int Id { get; set; }
        public int Cnpj { get; set; }
        public float Saldo { get; set; }
        public float TaxaPerformance { get; set; }
    }
}
