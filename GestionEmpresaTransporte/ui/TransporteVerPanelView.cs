using System;
using System.Collections.Generic;
using System.Linq;
namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;
    using GestionEmpresaTransporte.Core;

    public class TransporteVerPanelView : WForms.Panel
    {
        public TransporteVerPanelView()
        {
            Build();
        }

        /*Datos del cliente*/
        public WForms.TextBox EdCliente { get; private set; }
        public WForms.TextBox EdFlota { get; private set; }
        public WForms.DateTimePicker EdFechaContratacion { get; private set; }
        public WForms.NumericUpDown EdKmsRecorridos { get; private set; }
        public WForms.DateTimePicker EdFechaSalida { get; private set; }
        public WForms.DateTimePicker EdFechaEntrega { get; private set; }
        public WForms.NumericUpDown EdImporteDia { get; private set; }
        public WForms.NumericUpDown EdIVA { get; private set; }
        public WForms.NumericUpDown EdPrecioLitro { get; private set; }
        public WForms.NumericUpDown EdGas { get; private set; }
        public WForms.Button BtAceptar { get; private set; }
        public WForms.Button BtCancelar { get; private set; }
        public WForms.Button BtModificar { get; private set; }
        public WForms.Button BtBorrar { get; private set; }
        public WForms.Button BtInsertar { get; private set; }
        public WForms.Button BtSelecCliente { get; private set; }
        public WForms.Button BtSelecVehiculo { get; private set; }
        public WForms.TextBox EdFactura { get; private set; }
        
        //Botones individual búsqueda transporte y datos
        
        public WForms.NumericUpDown EdYearFiltro { get; private set; }
        
        public WForms.DateTimePicker EdFechaFiltro { get; private set; }
        
        public WForms.Button BtPendiente { get; private set; }
        
        public WForms.Button BtFiltroYear { get; private set; }
        
        public WForms.Button BtFiltroFecha { get; private set; }
        
        public WForms.Button BtVolver { get; private set; }

        private void Build()
        {
            var PanelDatos = new WForms.TableLayoutPanel();
            PanelDatos.SuspendLayout();
            PanelDatos.ColumnCount = 2;
            PanelDatos.Dock = WForms.DockStyle.Fill;
            PanelDatos.ColumnStyles.Add(new WForms.ColumnStyle(WForms.SizeType.Percent, 50F));
            PanelDatos.ColumnStyles.Add(new WForms.ColumnStyle(WForms.SizeType.Percent, 50F));

            var pnlTable1 = new WForms.TableLayoutPanel();
            pnlTable1.SuspendLayout();
            pnlTable1.Dock = WForms.DockStyle.Fill;

            var pnlTable2 = new WForms.TableLayoutPanel();
            pnlTable2.SuspendLayout();
            pnlTable2.Dock = WForms.DockStyle.Fill;

            pnlTable1.Controls.Add(BuildCliente());
            pnlTable1.Controls.Add(BuildFlota());
            pnlTable1.Controls.Add(BuildFechaContrata());
            pnlTable1.Controls.Add(BuildKmsRecorridos());
            pnlTable1.Controls.Add(BuildFechaSalida());
            pnlTable2.Controls.Add(BuildFechaEntrega());
            pnlTable2.Controls.Add(BuildImporteDia());
            pnlTable2.Controls.Add(BuildIVA());
            pnlTable2.Controls.Add(BuildPrecioLitro());
            pnlTable2.Controls.Add(BuildGas());
            
            pnlTable1.Controls.Add(BuildRelleno());
            pnlTable2.Controls.Add(BuildRelleno());
           
            pnlTable1.Controls.Add(BuildFiltroYear());
            pnlTable2.Controls.Add(BuildFiltroFecha());
            pnlTable2.Controls.Add(BuildPanelBotones());
            pnlTable1.Controls.Add(BuildPanelBotonesFiltro());
            

            var PanelFactura = new WForms.TableLayoutPanel();
            PanelFactura.SuspendLayout();
            PanelFactura.ColumnCount = 1;
            PanelFactura.Dock = WForms.DockStyle.Bottom;

            PanelFactura.Controls.Add(BuildFactura());

            pnlTable2.ResumeLayout(false);
            pnlTable1.ResumeLayout(false);

            pnlTable1.MaximumSize = pnlTable1.MinimumSize;
            pnlTable2.MaximumSize = pnlTable2.MinimumSize;


            PanelDatos.Controls.Add(pnlTable1, 0, 0);
            PanelDatos.Controls.Add(pnlTable2, 1, 0);

            PanelDatos.ResumeLayout(false);
            PanelFactura.ResumeLayout(false);

            Controls.Add(PanelDatos);
            Controls.Add(PanelFactura);

            MinimumSize = new Draw.Size(775, 330);
            MaximumSize = MinimumSize;
        }


        private WForms.Panel BuildCliente()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Cliente"
            });
            EdCliente = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 0.87),
                SelectionLength = 0,
                ReadOnly = true
            };
            BtSelecCliente = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Seleccionar"
            };
            
            toret.Controls.Add(EdCliente);
            toret.Controls.Add(BtSelecCliente);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdCliente.Height);
            return toret;
        }

        private WForms.Panel BuildFlota()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Matrícula"
            });
            EdFlota = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 0.87),
                SelectionLength = 0,
                ReadOnly = true
            };
            BtSelecVehiculo = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Seleccionar"
            };

            toret.Controls.Add(EdFlota);
            toret.Controls.Add(BtSelecVehiculo);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdFlota.Height);
            return toret;
        }

        private WForms.Panel BuildFechaContrata()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Contratacion"
            });

            EdFechaContratacion = new WForms.DateTimePicker
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
            };
            
            toret.Controls.Add(EdFechaContratacion);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdFechaContratacion.Height);
            return toret;
        }

        private WForms.Panel BuildKmsRecorridos()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Kms Recorridos"

            });

            EdKmsRecorridos = new WForms.NumericUpDown
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
                TextAlign = WForms.HorizontalAlignment.Right,
                Minimum = 0,
                Value = 0,
                
            };

            toret.Controls.Add(EdKmsRecorridos);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdKmsRecorridos.Height);
            return toret;
        }

        private WForms.Panel BuildFechaSalida()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Fecha Salida"
            });

            EdFechaSalida = new WForms.DateTimePicker
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
            };

            toret.Controls.Add(EdFechaSalida);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdFechaSalida.Height);
            return toret;
        }

        private WForms.Panel BuildFechaEntrega()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Fecha Entrega"
            });

            EdFechaEntrega = new WForms.DateTimePicker
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
            };

            toret.Controls.Add(EdFechaEntrega);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdFechaEntrega.Height);
            return toret;
        }

        private WForms.Panel BuildImporteDia()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Importe Dia"
            });

            EdImporteDia = new WForms.NumericUpDown
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
                TextAlign = WForms.HorizontalAlignment.Right,
                Minimum = 0,
                DecimalPlaces = 2,
                Value = 0,
                RightToLeft = WForms.RightToLeft.Inherit
            };

            toret.Controls.Add(EdImporteDia);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdImporteDia.Height);
            return toret;
        }

        private WForms.Panel BuildIVA()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "IVA"
            });

            EdIVA = new WForms.NumericUpDown
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
                TextAlign = WForms.HorizontalAlignment.Right,
                Minimum = 0,
                Maximum = 1,
                Increment = 0.1M,
                DecimalPlaces = 2,
                Value = 0,
                RightToLeft = WForms.RightToLeft.Inherit
            };

            toret.Controls.Add(EdIVA);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdIVA.Height);
            return toret;
        }

        private WForms.Panel BuildPrecioLitro()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Precio Litro"
            });

            EdPrecioLitro = new WForms.NumericUpDown
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
                TextAlign = WForms.HorizontalAlignment.Right,
                Minimum = 0,
                DecimalPlaces = 2,
                Value = 0,
                RightToLeft = WForms.RightToLeft.Inherit
            };

            toret.Controls.Add(EdPrecioLitro);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdPrecioLitro.Height);
            return toret;
        }

        private WForms.Panel BuildGas()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Gas"
            });

            EdGas = new WForms.NumericUpDown
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
                TextAlign = WForms.HorizontalAlignment.Right,
                Minimum = 0,
                DecimalPlaces = 2,
                Value = 0,
                RightToLeft = WForms.RightToLeft.Inherit
            };

            toret.Controls.Add(EdGas);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdGas.Height);
            return toret;
        }
        
        private WForms.Panel BuildFiltroYear()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Filtrar por año"
            });

            EdYearFiltro = new WForms.NumericUpDown
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
                TextAlign = WForms.HorizontalAlignment.Right,
                Minimum = 2000,
                Maximum = 2030,
                Value = 2020,
                RightToLeft = WForms.RightToLeft.Inherit
            };

            toret.Controls.Add(EdYearFiltro);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdYearFiltro.Height);
            return toret;
        }
        
        private WForms.Panel BuildFiltroFecha()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Filtrar por fecha"
            });

            EdFechaFiltro = new WForms.DateTimePicker()
            {
                Dock = WForms.DockStyle.Right,
                Width = (int)(Width * 1.25),
            };
            

            toret.Controls.Add(EdFechaFiltro);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdFechaFiltro.Height);
            return toret;
        }
        
        private WForms.Panel BuildRelleno()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.MaximumSize = new Draw.Size(int.MaxValue, EdGas.Height);
            return toret;
        }

        public WForms.Panel BuildPanelBotones()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            BtModificar = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Modificar"
            };
            toret.Controls.Add(BtModificar);
            BtBorrar = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Borrar"
            };
            toret.Controls.Add(BtBorrar);
            toret.Dock = WForms.DockStyle.Top;
            toret.MaximumSize = new Draw.Size(int.MaxValue, 30);


            BtInsertar = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Insertar"
            };
            toret.Controls.Add(BtInsertar);

            toret.Controls.Add(BtModificar);
            BtAceptar = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                DialogResult = WForms.DialogResult.OK,
                Text = "&Aceptar"
            };
            toret.Controls.Add(BtAceptar);

            toret.Controls.Add(BtModificar);
            BtCancelar = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                DialogResult = WForms.DialogResult.Cancel,
                Text = "&Cancelar"
            };
            toret.Controls.Add(BtCancelar);


            toret.Dock = WForms.DockStyle.Top;
            toret.MaximumSize = new Draw.Size(int.MaxValue, 30);
            return toret;
        }

        public WForms.Panel BuildPanelBotonesFiltro()
        {
  
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            //Botones individual_búsquedas
            BtPendiente = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Pendientes"
            };
            toret.Controls.Add(BtPendiente);
            BtFiltroYear = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Filtrar año"
            };
            toret.Controls.Add(BtFiltroYear);
            
            BtFiltroFecha = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Filtrar fecha"
            };
            toret.Controls.Add(BtFiltroFecha);
            
            BtVolver = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Volver"
            };
            toret.Controls.Add(BtVolver);
            toret.Dock = WForms.DockStyle.Top;
            toret.MaximumSize = new Draw.Size(int.MaxValue, 30);
            return toret;
        }

        private WForms.Panel BuildFactura()
        {
            var toret = new WForms.Panel();
            EdFactura = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Left,
                Width = 765,
                TextAlign = WForms.HorizontalAlignment.Left,
                Multiline = true,
                ScrollBars = WForms.ScrollBars.Vertical
            };
            EdFactura.Enabled = false;
            var lbFactura = new WForms.Label
            {
                Text = "FACTURA",
                Dock = WForms.DockStyle.Top
            };

            toret.Controls.Add(this.EdFactura);
            toret.Controls.Add(lbFactura);
            toret.Dock = WForms.DockStyle.Top;

            return toret;
        }


        public void ModoVolver()
        {
            BtBorrar.Enabled = false;
            BtModificar.Enabled = false;
            BtInsertar.Enabled = false;
            BtAceptar.Enabled = false;
            BtCancelar.Enabled = false;
            BtPendiente.Enabled = false;
            BtFiltroFecha.Enabled = false;
            BtFiltroYear.Enabled = false;
            BtVolver.Enabled = true;
        }
        
        public void ModoConsulta()
        {
            EdCliente.Enabled = false;
            EdFlota.Enabled = false;
            EdFechaContratacion.Enabled = false;
            EdKmsRecorridos.Enabled = false;
            EdFechaSalida.Enabled = false;
            EdFechaEntrega.Enabled = false;
            EdImporteDia.Enabled = false;
            EdIVA.Enabled = false;
            EdPrecioLitro.Enabled = false;
            EdGas.Enabled = false;
            EdFechaFiltro.Enabled = true;
            EdYearFiltro.Enabled = true;

            DeshabilitarBtAceptar();
        }

        public void ModoModificar()
        {
            EdCliente.Enabled = false;
            EdFlota.Enabled = false;
            EdFechaContratacion.Enabled = false;
            EdKmsRecorridos.Enabled = true;
            EdFechaSalida.Enabled = true;
            EdFechaEntrega.Enabled = true;
            EdImporteDia.Enabled = true;
            EdIVA.Enabled = true;
            EdPrecioLitro.Enabled = true;
            EdGas.Enabled = true;
            BtSelecCliente.Enabled = false;
            BtSelecVehiculo.Enabled = false;
            EdFechaFiltro.Enabled = false;
            EdYearFiltro.Enabled = false;

            EdKmsRecorridos.Focus();
            HabilitarBtAceptar();
        }

        public void ModoInsercion()
        {
            EdCliente.Text = "";
            EdFlota.Text = "";
            EdFechaContratacion.Text = "";
            EdKmsRecorridos.Text = "";
            EdFechaSalida.Text = "";
            EdFechaEntrega.Text = "";
            EdImporteDia.Text = "";
            EdIVA.Text = "";
            EdPrecioLitro.Text = "";
            EdGas.Text = "";

            EdCliente.Enabled = true;
            EdFlota.Enabled = true;
            EdFechaContratacion.Enabled = true;
            EdKmsRecorridos.Enabled = true;
            EdFechaSalida.Enabled = true;
            EdFechaEntrega.Enabled = true;
            EdImporteDia.Enabled = true;
            EdIVA.Enabled = true;
            EdPrecioLitro.Enabled = true;
            EdGas.Enabled = true;
            BtSelecCliente.Enabled = true;
            BtSelecVehiculo.Enabled = true;
            EdFechaFiltro.Enabled = false;
            EdYearFiltro.Enabled = false;

            EdCliente.Focus();
            HabilitarBtAceptar();

        }

        private void HabilitarBtAceptar()
        {
            BtBorrar.Enabled = false;
            BtModificar.Enabled = false;
            BtInsertar.Enabled = false;
            BtAceptar.Enabled = true;
            BtCancelar.Enabled = true;
            BtPendiente.Enabled = false;
            BtFiltroFecha.Enabled = false;
            BtFiltroYear.Enabled = false;
            BtVolver.Enabled = false;
        }

        private void DeshabilitarBtAceptar()
        {
            BtBorrar.Enabled = true;
            BtModificar.Enabled = true;
            BtInsertar.Enabled = true;
            BtAceptar.Enabled = false;
            BtCancelar.Enabled = false;
            BtSelecCliente.Enabled = false;
            BtSelecVehiculo.Enabled = false;
            BtPendiente.Enabled = true;
            BtFiltroFecha.Enabled = true;
            BtFiltroYear.Enabled = true;
            BtVolver.Enabled = false;
        }
    }
}
