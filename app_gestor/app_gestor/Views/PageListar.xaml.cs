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
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
            AtualizarLista();
        }
        public void AtualizarLista()
        {
            String nome = "";
            if (txtNome.Text != null) nome = txtNome.Text;
            ServiceDBEmployee dbFuncionario = new ServiceDBEmployee(App.DbPath);
            if (swFavorito.IsToggled)
            {
                ListaFuncionario.ItemsSource = dbFuncionario.Localizar(nome, true);
            }
            else
            {
                var listaTurnos = new List<string>();
                listaTurnos.Add("Manhã");
                listaTurnos.Add("Tarde");
                listaTurnos.Add("Noite");
                var picker = new Picker { Title = "Selecione um turno:", TitleColor = Color.Red };
                picker.ItemsSource = listaTurnos;

               
                
                var turnoLabel = new Label();
                turnoLabel.SetBinding(Label.TextProperty, new Binding("SelectedItem", source: picker));
            }
        }

        private void ListaFuncionario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ModelEmployee funcionario = (ModelEmployee)ListaFuncionario.SelectedItem;
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageCadastrar(funcionario));
        }

        private void swFavorito_Toggled(object sender, ToggledEventArgs e)
        {
            AtualizarLista();
        }

        private void btLocalizar_Clicked(object sender, EventArgs e)
        {
            AtualizarLista();
        }
    }
}