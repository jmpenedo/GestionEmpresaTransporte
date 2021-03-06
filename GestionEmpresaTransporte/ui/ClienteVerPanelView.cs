﻿namespace GestionEmpresaTransporte.ui
{
    using Draw = System.Drawing;
    using WForms = System.Windows.Forms;

    public class ClienteVerPanelView : WForms.Panel
    {
        public ClienteVerPanelView()
        {
            Build();
        }

        /*Datos del cliente*/
        public WForms.TextBox EdNif { get; private set; }
        public WForms.TextBox EdNombre { get; private set; }
        public WForms.TextBox EdTelefono { get; private set; }
        public WForms.TextBox EdCorreo { get; private set; }
        public WForms.TextBox EdDireccion { get; private set; }
        public WForms.Button BtAceptar { get; private set; }
        public WForms.Button BtCancelar { get; private set; }
        public WForms.Button BtModificar { get; private set; }
        public WForms.Button BtBorrar { get; private set; }
        public WForms.Button BtInsertar { get; private set; }

        //Filtros

        public WForms.NumericUpDown EdYearFiltro { get; private set; }
        public WForms.Button BtReservasCliente { get; private set; }

        public WForms.Button BtReservasClienteYear { get; private set; }

        public WForms.Button BtSeleccionar { get; private set; }

        public WForms.Button BtVolver { get; private set; }

        private void Build()
        {
            var pnlTable = new WForms.TableLayoutPanel();
            pnlTable.SuspendLayout();
            pnlTable.Dock = WForms.DockStyle.Fill;
            pnlTable.Controls.Add(BuildNif());
            pnlTable.Controls.Add(BuildNombre());
            pnlTable.Controls.Add(BuildTelefono());
            pnlTable.Controls.Add(BuildCorreo());
            pnlTable.Controls.Add(BuildFiltroYear());
            pnlTable.Controls.Add(BuildPanelBotones());
            pnlTable.Controls.Add(BuildPanelBotonesSeleccion());


            pnlTable.ResumeLayout(false);
            pnlTable.MinimumSize = new Draw.Size(400, 300);
            pnlTable.MaximumSize = pnlTable.MinimumSize;
            Controls.Add(pnlTable);
            Controls.Add(BuildDireccion());

            MinimumSize = new Draw.Size(775, 300);
            MaximumSize = MinimumSize;

            ModoSeleccion(false);
            ModoInicial();
        }


        private WForms.Panel BuildNif()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Nif"
            });
            EdNif = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Right
            };
            toret.Controls.Add(EdNif);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdNif.Height);
            return toret;
        }

        private WForms.Panel BuildNombre()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };
            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Nombre"
            });
            EdNombre = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Right
            };

            toret.Controls.Add(EdNombre);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdNombre.Height);
            return toret;
        }

        private WForms.Panel BuildTelefono()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Telefono"
            });

            EdTelefono = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Right
            };

            toret.Controls.Add(EdTelefono);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdTelefono.Height);
            return toret;
        }


        private WForms.Panel BuildCorreo()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Fill
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Correo"
            });

            EdCorreo = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = (int) (Width * 0.90),
                TextAlign = WForms.HorizontalAlignment.Right
            };

            toret.Controls.Add(EdCorreo);
            toret.MaximumSize = new Draw.Size(int.MaxValue, EdCorreo.Height);
            return toret;
        }

        private WForms.Panel BuildDireccion()
        {
            var toret = new WForms.Panel
            {
                Dock = WForms.DockStyle.Right
            };

            toret.Controls.Add(new WForms.Label
            {
                Dock = WForms.DockStyle.Left,
                Text = "Direccion"
            });

            EdDireccion = new WForms.TextBox
            {
                Dock = WForms.DockStyle.Right,
                Width = 250,
                Height = 150,
                TextAlign = WForms.HorizontalAlignment.Left,
                Multiline = true,
                ScrollBars = WForms.ScrollBars.Vertical
            };

            toret.Controls.Add(EdDireccion);
            toret.MinimumSize = new Draw.Size(350, 150);
            toret.MaximumSize = toret.MinimumSize;

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
                Width = (int) (Width * 1.25),
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

        public WForms.Panel BuildPanelBotonesSeleccion()
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
            toret.Controls.Add(BtSeleccionar);
            BtVolver = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Volver"
            };
            toret.Controls.Add(BtVolver);
            BtReservasCliente = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Reservas"
            };
            toret.Controls.Add(BtReservasCliente);
            BtReservasClienteYear = new WForms.Button
            {
                Dock = WForms.DockStyle.Right,
                Text = "&Res/año"
            };
            toret.Controls.Add(BtReservasClienteYear);
            toret.Dock = WForms.DockStyle.Top;

            toret.MaximumSize = new Draw.Size(int.MaxValue, 30);

            return toret;
        }

        public void ModoInicial()
        {
            EdNif.Enabled = false;
            EdNombre.Enabled = false;
            EdTelefono.Enabled = false;
            EdCorreo.Enabled = false;
            EdDireccion.Enabled = false;
            DeshabilitarBtAceptar();
        }

        public void ModoModificar()
        {
            EdNif.Enabled = false;
            EdNombre.Enabled = true;
            EdTelefono.Enabled = true;
            EdCorreo.Enabled = true;
            EdDireccion.Enabled = true;
            EdNombre.Focus();
            HabilitarBtAceptar();
        }

        public void ModoInsercion()
        {
            EdNif.Clear();
            EdNombre.Clear();
            EdTelefono.Clear();
            EdCorreo.Clear();
            EdDireccion.Clear();
            EdNif.Enabled = true;
            EdNombre.Enabled = true;
            EdTelefono.Enabled = true;
            EdCorreo.Enabled = true;
            EdDireccion.Enabled = true;
            EdNif.Focus();
            HabilitarBtAceptar();
        }

        private void HabilitarBtAceptar()
        {
            BtBorrar.Enabled = false;
            BtModificar.Enabled = false;
            BtInsertar.Enabled = false;
            BtAceptar.Enabled = true;
            BtCancelar.Enabled = true;
            BtReservasCliente.Enabled = false;
            BtReservasClienteYear.Enabled = false;
        }

        private void DeshabilitarBtAceptar()
        {
            BtBorrar.Enabled = true;
            BtModificar.Enabled = true;
            BtInsertar.Enabled = true;
            BtAceptar.Enabled = false;
            BtCancelar.Enabled = false;
            BtReservasCliente.Enabled = true;
            BtReservasClienteYear.Enabled = true;
        }


        public void ModoSeleccion(bool estado)
        {
            BtSeleccionar.Visible = estado;
        }
    }
}