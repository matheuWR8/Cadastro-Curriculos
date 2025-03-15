using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Curriculos.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using System.Collections.Generic;

namespace Curriculos.DAO
{
    public class CurriculoDAO
    {
        public void Inserir(CurriculoViewModel curriculo)
        {
            SqlParameter[] parametros = CriarParametros(curriculo);
            string sql = "INSERT INTO Curriculos(cpf, nome, data_nascimento, endereco, telefone, email, salario, cargo, " +
                            "formacao1, formacao2, formacao3, formacao4, formacao5, " +
                            "experiencia1, experiencia2, experiencia3, idioma1, idioma2, idioma3) " +
                            "VALUES (@cpf, @nome, @data_nascimento, @endereco, @telefone, @email, @salario, @cargo, " +
                            "@formacao1, @formacao2, @formacao3, @formacao4, @formacao5, " +
                            "@experiencia1, @experiencia2, @experiencia3, @idioma1, @idioma2, @idioma3)";

            HelperDAO.ExecutarSQL(sql, parametros);
        }

        public void Alterar(CurriculoViewModel curriculo)
        {
            SqlParameter[] parametros = CriarParametros(curriculo);
            string sql = "UPDATE Curriculos SET nome = @nome, data_nascimento = @data_nascimento, " +
                            "endereco = @endereco, telefone = @telefone, email = @email, salario = @salario, cargo = @cargo, " +
                            "formacao1 = @formacao1, formacao2 = @formacao2, formacao3 = @formacao3, formacao4 = @formacao4, formacao5 = @formacao5, " +
                            "experiencia1 = @experiencia1, experiencia2 = @experiencia2, experiencia3 = @experiencia3, idioma1 = @idioma1, idioma2 = @idioma2, idioma3 = @idioma3 " +
                            "WHERE cpf = @cpf";

            HelperDAO.ExecutarSQL(sql, parametros);
        }

        public void Excluir(string cpf)
        {
            SqlParameter[] parametroCpf = { new SqlParameter("@cpf", cpf) };
            string sql = $"DELETE Curriculos WHERE cpf = @cpf";

            HelperDAO.ExecutarSQL(sql, parametroCpf);
        }

        public CurriculoViewModel Consultar(string cpf)
        {
            SqlParameter[] parametroCpf = { new SqlParameter("@cpf", cpf) };
            string sql = "SELECT * FROM curriculos WHERE cpf = @cpf";
            DataTable tabela = HelperDAO.ExecutarSelect(sql, parametroCpf);
            if (tabela.Rows.Count == 0)
                return null;
            else
            {
                DataRow registro = tabela.Rows[0];
                return MontarModel(registro);
            }
        }

        public List<CurriculoViewModel> Listar()
        {
            List<CurriculoViewModel> lista = new List<CurriculoViewModel>();

            string sql = "SELECT * FROM Curriculos";
            DataTable tabela = HelperDAO.ExecutarSelect(sql);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontarModel(registro));

            return lista;
        }

        private SqlParameter[] CriarParametros(CurriculoViewModel curriculo)
        {
            SqlParameter[] parametros =
            {
                new SqlParameter("@cpf", curriculo.Cpf),
                new SqlParameter("@nome", curriculo.Nome),
                new SqlParameter("@data_nascimento", curriculo.DataNascimento),
                new SqlParameter("@endereco", curriculo.Endereco),
                new SqlParameter("@telefone", curriculo.Telefone),
                new SqlParameter("@email", curriculo.Email),
                new SqlParameter("@salario", curriculo.Salario),
                new SqlParameter("@cargo", curriculo.Cargo),
                new SqlParameter("@formacao1", curriculo.Formacoes[0]),
                new SqlParameter("@formacao2", curriculo.Formacoes[1]),
                new SqlParameter("@formacao3", curriculo.Formacoes[2]),
                new SqlParameter("@formacao4", curriculo.Formacoes[3]),
                new SqlParameter("@formacao5", curriculo.Formacoes[4]),
                new SqlParameter("@experiencia1", curriculo.Experiencias[0]),
                new SqlParameter("@experiencia2", curriculo.Experiencias[1]),
                new SqlParameter("@experiencia3", curriculo.Experiencias[2]),
                new SqlParameter("@idioma1", curriculo.Idiomas[0]),
                new SqlParameter("@idioma2", curriculo.Idiomas[1]),
                new SqlParameter("@idioma3", curriculo.Idiomas[2])
            };

            return parametros;
        }

        private CurriculoViewModel MontarModel(DataRow registro)
        {
            CurriculoViewModel curriculo = new CurriculoViewModel()
            {
                Cpf = registro["cpf"].ToString(),
                Nome = registro["nome"].ToString(),
                DataNascimento = Convert.ToDateTime(registro["data"]),
                Endereco = registro["endereco"].ToString(),
                Telefone = registro["telefone"].ToString(),
                Email = registro["email"].ToString(),
                Salario = Convert.ToDouble(registro["salario"]),
                Cargo = registro["cargo"].ToString(),
            };

            curriculo.Formacoes[0] = registro["formacao1"].ToString();
            curriculo.Formacoes[1] = registro["formacao2"].ToString();
            curriculo.Formacoes[2] = registro["formacao3"].ToString();
            curriculo.Formacoes[3] = registro["formacao4"].ToString();
            curriculo.Formacoes[4] = registro["formacao5"].ToString();

            curriculo.Experiencias[0] = registro["experiencia1"].ToString();
            curriculo.Experiencias[1] = registro["experiencia2"].ToString();
            curriculo.Experiencias[2] = registro["experiencia3"].ToString();

            curriculo.Idiomas[0] = registro["idioma1"].ToString();
            curriculo.Idiomas[1] = registro["idioma2"].ToString();
            curriculo.Idiomas[2] = registro["idioma3"].ToString();

            return curriculo;
        }
    }
}
