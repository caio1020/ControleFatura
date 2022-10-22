using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaClienteTeste.Models
{
    public class MensalidadeModel
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int  Ano { get; set; }
        public decimal Valor { get; set; }
        public string Situacao { get; set; }
        public int SolicitanteId { get; set; }
        public string SolicitanteNome { get; set; }
        public List<SolicitanteModel> Solicitantes { get; set; }
    }
}
