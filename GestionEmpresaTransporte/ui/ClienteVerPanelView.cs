namespace GestionEmpresaTransporte.ui
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

        private void Build()
        {
            var pnlTable = new WForms.TableLayoutPanel();
            pnlTable.SuspendLayout();
            pnlTable.Dock = WForms.DockStyle.Fill;
            pnlTable.Controls.Add(BuildNif());
            pnlTable.Controls.Add(BuildNombre());
            pnlTable.Controls.Add(BuildTelefono());
            pnlTable.Controls.Add(BuildCorreo());
            pnlTable.Controls.Add(BuildPanelBotones());
            pnlTable.ResumeLayout(false);
            pnlTable.MinimumSize = new Draw.Size(400, 300);
            pnlTable.MaximumSize = pnlTable.MinimumSize;
            Controls.Add(pnlTable);
            Controls.Add(BuildDireccion());
            MinimumSize = new Draw.Size(775, 300);
            MaximumSize = MinimumSize;
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
                TextAlign = WForms.HorizontalAlignment.Left,
                Multiline = true,
                ScrollBars = WForms.ScrollBars.Vertical
            };

            toret.Controls.Add(EdDireccion);
            toret.MinimumSize = new Draw.Size(350, 300);
            toret.MaximumSize = toret.MinimumSize;

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


            toret.Dock = WForms.DockStyle.Top;
            toret.MaximumSize = new Draw.Size(int.MaxValue, 30);
            return toret;
        }

        public void ModoConsulta()
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
        }

        private void DeshabilitarBtAceptar()
        {
            BtBorrar.Enabled = true;
            BtModificar.Enabled = true;
            BtInsertar.Enabled = true;
            BtAceptar.Enabled = false;
            BtCancelar.Enabled = false;
        }
    }
}