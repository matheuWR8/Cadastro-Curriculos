using System;

namespace Curriculos.Models
{
    public class CurriculoViewModel
    {
        public String Nome { get; set; }
        public String Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Endereco { get; set; }
        public String Telefone { get; set; }
        public String Email { get; set; }
        public double Salario { get; set; }
        public String Cargo { get; set; }
        public String[] Formacoes { get; set; }
        public String[] Experiencias { get; set; }
        public String[] Idiomas { get; set; }

        public CurriculoViewModel()
        {
            Formacoes = new string[5];
            Experiencias = new string[3];
            Idiomas = new string[3];
        }
    }
}