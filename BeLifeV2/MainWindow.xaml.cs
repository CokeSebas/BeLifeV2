using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using MahApps.Metro.Actions;

namespace BeLifeV2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListarCliente listCli = new ListarCliente();
            listCli.Owner = this;
            listCli.ShowDialog();
        }

        private void btn_editar_cotrato_Click(object sender, RoutedEventArgs e){
            editarCliente edClie = new editarCliente();
            edClie.Owner = this;
            edClie.ShowDialog();
        }

        private void btn_editar_contrato_Click(object sender, RoutedEventArgs e)
        {
            editarContrato edCont = new editarContrato();
            edCont.Owner = this;
            edCont.ShowDialog();
        }

        private void btn_listar_contrato_Click(object sender, RoutedEventArgs e)
        {
            ListarContrato listCont = new ListarContrato();
            listCont.Owner = this;
            listCont.ShowDialog();
        }

        private void btn_agregar_contrato_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
