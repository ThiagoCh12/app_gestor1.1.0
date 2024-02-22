using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using app_gestor.Model;

namespace app_gestor.Service
{
    internal class ServiceDBEmployee
    {
        SQLiteConnection conn;
        public string StatusMessage { get; set; }
        public ServiceDBEmployee(string dbPath)
        {
            if (dbPath == "") dbPath = App.DbPath;
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<ModelEmployee>();//isso que é criação de tabelas em mode de execução  
        }
        public void Inserir(ModelEmployee employee)
        {
            try
            {
                if (string.IsNullOrEmpty(employee.Nome))
                    throw new Exception("nome do funcionário não informado!");
                if (string.IsNullOrEmpty(employee.Turno))
                    throw new Exception("Turno do funcionário não informado!");
                if (string.IsNullOrEmpty(employee.Setor))
                    throw new Exception("Setor do funcionário não informado!");
                int result = conn.Insert(employee);
                if (result != 0)
                {
                    this.StatusMessage = string.Format("{0} registro(s) adicionado(s): [Nota: {1}]", result, employee.Nome);
                }
                else
                {
                    this.StatusMessage = string.Format("0 registro adicionado! Por faovr, informe os dados do funcionário!");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<ModelEmployee> Listar() //tudo que não é void precisa de um return
        {
            List<ModelEmployee> lista = new List<ModelEmployee>();
            try
            {
                lista = conn.Table<ModelEmployee>().ToList();
                this.StatusMessage = "Listagem de funcionarios";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return lista;
        }
        public void Alterar(ModelEmployee employee)
        {
            try
            {
                if (string.IsNullOrEmpty(employee.Nome))
                    throw new Exception("nome do funcionário não informado!");
                if (string.IsNullOrEmpty(employee.Turno))
                    throw new Exception("Turno do funcionário não informado!");
                if (string.IsNullOrEmpty(employee.Setor))
                    throw new Exception("Setor do funcionário não informado!");
                int result = conn.Update(employee);
                StatusMessage = string.Format("{0} registro alterado!", result);

            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<ModelEmployee>().Delete(r => r.Id == id);//expressão regular, usada pelo Linq
                StatusMessage = string.Format("{0} registro deletado", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        public List<ModelEmployee> Localizar(string nome)
        {
            List<ModelEmployee> lista = new List<ModelEmployee>();
            try
            {
                var resp = from p in conn.Table<ModelEmployee>() where p.Nome.ToLower().Contains(nome.ToLower()) select p;//to lower é pra deixar tudo minúsculo; p é a tabela de anotações
                lista = resp.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }
        public List<ModelEmployee> Localizar(string titulo, Boolean favorito) //esse localizar só mostra os favoritados
        {
            List<ModelEmployee> lista = new List<ModelEmployee>();
            try
            {
                var resp = from p in conn.Table<ModelEmployee>() where p.Nome.ToLower().Contains(titulo.ToLower()) && p.Favorito == favorito select p;//to lower é pra deixar tudo minúsculo; p é a tabela de anotações
                lista = resp.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return lista;
        }
        public ModelEmployee GetFuncionario(int id)
        {
            ModelEmployee m = new ModelEmployee();
            try
            {
                m = conn.Table<ModelEmployee>().First(n => n.Id == id);// <- chama-se expressão regular
                StatusMessage = "Encontrei o funcionário!";
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
            return m;
        }
    }
}
