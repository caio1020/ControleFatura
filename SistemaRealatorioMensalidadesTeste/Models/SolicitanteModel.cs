using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaClienteTeste.Models
{
    public class SolicitanteModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
    }
}
