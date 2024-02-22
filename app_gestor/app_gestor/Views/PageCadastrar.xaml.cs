using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using app_gestor.Model;
using app_gestor.Service;

namespace app_gestor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        public PageCadastrar()
        {
            InitializeComponent();
        }
        public PageCadastrar(ModelEmployee funcionario)
        {
            InitializeComponent();
            btSalvar.Text = "Alterar";
            txtCodigo.IsVisible = true;
            btExcluir.IsVisible = true;
            txtCodigo.Text = funcionario.Id.ToString();
            txtNome.Text = funcionario.Nome;
            txtSetor.Text = funcionario.Setor;
            if (pkTurno.Items.Contains(funcionario.Turno))
            {
                // Seleciona o turno correspondente no Picker
                pkTurno.SelectedItem = funcionario.Turno;
            }
            swFavorito.IsToggled = funcionario.Favorito;
        }

        private void btSalvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                ModelEmployee funcionario = new ModelEmployee();
                funcionario.Nome = txtNome.Text;
                funcionario.Setor = txtSetor.Text;
                
                var listaTurnos = new List<string>();
                listaTurnos.Add("Manhã");
                listaTurnos.Add("Tarde");
                listaTurnos.Add("Noite");
                var picker = new Picker { Title = "Selecione um turno:", TitleColor = Color.Red };
                picker.ItemsSource = listaTurnos;

                funcionario.Turno = picker.ToString();

                ServiceDBEmployee dbEmployee = new ServiceDBEmployee(App.DbPath);
                if (btSalvar.Text == "Inserir")
                {
                    dbEmployee.Inserir(funcionario);
                    DisplayAlert("Resultado", dbEmployee.StatusMessage, "ok");
                }
                else
                {
                    funcionario.Id = Convert.ToInt32(txtCodigo.Text);
                    dbEmployee.Alterar(funcionario);
                    DisplayAlert("Funcionario alterado com sucesso", dbEmployee.StatusMessage, "OK");

                }
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {

                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void btExcluir_Clicked(object sender, EventArgs e)
        {
            var resp = await DisplayAlert("Excluir funcionario", "Deseja excluir o funcionario selecionado?", "Sim", "Não");
            if (resp == true)
            {
                ServiceDBEmployee dbEmployee = new ServiceDBEmployee(App.DbPath);
                int id = Convert.ToInt32(txtCodigo.Text);
                dbEmployee.Excluir(id);
                DisplayAlert("Funcionario excluida com sucesso", dbEmployee.StatusMessage, "ok");
                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }

        private void btCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }
    }
}