using System;
using System.Data;
using System.Data.SqlClient;
using Curriculos.Models;
using System.Security.Principal;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Curriculos.DAO
{
    public class CurriculoDAO
    {
        /// <summary>
        /// Insere um currículo na tabela
        /// </summary>
        /// <param name="curriculo">View model do currículo</param>
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

        /// <summary>
        /// Altera um registro da tabela de currículos
        /// </summary>
        /// <param name="curriculo">View model do currículo</param>
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

        /// <summary>
        /// Exclui um registro da tabela de currículos
        /// </summary>
        /// <param name="cpf">CPF do currículo a ser excluído</param>
        public void Excluir(string cpf)
        {
            SqlParameter[] parametroCpf = { new SqlParameter("@cpf", cpf) };
            string sql = $"DELETE Curriculos WHERE cpf = @cpf";

            HelperDAO.ExecutarSQL(sql, parametroCpf);
        }

        /// <summary>
        /// Busca um currículo pelo seu CPF
        /// </summary>
        /// <param name="cpf">CPF do currículo</param>
        /// <returns>View model do currículo encontrado</returns>
        public CurriculoViewModel Consultar(string cpf)
        {
            SqlParameter[] parametroCpf = { new SqlParameter("@cpf", cpf) };
            string sql = "SELECT * FROM Curriculos WHERE cpf = @cpf";
            DataTable tabela = HelperDAO.ExecutarSelect(sql, parametroCpf);
            if (tabela.Rows.Count == 0)
                return null;
            else
            {
                DataRow registro = tabela.Rows[0];
                return MontarModel(registro);
            }
        }

        /// <summary>
        /// Cria uma lista contendo todos os registros da tabela
        /// </summary>
        /// <returns>Lista de DataRows com os registros</returns>
        public List<CurriculoViewModel> Listar()
        {
            List<CurriculoViewModel> lista = new List<CurriculoViewModel>();

            string sql = "SELECT * FROM Curriculos";
            DataTable tabela = HelperDAO.ExecutarSelect(sql);

            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontarModel(registro));

            return lista;
        }

        /// <summary>
        /// Cria um vetor de parâmetros para algum comando SQL
        /// </summary>
        /// <param name="curriculo">View model do currículo</param>
        /// <returns>Vetor de parâmetros SQL</returns>
        private SqlParameter[] CriarParametros(CurriculoViewModel curriculo)
        {
            SqlParameter[] parametros = new SqlParameter[19];

            if (curriculo.Cpf != null)
                parametros[0] = new SqlParameter("@cpf", curriculo.Cpf);
            else
                throw new Exception("O CPF não pode ficar vazio.");

            if (curriculo.Nome != null)
                parametros[1] = new SqlParameter("@nome", curriculo.Nome);
            else
                throw new Exception("O Nome não pode ficar vazio.");

            if (curriculo.DataNascimento != null)
                parametros[2] = new SqlParameter("@data_nascimento", curriculo.DataNascimento);
            else
                parametros[2] = new SqlParameter("@data_nascimento", DBNull.Value);

            if (curriculo.Endereco != null)
                parametros[3] = new SqlParameter("@endereco", curriculo.Endereco);
            else
                parametros[3] = new SqlParameter("@endereco", DBNull.Value);

            if (curriculo.Telefone != null)
                parametros[4] = new SqlParameter("@telefone", curriculo.Telefone);
            else
                parametros[4] = new SqlParameter("@telefone", DBNull.Value);

            if (curriculo.Email != null)
                parametros[5] = new SqlParameter("@email", curriculo.Email);
            else
                parametros[5] = new SqlParameter("@email", DBNull.Value);

            if (curriculo.Salario != null)
                parametros[6] = new SqlParameter("@salario", curriculo.Salario);
            else
                parametros[6] = new SqlParameter("@salario", DBNull.Value);

            if (curriculo.Cargo != null)
                parametros[7] = new SqlParameter("@cargo", curriculo.Cargo);
            else
                parametros[7] = new SqlParameter("@cargo", DBNull.Value);

            if (curriculo.Formacoes[0] != null)
                parametros[8] = new SqlParameter("@formacao1", curriculo.Formacoes[0]);
            else
                parametros[8] = new SqlParameter("@formacao1", DBNull.Value);

            if (curriculo.Formacoes[1] != null)
                parametros[9] = new SqlParameter("@formacao2", curriculo.Formacoes[1]);
            else
                parametros[9] = new SqlParameter("@formacao2", DBNull.Value);

            if (curriculo.Formacoes[2] != null)
                parametros[10] = new SqlParameter("@formacao3", curriculo.Formacoes[2]);
            else
                parametros[10] = new SqlParameter("@formacao3", DBNull.Value);

            if (curriculo.Formacoes[3] != null)
                parametros[11] = new SqlParameter("@formacao4", curriculo.Formacoes[3]);
            else
                parametros[11] = new SqlParameter("@formacao4", DBNull.Value);

            if (curriculo.Formacoes[4] != null)
                parametros[12] = new SqlParameter("@formacao5", curriculo.Formacoes[4]);
            else
                parametros[12] = new SqlParameter("@formacao5", DBNull.Value);

            if (curriculo.Experiencias[0] != null)
                parametros[13] = new SqlParameter("@experiencia1", curriculo.Experiencias[0]);
            else
                parametros[13] = new SqlParameter("@experiencia1", DBNull.Value);

            if (curriculo.Experiencias[1] != null)
                parametros[14] = new SqlParameter("@experiencia2", curriculo.Experiencias[1]);
            else
                parametros[14] = new SqlParameter("@experiencia2", DBNull.Value);

            if (curriculo.Experiencias[2] != null)
                parametros[15] = new SqlParameter("@experiencia3", curriculo.Experiencias[2]);
            else
                parametros[15] = new SqlParameter("@experiencia3", DBNull.Value);

            if (curriculo.Idiomas[0] != null)
                parametros[16] = new SqlParameter("@idioma1", curriculo.Idiomas[0]);
            else
                parametros[16] = new SqlParameter("@idioma1", DBNull.Value);

            if (curriculo.Idiomas[1] != null)
                parametros[17] = new SqlParameter("@idioma2", curriculo.Idiomas[1]);
            else
                parametros[17] = new SqlParameter("@idioma2", DBNull.Value);

            if (curriculo.Idiomas[2] != null)
                parametros[18] = new SqlParameter("@idioma3", curriculo.Idiomas[2]);
            else
                parametros[18] = new SqlParameter("@idioma3", DBNull.Value);

            return parametros;
        }

        /// <summary>
        /// Cria um view model de currículo a partir de um registro da tabela
        /// </summary>
        /// <param name="registro">DataRow contendo o registro de um currículo</param>
        /// <returns>View model do currículo</returns>
        private CurriculoViewModel MontarModel(DataRow registro)
        {
            CurriculoViewModel curriculo = new CurriculoViewModel();

            if (registro["cpf"] != DBNull.Value)
                curriculo.Cpf = registro["cpf"].ToString();

            if (registro["nome"] != DBNull.Value)
                curriculo.Nome = registro["nome"].ToString();

            if (registro["data_nascimento"] != DBNull.Value)
                curriculo.DataNascimento = Convert.ToDateTime(registro["data_nascimento"]);

            if (registro["endereco"] != DBNull.Value)
                curriculo.Endereco = registro["endereco"].ToString();

            if (registro["telefone"] != DBNull.Value)
                curriculo.Telefone = registro["telefone"].ToString();

            if (registro["email"] != DBNull.Value)
                curriculo.Email = registro["email"].ToString();

            if (registro["salario"] != DBNull.Value)
                curriculo.Salario = Convert.ToDouble(registro["salario"]);

            if (registro["cargo"] != DBNull.Value)
                curriculo.Cargo = registro["cargo"].ToString();

            if (registro["formacao1"] != DBNull.Value)
                curriculo.Formacoes[0] = registro["formacao1"].ToString();

            if (registro["formacao2"] != DBNull.Value)
                curriculo.Formacoes[1] = registro["formacao2"].ToString();

            if (registro["formacao3"] != DBNull.Value)
                curriculo.Formacoes[2] = registro["formacao3"].ToString();

            if (registro["formacao4"] != DBNull.Value)
                curriculo.Formacoes[3] = registro["formacao4"].ToString();

            if (registro["formacao5"] != DBNull.Value)
                curriculo.Formacoes[4] = registro["formacao5"].ToString();

            if (registro["experiencia1"] != DBNull.Value)
                curriculo.Experiencias[0] = registro["experiencia1"].ToString();

            if (registro["experiencia2"] != DBNull.Value)
                curriculo.Experiencias[1] = registro["experiencia2"].ToString();

            if (registro["experiencia3"] != DBNull.Value)
                curriculo.Experiencias[2] = registro["experiencia3"].ToString();

            if (registro["idioma1"] != DBNull.Value)
                curriculo.Idiomas[0] = registro["idioma1"].ToString();

            if (registro["idioma2"] != DBNull.Value)
                curriculo.Idiomas[1] = registro["idioma2"].ToString();

            if (registro["idioma3"] != DBNull.Value)
                curriculo.Idiomas[2] = registro["idioma3"].ToString();

            return curriculo;
        }
    }
}
