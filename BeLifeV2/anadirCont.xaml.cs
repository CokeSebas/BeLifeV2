﻿using LibbClas;
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
using System.Windows.Shapes;

namespace BeLifeV2
{
    /// <summary>
    /// Lógica de interacción para anadirCont.xaml
    /// </summary>
    public partial class anadirCont : Window
    {
        public Conexion conec = new Conexion();
        public Contrato objCont = new Contrato();
        public Cliente objCli = new Cliente();
        private int _edad;
        private int _estadoC;
        private int _sexo;
        private double _primaBase;

        public int Edad
        {
            get
            {
                return _edad;
            }

            set
            {
                _edad = value;
            }
        }

        public int EstadoC
        {
            get
            {
                return _estadoC;
            }

            set
            {
                _estadoC = value;
            }
        }

        public int Sexo
        {
            get
            {
                return _sexo;
            }

            set
            {
                _sexo = value;
            }
        }

        public double PrimaBase
        {
            get
            {
                return _primaBase;
            }

            set
            {
                _primaBase = value;
            }
        }

        public anadirCont()
        {
            InitializeComponent();
            listCombo();
            dtpFechaInicio.SelectedDate = DateTime.Today;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public void listCombo()
        {
            string[] listRut = conec.listRut();

            string[] listPlan = conec.listPlan();
            cbbPlan.SelectedIndex = 0;
            cbbPlan.Items.Add("Seleccione");
            for (int x = 0; x < listPlan.Length; x++)
            {
                cbbPlan.Items.Add(listPlan[x]);
            }

            cbbSalud.SelectedIndex = 0;
            cbbSalud.Items.Add("Seleccione");
            cbbSalud.Items.Add("No");
            cbbSalud.Items.Add("Si");
        }

        private void txtNroCont_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void limpiar()
        {
            txtNombreCliCon.Clear();
            txtRutCont.Clear();
            cbbPlan.SelectedIndex = 0;
            txtPoliza.Clear();
            dtpFechaInicio.DisplayDate = DateTime.Today;
            dtpFechaInicio.SelectedDate = DateTime.Today;
            cbbSalud.SelectedIndex = 0;
            txtPrimaMen.Clear();
            txtPrimaAnu.Clear();
            txtObsv.Clear();
        }

        private void btnGuardarCont_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool guarda = false;
                string rutCliente = txtRutCont.Text;

                DateTime localDate = DateTime.Now;
                string mes = "", dia = "", minu = "", seg = "";
                string anio = localDate.Year.ToString();
                mes = localDate.Month.ToString();
                dia = localDate.Day.ToString();
                string hora = localDate.Hour.ToString();
                minu = localDate.Minute.ToString();
                seg = localDate.Second.ToString();

                if (mes.Length == 1)
                {
                    mes = "0" + mes;
                }
                if (dia.Length == 1)
                {
                    dia = "0" + dia;
                }
                if (minu.Length == 1)
                {
                    minu = "0" + minu;
                }
                if (seg.Length == 1)
                {
                    seg = "0" + seg;
                }
                string numero = anio + mes + dia + hora + minu + seg;

                string plan = cbbPlan.SelectedValue.ToString();
                DateTime fechaIniVig = dtpFechaInicio.SelectedDate.Value;
                DateTime fechaFinVig = fechaIniVig.AddYears(1);
                string fechaVigencia = fechaIniVig.Year.ToString() + "-" + fechaIniVig.Month.ToString() + "-" + fechaIniVig.Day.ToString();
                string fechaFinVigencia = fechaFinVig.Year.ToString() + "-" + fechaFinVig.Month.ToString() + "-" + fechaFinVig.Day.ToString();
                string salud = cbbSalud.SelectedValue.ToString();

                if (salud == "Si")
                {
                    salud = "1";
                }
                else if (salud == "No")
                {
                    salud = "0";
                }
                else
                {
                    salud = "";
                }
                string primaAnu = txtPrimaAnu.Text;
                string primaMen = txtPrimaMen.Text;
                string observacion = txtObsv.Text;

                //MessageBox.Show("plan " + plan);

                objCont.RutCliente = rutCliente;
                objCont.NumeroContrato = numero;
                objCont.CodigoPlan = plan;
                objCont.FechaInicioVigencia = fechaVigencia;
                objCont.FechaFinVigencia = fechaFinVigencia;
                objCont.DeclaracionSalud = salud;
                objCont.PrimaAnual = primaAnu;
                objCont.PrimaMensual = primaMen;
                objCont.Vigente = "1";
                objCont.Observaciones = observacion;


                //DateTime fecha_iniv = Convert.ToDateTime(value);
                int result = DateTime.Compare(fechaIniVig, DateTime.Today);
                int mesV = fechaIniVig.Month - DateTime.Today.Month;
                //if (result >= 0){
                if (mesV < 1)
                {
                    guarda = objCont.agregarContrato();
                    if (guarda == true)
                    {
                        MessageBox.Show("Contrato Ingresado", "Confirmacion!", MessageBoxButton.OK, MessageBoxImage.Information);
                        limpiar();
                    }
                    else
                    {
                        MessageBox.Show("Contrato Ya Ingresado", "Advertencia!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Mes de inicio de vigencia no puede ser superior a un mes", "Advertencia!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnVerDatos_Click(object sender, RoutedEventArgs e)
        {
            string rut = txtRutCont.Text;
            bool existe = objCli.validar("Cliente", rut);

            if (existe == false)
            {
                string[] datos = conec.getDatosCliente(rut);
                txtNombreCliCon.Text = datos[0] + " " + datos[1];
                Edad = (DateTime.Now.Subtract(Convert.ToDateTime(datos[2])).Days / 365);
                Sexo = int.Parse(datos[3]);
                EstadoC = int.Parse(datos[4]);
            }
            else
            {
                MessageBox.Show("El Cliente no ha sido Ingresado en la Base de Datos", "Advertencia!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtNombreCliCon.Clear();
            }

        }

        private void cbbPlan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string plan = cbbPlan.SelectedItem.ToString();
            string[] datosPoliza = conec.datosPoliza(plan);
            txtPoliza.Text = datosPoliza[0];
            //txtPoliza.Text = datosPoliza[1];
            PrimaBase = Convert.ToDouble(datosPoliza[1]);

            double total, recargoEdad = 0, recargoSexo = 0, recargoEstadoC = 0, recargoBase = 0;

            if (Edad < 18 && Edad > 25)
            {
                recargoEdad = 3.6;
            }
            else if (Edad < 26 && Edad > 45)
            {
                recargoEdad = 2.4;
            }
            else if (Edad > 45)
            {
                recargoEdad = 6.0;
            }

            if (Sexo == 1)
            {
                recargoSexo = 2.4;
            }
            else if (Sexo == 2)
            {
                recargoSexo = 1.2;
            }

            if (EstadoC == 1)
            {
                recargoEstadoC = 4.8;
            }
            else if (EstadoC == 2)
            {
                recargoEstadoC = 2.4;
            }
            else if (EstadoC == 3 || EstadoC == 4)
            {
                recargoEstadoC = 3.6;
            }

            recargoBase = PrimaBase;
            total = recargoBase + recargoEdad + recargoSexo + recargoEstadoC;
            txtPrimaAnu.Text = total.ToString();
            txtPrimaMen.Text = Math.Round((total / 12), 2).ToString();
        }

        private void btnListarCon_Click(object sender, RoutedEventArgs e)
        {
            
            ListarContrato listCont = new ListarContrato();
            this.Hide();
            listCont.Owner = this;
            listCont.ShowDialog();
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiar();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
