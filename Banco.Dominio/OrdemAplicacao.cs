using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Dominio
{
    public class OrdemAplicacao
    {
        public int Id { get; set; }
        public int IdCotista { get; set; }
        public int IdFundo { get; set; }
        public float Valor { get; set; }
        public DateTime DataOrdem { get; set; }
    }
}
