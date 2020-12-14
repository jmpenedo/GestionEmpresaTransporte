using System;

namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class VehiculoVerPanelView : WForms.Panel
    {
        public VehiculoVerPanelView()
        {
            Build();
        }

        /*Datos del transporte*/
        public WForms.TextBox EdMatricula { get; private set; }
        public WForms.TextBox EdModelo { get; private set; }
        public WForms.TextBox EdMarca { get; private set; }
        public WForms.DateTimePicker EdFechaFa { get; private set; }
        public WForms.DateTimePicker EdFechaAd { get; private set; }
        public WForms.NumericUpDown EdConsumo { get; private set; }

        public WForms.ComboBox EdTipo { get; private set; }

        /*Comodidades*/
        public WForms.CheckBox EdWIFI { get; private set; }
        public WForms.CheckBox EdAC { get; private set; }
        public WForms.CheckBox EdNevera { get; private set; }
        public WForms.CheckBox EdBluetooth { get; private set; }
        public WForms.CheckBox EdTV { get; private set; }


        public WForms.Button BtAceptar { get; private set; }
        public WForms.Button BtCancelar { get; private set; }
        public WForms.Button BtModificar { get; private set; }
        public WForms.Button BtBorrar { get; private set; }
        public WForms.Button BtInsertar { get; private set; }

        public WForms.Button BtSeleccionar { get; private set; }


        public WForms.Button BtVolver { get; private set; }
        
        //Búsquedas
        
        public WForms.Button btDisponibles { get; private set; }
        
        public WForms.Button btReservas { get; private set; }
        
        public WForms.Button btReservasYear { get; private set; }

        public WForms.Button btPendientes { get; private set; }
        
        public WForms.Button btSalirFiltrado { get; private set; }
        
        public WForms.ComboBox EdTipoFiltro { get; private set; }
        
        public WForms.NumericUpDown EdYearFiltro { get; private set; }
        
        
        private void Build()
        {
            var PanelDatos = new WForms.TableLayoutPanel();
            PanelDatos.SuspendLayout();
            PanelDatos.ColumnCount = 2;
            PanelDatos.Dock = WForms.DockStyle.Fill;
            PanelDatos.ColumnStyles.Add(new WForms.ColumnStyle(WForms.SizeType.Percent, 70F));
            PanelDatos.ColumnStyles.Add(new WForms.ColumnStyle(WForms.SizeType.Percent, 30F));

            var pnlTable1 = new WForms.TableLayoutPanel();
            pnlTable1.SuspendLayout();
            pnlTable1.Dock = WForms.DockStyle.Fill;

            var pnlTable2 = new WForms.TableLayoutPanel();
            pnlTable2.SuspendLayout();
            pnlTable2.Dock = WForms.DockStyle.Fill;

            pnlTable1.Controls.Add(BuildMatricula());
            pnlTable1.Controls.Add(BuildMarca());
            pnlTable1.Controls.Add(BuildModelo());
            pnlTable1.Controls.Add(BuildConsumo());
            pnlTable1.Controls.Add(BuildFechaFa());
            pnlTable1.Controls.Add(BuildFechaAd());
            pnlTable1.Controls.Add(BuildYearFiltro());

            pnlTable2.Controls.Add(BuildTipo());
            pnlTable2.Controls.Add(BuildWIFI());
            pnlTable2.Controls.Add(BuildAC());
            pnlTable2.Controls.Add(BuildTV());
            pnlTable2.Controls.Add(BuildBluetooth());
            pnlTable2.Controls.Add(BuildNevera());
            pnlTable2.Controls.Add(BuildTipoFiltro());

            pnlTable1.Controls.Add(BuildPanelBotones());
            pnlTable2.Controls.Add(BuildPanelBotonVolver());
            pnlTable1.Controls.Add(BuildPanelBotonesFiltro());

            pnlTable2.ResumeLayout(false);
            pnlTable1.ResumeLayout(false);

            pnlTable1.MaximumSize = pnlTable1.MinimumSize;
            pnlTable2.MaximumSize = pnlTable2.MinimumSize;

            PanelDatos.Controls.Add(pnlTable1, 0, 0);
            PanelDatos.Controls.Add(pnlTable2, 1, 0);

            PanelDatos.ResumeLayout(false);

            Controls.Add(PanelDatos);

            MinimumSize = new Draw.Size(775, 300);
            MaximumSize = MinimumSize;
        }

        private WForms.Panel BuildMatricula()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Matricula"
            });
            EdMatricula = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.40),
                TextAlign = WForms.HorizontalAlignment.Right
            };
            toret.Controls.Add(EdMatricula);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdMatricula.Height);
            return toret;
        }

        private WForms.Panel BuildMarca()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Marca"
            });
            EdMarca = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.80),
                TextAlign = WForms.HorizontalAlignment.Right
            };
            toret.Controls.Add(EdMarca);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdMarca.Height);
            return toret;
        }

        private WForms.Panel BuildModelo()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Modelo"
            });
            EdModelo = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.80),
                TextAlign = WForms.HorizontalAlignment.Right
            };
            toret.Controls.Add(EdModelo);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdModelo.Height);
            return toret;
        }

        private WForms.Panel BuildConsumo()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Consumo L/100Km",
                Width = Width
            });

            EdConsumo = new WForms.NumericUpDown
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.40),
                Value = 4,
                TextAlign = WForms.HorizontalAlignment.Center,
                Minimum = 0,
                Maximum = 40,
                DecimalPlaces = 1,
                Increment = 0.1M
            };
            toret.Controls.Add(EdConsumo);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdConsumo.Height);
            return toret;
        }

        private WForms.Panel BuildFechaFa()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Fecha Fabricacion"
            });

            EdFechaFa = new WForms.DateTimePicker
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 1.00)
            };

            toret.Controls.Add(EdFechaFa);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdFechaFa.Height);
            return toret;
        }

        private WForms.Panel BuildFechaAd()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Fecha Adquisicion"
            });

            EdFechaAd = new WForms.DateTimePicker
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 1.00),
                MaxDate = DateTime.Today
            };

            toret.Controls.Add(EdFechaAd);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdFechaAd.Height);
            return toret;
        }

        private WForms.Panel BuildTipo()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Tipo"
            });
            EdTipo = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.60),
                RightToLeft = WForms.RightToLeft.Inherit
            };
            object[] tipos = {"Furgoneta", "Camion", "Camion Articulado"};
            EdTipo.Items.AddRange(tipos);
            toret.Controls.Add(EdTipo);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdTipo.Height);
            return toret;
        }

        private WForms.Panel BuildTipoFiltro()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Filtrar por tipo"
            });
            EdTipoFiltro = new WForms.ComboBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.60),
                RightToLeft = WForms.RightToLeft.Inherit
            };
            object[] tipos = {"No filtrar","Furgoneta", "Camion", "Camion Articulado"};
            EdTipoFiltro.Items.AddRange(tipos);
            EdTipoFiltro.SelectedItem = "No filtrar";
            toret.Controls.Add(EdTipoFiltro);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdTipo.Height);
            return toret;
        }

        private WForms.Panel BuildWIFI()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "WIFI"
            });

            EdWIFI = new WForms.CheckBox
            {
                Dock = WForms.DockStyle.Right
            };
            toret.Controls.Add(EdWIFI);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdWIFI.Height / 4);
            return toret;
        }

        private WForms.Panel BuildAC()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "A/C"
            });

            EdAC = new WForms.CheckBox
            {
                Dock = WForms.DockStyle.Right
            };
            toret.Controls.Add(EdAC);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdAC.Height / 4);
            return toret;
        }

        private WForms.Panel BuildBluetooth()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Bluetooth"
            });

            EdBluetooth = new WForms.CheckBox
            {
                Dock = WForms.DockStyle.Right
            };
            toret.Controls.Add(EdBluetooth);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdBluetooth.Height / 4);
            return toret;
        }

        private WForms.Panel BuildTV()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "TV"
            });

            EdTV = new WForms.CheckBox
            {
                Dock = WForms.DockStyle.Right
            };
            toret.Controls.Add(EdTV);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdTV.Height / 4);
            return toret;
        }

        private WForms.Panel BuildNevera()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Nevera"
            });

            EdNevera = new WForms.CheckBox
            {
                Dock = WForms.DockStyle.Right
            };
            toret.Controls.Add(EdNevera);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdNevera.Height / 4);
            return toret;
        }

        private WForms.Panel BuildYearFiltro()
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
            btReservas = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Reservas"
            };
            toret.Controls.Add(btReservas);
            btReservasYear = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Res/año"
            };
            toret.Controls.Add(btReservasYear);
            btPendientes = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Pendientes"
            };
            toret.Controls.Add(btPendientes);
            btSalirFiltrado = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Salir filtro"
            };
            toret.Controls.Add(btSalirFiltrado);
            toret.Dock = WForms.DockStyle.Top;
            toret.MaximumSize = new Draw.Size(int.MaxValue, 30);

            return toret;
        }
        
        
        public WForms.Panel BuildPanelBotonVolver()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            BtSeleccionar = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Seleccionar"
            };
            BtVolver = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Volver"
            };
            btDisponibles = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Disponibles"
            };
            toret.Controls.Add(btDisponibles);
            toret.Controls.Add(BtSeleccionar);
            toret.Controls.Add(BtVolver);
            toret.Dock = WForms.DockStyle.Top;
            toret.MaximumSize = new Draw.Size(int.MaxValue, 30);

            return toret;
        }

        public void ModoConsulta()
        {
            EdMatricula.Enabled = false;
            EdMarca.Enabled = false;
            EdModelo.Enabled = false;
            EdFechaAd.Enabled = false;
            EdFechaFa.Enabled = false;
            EdConsumo.Enabled = false;

            EdTipo.Enabled = false;
            EdWIFI.Enabled = false;
            EdNevera.Enabled = false;
            EdTV.Enabled = false;
            EdAC.Enabled = false;
            EdBluetooth.Enabled = false;

            DeshabilitarBtAceptar();
        }

        public void ModoModificar()
        {
            EdMatricula.Enabled = false;
            EdMarca.Enabled = false;
            EdModelo.Enabled = false;
            EdFechaAd.Enabled = false;
            EdFechaFa.Enabled = false;
            EdConsumo.Enabled = true;

            EdTipo.Enabled = false;
            EdWIFI.Enabled = true;
            EdNevera.Enabled = true;
            EdTV.Enabled = true;
            EdAC.Enabled = true;
            EdBluetooth.Enabled = true;

            EdConsumo.Focus();

            HabilitarBtAceptar();
        }

        public void ModoInsercion()
        {
            EdMatricula.Clear();
            EdMarca.Clear();
            EdModelo.Clear();
            EdFechaAd.Value = DateTime.Now.Date;
            EdFechaFa.Value = DateTime.Now.Date;
            EdConsumo.Text = "";

            EdTipo.Text = "";
            EdWIFI.Checked = false;
            EdNevera.Checked = false;
            EdTV.Checked = false;
            EdAC.Checked = false;
            EdBluetooth.Checked = false;


            EdMatricula.Enabled = true;
            EdMarca.Enabled = true;
            EdModelo.Enabled = true;
            EdFechaAd.Enabled = true;
            EdFechaFa.Enabled = true;
            EdConsumo.Enabled = true;

            EdTipo.Enabled = true;
            EdWIFI.Enabled = true;
            EdNevera.Enabled = true;
            EdTV.Enabled = true;
            EdAC.Enabled = true;
            EdBluetooth.Enabled = true;

            EdMatricula.Focus();

            HabilitarBtAceptar();
        }

        public void ModoSeleccion(bool estado)
        {
            BtSeleccionar.Visible = estado;
        }

        public void ModoSalir()
        {
            BtBorrar.Enabled = false;
            BtModificar.Enabled = false;
            BtInsertar.Enabled = false;
            BtAceptar.Enabled = false;
            BtCancelar.Enabled = false;
            btDisponibles.Enabled = false;
            btReservas.Enabled = false;
            btReservasYear.Enabled = false;
            btPendientes.Enabled = false;
            btSalirFiltrado.Enabled = true;
            BtVolver.Enabled = false;
        }

        private void HabilitarBtAceptar()
        {
            BtBorrar.Enabled = false;
            BtModificar.Enabled = false;
            BtInsertar.Enabled = false;
            BtAceptar.Enabled = true;
            BtCancelar.Enabled = true;
            btDisponibles.Enabled = false;
            btReservas.Enabled = false;
            btReservasYear.Enabled = false;
            btPendientes.Enabled = false;
            btSalirFiltrado.Enabled = false;
            BtVolver.Enabled = true;

        }

        private void DeshabilitarBtAceptar()
        {
            BtBorrar.Enabled = true;
            BtModificar.Enabled = true;
            BtInsertar.Enabled = true;
            BtAceptar.Enabled = false;
            BtCancelar.Enabled = false;
            btDisponibles.Enabled = true;
            btReservas.Enabled = true;
            btReservasYear.Enabled = true;
            btPendientes.Enabled = true;
            btSalirFiltrado.Enabled = false;
            BtVolver.Enabled = true;
        }
        
        
    }
}