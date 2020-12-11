using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using GestionEmpresaTransporte.Core;
using GestionEmpresaTransporte.Core.Vehiculos;


namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;
    
    public class VehiculoListarPanelCtrl
    {
        public enum Estados
        {
            Consultar,
            Modificar,
            Borrar,
            Insertar
        }
        
        private readonly BindingList<Vehiculo> _bindingList;
        private Vehiculo miVehiculo;
        
        public Estados EstadoPnlVehiculo { get; set; }
        public VehiculoListarPanelView View { get; }

        public VehiculoVerPanelCtrl vehiculoVerPanelCtrl { get; }
        
        private Empresa Empresa { get; }
        
        public VehiculoListarPanelCtrl(Empresa Empresa)
        {
            this.Empresa = Empresa;
            _bindingList = new BindingList<Vehiculo>(Empresa.ColeccionVehiculos.listaVehiculos);
            var sourceVehiculos = new WForms.BindingSource(_bindingList, null);
            vehiculoVerPanelCtrl = new VehiculoVerPanelCtrl();
            View = new VehiculoListarPanelView(vehiculoVerPanelCtrl.View);
            View.grdLista.DataSource = sourceVehiculos;
            
            View.grdLista.SelectionChanged += (sender, args) => ActualizarPanelVehiculo();
            View.grdLista.DataBindingComplete += (sender, args) => View.AjustarColGrid();

            vehiculoVerPanelCtrl.View.BtInsertar.Click += (sender, e) => InsertarVehiculo();
            vehiculoVerPanelCtrl.View.BtAceptar.Click += (sender, e) => Aceptar();
            vehiculoVerPanelCtrl.View.BtCancelar.Click += (sender, e) => Cancelar();
            vehiculoVerPanelCtrl.View.BtBorrar.Click += (sender, e) => BorrarVehiculo();
            vehiculoVerPanelCtrl.View.BtModificar.Click += (sender, e) => ModoModificar();
            vehiculoVerPanelCtrl.View.BtVolver.Click += (sender, e) => Volver();

            EstadoPnlVehiculo = Estados.Consultar;
            vehiculoVerPanelCtrl.View.ModoConsulta();
        }
        
        public Vehiculo ElVehiculo
        {
            get => miVehiculo;
            set
            {
                miVehiculo = value;
                ActualizaTextVehiculo();
            }
        }
        
        public void ActualizarPanelVehiculo()
        {
            foreach (WForms.DataGridViewRow row in View.grdLista.SelectedRows)
            {
                var matricula = View.grdLista.SelectedRows[0].Cells[0].Value.ToString();
                var VehiculoSeleccionado = _bindingList.FirstOrDefault(item => item.Matricula == matricula);
                ElVehiculo = VehiculoSeleccionado;
            }

            View.AjustarColGrid();
        }
        
        private void ActualizaTextVehiculo()
        {
            if (ElVehiculo != null)
            {
                vehiculoVerPanelCtrl.View.EdMatricula.Text = ElVehiculo.Matricula;
                vehiculoVerPanelCtrl.View.EdMarca.Text = ElVehiculo.Marca;
                vehiculoVerPanelCtrl.View.EdModelo.Text = ElVehiculo.Modelo;
                vehiculoVerPanelCtrl.View.EdConsumo.Text = ElVehiculo.Consumo.ToString(CultureInfo.CurrentCulture);
                vehiculoVerPanelCtrl.View.EdFechaFa.Value = ElVehiculo.FechaFabricacion;
                vehiculoVerPanelCtrl.View.EdFechaAd.Value = ElVehiculo.FechaAdquisicion;

                vehiculoVerPanelCtrl.View.EdTipo.Text = ElVehiculo.Tipo;

                vehiculoVerPanelCtrl.View.EdWIFI.Checked = ElVehiculo.Comodidades.Contains("WIFI");
                vehiculoVerPanelCtrl.View.EdTV.Checked = ElVehiculo.Comodidades.Contains("TV");
                vehiculoVerPanelCtrl.View.EdAC.Checked = ElVehiculo.Comodidades.Contains("A/C");
                vehiculoVerPanelCtrl.View.EdBluetooth.Checked = ElVehiculo.Comodidades.Contains("BLUETOOTH");
                vehiculoVerPanelCtrl.View.EdNevera.Checked = ElVehiculo.Comodidades.Contains("NEVERA");


            }
        }
        
        private void Aceptar()
        {
            switch (EstadoPnlVehiculo)
            {
                case Estados.Insertar:
                    AñadirVehiculo();
                    return;
                case Estados.Borrar:
                    BorrarVehiculo();
                    return;
                case Estados.Modificar:
                    ModificarVehiculo();
                    return;
                default:
                    return;
            }
        }
        
        private void Cancelar()
        {
            vehiculoVerPanelCtrl.View.ModoConsulta();
            ActualizaTextVehiculo();
        }
        
        private void ModoModificar()
        {
            EstadoPnlVehiculo = Estados.Modificar;
            vehiculoVerPanelCtrl.View.ModoModificar();
        }
        
        private void BorrarVehiculo()
        {
            if (ElVehiculo != null)
            {
                if (!Empresa.ColeccionTransportes.ExisteVehiculo(ElVehiculo))
                {
                    var id = ElVehiculo.Matricula;
                    /*MessageBOX de confirmacion*/
                    var message = string.Format("¿Estás seguro de borrar {1} con matricula: {0}?", id, ElVehiculo.Tipo);
                    var caption = "Borrar vehiculo";
                    var buttons = WForms.MessageBoxButtons.YesNo;
                    WForms.DialogResult result;
                    // Displays the MessageBox.
                    result = WForms.MessageBox.Show(message, caption, buttons);
                    if (result == WForms.DialogResult.Yes) _bindingList.Remove(ElVehiculo);
                }
                else
                {
                    WForms.MessageBox.Show("Este vehículo tiene transportes asignados, no se puede borrar ");
                }

                View.Actualizar();
                vehiculoVerPanelCtrl.View.ModoConsulta();
            }
        }
        
        private void InsertarVehiculo()
        {
            vehiculoVerPanelCtrl.View.ModoInsercion();
            EstadoPnlVehiculo = Estados.Insertar;
        }

        private List<string> ListaComodidades()
        {
            List<string> comodidades = new List<string>();
            if (vehiculoVerPanelCtrl.View.EdWIFI.Checked)
            {
                comodidades.Add("WIFI");
            }
            if (vehiculoVerPanelCtrl.View.EdAC.Checked)
            {
                comodidades.Add("A/C");
            }
            if (vehiculoVerPanelCtrl.View.EdBluetooth.Checked)
            {
                comodidades.Add("BLUETOOTH");
            }
            if (vehiculoVerPanelCtrl.View.EdTV.Checked)
            {
                comodidades.Add("TV");
            }
            if (vehiculoVerPanelCtrl.View.EdNevera.Checked)
            {
                comodidades.Add("NEVERA");
            }

            return comodidades;
        }

        private void ModificarVehiculo()
        {

            ElVehiculo.Consumo = (double) vehiculoVerPanelCtrl.View.EdConsumo.Value;
            
            ElVehiculo.Comodidades = ListaComodidades();
            
            View.Actualizar();
            vehiculoVerPanelCtrl.View.ModoConsulta();
        }

        private void AñadirVehiculo()
        {
            //Recogemos los datos del formulario
            var valido = true;
            var matricula = vehiculoVerPanelCtrl.View.EdMatricula.Text.ToUpper();
            var tipo = vehiculoVerPanelCtrl.View.EdTipo.SelectedItem;
            var marca = vehiculoVerPanelCtrl.View.EdMarca.Text;
            var modelo = vehiculoVerPanelCtrl.View.EdModelo.Text;
            var consumo = (double) vehiculoVerPanelCtrl.View.EdConsumo.Value;
            DateTime fechaFa = vehiculoVerPanelCtrl.View.EdFechaFa.Value;
            DateTime fechaAd = vehiculoVerPanelCtrl.View.EdFechaAd.Value;

            var comodidades = ListaComodidades();

            if (!utilidades.ValidarMatricula(matricula))
            {
                WForms.MessageBox.Show("La matricula " + matricula + " no es correcta");
                valido = false;
            }

            //Todos los campos tienen que tener un valor
            if ((marca.Length < 1) | (modelo.Length < 1) | (consumo < 0))
            {
                WForms.MessageBox.Show("Se requiere cubrir todos los campos");
                valido = false;
            }

            //La fecha de Fabricacion no puede ser superior a la fecha de Adquisicion
            if (DateTime.Compare(fechaFa, fechaAd) > 0)
            {
                WForms.MessageBox.Show("La fecha de Adquisicion no puede ser posterior a la de Fabricacion");
                valido = false; 
            }


        //Si  fue bien se crea un nuevo vehiculo
        if (valido)
            {
                switch (tipo)
                {
                    case "Furgoneta":
                        _bindingList.Add(new Furgoneta(matricula, fechaFa, fechaAd, consumo,
                            marca, modelo, comodidades));
                        vehiculoVerPanelCtrl.View.ModoConsulta();
                        break;
                    case "Camion":
                        _bindingList.Add(new Camion(matricula, fechaFa, fechaAd, consumo,
                            marca, modelo, comodidades));
                        vehiculoVerPanelCtrl.View.ModoConsulta();
                        break;
                    case "Camion Articulado":
                        _bindingList.Add(new CamionArticulado(matricula, fechaFa, fechaAd, consumo,
                            marca, modelo, comodidades));
                        vehiculoVerPanelCtrl.View.ModoConsulta();
                        break;
                    default:
                        WForms.MessageBox.Show("Introduce el tipo de vehiculo");
                        break;
                        
                }
            }
        }
        
        public void SeleccionarVehiculo(Vehiculo vehiculo)
        {
            var pos = Empresa.ColeccionVehiculos.PosVehiculo(vehiculo);

            if (View.grdLista.Columns.Count > 0) View.grdLista.Rows[pos].Selected = true;
        }
        
        private void Volver()
        {
            ElVehiculo = null;
            View.Visible = false;
            vehiculoVerPanelCtrl.View.ModoConsulta();
        }

    }
}