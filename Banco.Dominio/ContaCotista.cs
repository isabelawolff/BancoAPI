using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Dominio
{
    public class ContaCotista
    {
        public int Id { get; set; }
        public int IdCotista { get; set; }
        public int Saldo { get; set; }
        public BloqueioContaCotistaEnum SituacaoContaCotista { get; set; }
    }
}
